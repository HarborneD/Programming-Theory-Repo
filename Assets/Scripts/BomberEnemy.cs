using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PathCreation;

public class BomberEnemy : Enemy
{
    [SerializeField] float attackDistanceAlongPath = 0.45f;
    [SerializeField] float hoverBeforeAttackLength = 1f;
    [SerializeField] float hoverAfterAttackLength = 1f;

    [SerializeField] GameObject rocketPrefab;
    [SerializeField] List<GameObject> rocketLaunchLocations = new List<GameObject>();

    [SerializeField] int rocketsToFire = 2;

    bool hasAttacked = false;
    float endHoveringTime;


    protected override void Start()
    {
        base.Start();

        pathFollower.endOfPath.AddListener(HandleFlightPathStart);
    }


    protected override void HandleMovement()
    {
        base.HandleMovement();
        switch (state)
        {
            case AiState.flyby:
                if(!hasAttacked && pathFollower.distanceTravelledRatio >= attackDistanceAlongPath)
                {
                    endHoveringTime = Time.time + hoverBeforeAttackLength;
                    state = AiState.hovering;
                    pathFollower.speed = 0;
                    
                }
                break;


            case AiState.hovering:
                if (Time.time >= endHoveringTime)
                {
                    state = hasAttacked ? AiState.flyby :  AiState.attacking;
                    if(state == AiState.flyby)
                    {
                        pathFollower.speed = 55;
                    }
                    
                }
                break;


            case AiState.attacking:
                state = AiState.hovering;
                hasAttacked = true;
                endHoveringTime = Time.time + hoverAfterAttackLength;
                Attack();
                break;
        }
    }

    protected void HandleFlightPathStart()
    {
        hasAttacked = false;
    }

    protected override void Attack()
    {
        FireRockets(rocketsToFire);
    }


    void FireRockets(int numRockets)
    {
        List<Vector3> rocketPathStarts = new List<Vector3>();
        foreach (PathCreator rocketPath in spawnManager.rocketPaths)
        {
            rocketPathStarts.Add(rocketPath.path.GetPointAtDistance(0, EndOfPathInstruction.Stop));
        }

        List<int> usedRocketPaths = new List<int>();

        for (int launchLocationId = 0; launchLocationId < Mathf.Min(numRockets, rocketLaunchLocations.Count); launchLocationId++)
        {
            Vector3 launchLocation = rocketLaunchLocations[launchLocationId].transform.position;
            GameObject rocket = Instantiate(rocketPrefab, launchLocation, Quaternion.identity);
            
            int pathIndex = GetClosestPathIndex(launchLocation, usedRocketPaths, rocketPathStarts);

            if(usedRocketPaths.Count == rocketPathStarts.Count - 1)
            {
                usedRocketPaths = new List<int>();
            }
            else
            {
                usedRocketPaths.Add(pathIndex);
            }
            rocket.GetComponent<PathedRocket>().FireAlongPath(spawnManager.rocketPaths[pathIndex]);

        }
    }

    int GetClosestPathIndex(Vector3 position, List<int> skipIndexes, List<Vector3> rocketPathStarts)
    {
        float currentClosestDistance = float.MaxValue;
        int currentClosestIndex = -1;

        for (int pathIndex = 0; pathIndex < rocketPathStarts.Count; pathIndex++)
        {
            if (skipIndexes.Contains(pathIndex))
            {
                continue;
            }
            float distance = (rocketPathStarts[pathIndex] - position).magnitude;

            if(distance < currentClosestDistance)
            {
                currentClosestIndex = pathIndex;
                currentClosestDistance = distance;
            }
        }

        return currentClosestIndex;
    }

}

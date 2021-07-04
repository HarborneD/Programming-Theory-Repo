using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Enemy
{
    [SerializeField] float attackDistanceAlongPath = 0.4f;
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
        for (int launchLocationId = 0; launchLocationId < Mathf.Min(numRockets, rocketLaunchLocations.Count); launchLocationId++)
        {
            GameObject rocket = Instantiate(rocketPrefab, rocketLaunchLocations[launchLocationId].transform.position, Quaternion.identity);
            rocket.GetComponent<Rocket>().FireAtTarget(player);
        }
    }

}

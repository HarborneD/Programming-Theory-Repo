using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector3 rotationToRoadNomal;

    [SerializeField]
    Vector3 positionOffsetToRoadPath;

    [SerializeField]
    PathCreator pathCreator;

    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    
    float distanceTravelled;

    void Start()
    {
        if (pathCreator != null)
        {
            SetDistanceTravelledToCurrentLocation();
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    }


    void MoveAlongPath()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            Vector3 localEulerTemp = transform.localEulerAngles;
            localEulerTemp += rotationToRoadNomal;
            transform.localEulerAngles = localEulerTemp;

            Vector3 worldOffset = transform.rotation * positionOffsetToRoadPath;
            transform.position += worldOffset;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        SetDistanceTravelledToCurrentLocation();
    }

    void SetDistanceTravelledToCurrentLocation()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}

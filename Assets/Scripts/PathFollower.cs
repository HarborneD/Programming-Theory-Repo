using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    Vector3 rotationToPathNomal;

    [SerializeField]
    Vector3 positionOffsetToPath;

    public PathCreator pathCreator;

    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;

    float distanceTravelled;

    private void Update()
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
            localEulerTemp += rotationToPathNomal;
            transform.localEulerAngles = localEulerTemp;

            Vector3 worldOffset = transform.rotation * positionOffsetToPath;
            transform.position += worldOffset;
        }
    }

    void SetDistanceTravelledToCurrentLocation()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}

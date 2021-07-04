using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Events;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    float minProgressTowardsTargetMovePosition = 1f;
    [SerializeField]
    float maxProgressTowardsTargetMovePosition = 1f;

    [SerializeField]
    Vector3 rotationToPathNomal;

    [SerializeField]
    Vector3 positionOffsetToPath;

    public PathCreator pathCreator;

    public UnityEvent endOfPath;

    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public float distanceTravelled { get; private set; }
    public float distanceTravelledRatio => distanceTravelled / pathCreator.path.length;

    private void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            if(distanceTravelledRatio >= 1 | distanceTravelled < 0)
            {
                distanceTravelled %= pathCreator.path.length;
                endOfPath.Invoke();
            }
            transform.position = Vector3.Lerp(transform.position, pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction), Random.Range(minProgressTowardsTargetMovePosition, maxProgressTowardsTargetMovePosition));
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

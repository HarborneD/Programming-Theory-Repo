using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMountedGun : MonoBehaviour
{
    [SerializeField]
    GameObject aim;

    [SerializeField]
    GameObject muzzle;

    [SerializeField]
    Vector3 minRotations;
    [SerializeField]
    Vector3 maxRotations;

    [SerializeField]
    Vector3 minLocalAimPosition;
    [SerializeField]
    Vector3 maxLocalAimPosition;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float shotsPerSecond = 5;

    [SerializeField] ThingWithHp thingAttachedTo;

    float secondsPerShot;
    float nextShotTimestamp;
    

    // Start is called before the first frame update
    void Start()
    {
        secondsPerShot = 1 / shotsPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (thingAttachedTo.currentHp > 0)
        {
            MoveAim();
            MoveGunTowardsAim();
            CheckForShootInputAndShoot();
        }
    }


    void MoveGunTowardsAim()
    {
        transform.LookAt(aim.transform);
        transform.localEulerAngles = BoundRotation(transform.localEulerAngles);
    }

    Vector3 BoundRotation(Vector3 eulerRotation)
    {
        float roatationX = eulerRotation.x >= 180 ? eulerRotation.x - 360 : eulerRotation.x;
        float roatationY = eulerRotation.y >= 180 ? eulerRotation.y - 360 : eulerRotation.y;

        return new Vector3(
            Mathf.Clamp(roatationX, minRotations.x, maxRotations.x),
            Mathf.Clamp(roatationY, minRotations.y, maxRotations.y),
            Mathf.Clamp(eulerRotation.z, minRotations.z, maxRotations.z));
    }

    void MoveAim()
    {
        float mousePosXRatio = Input.mousePosition.x / Screen.width;
        float mousePosYRatio = Input.mousePosition.y / Screen.height;

        aim.transform.localPosition = new Vector3(
            Mathf.Lerp(minLocalAimPosition.x, maxLocalAimPosition.x, mousePosXRatio),
            Mathf.Lerp(minLocalAimPosition.y, maxLocalAimPosition.y, mousePosYRatio),
            aim.transform.localPosition.z);
    }


    void CheckForShootInputAndShoot()
    {
        if (Input.GetMouseButton(0))
        {
            FireGun();
        }
    }
    
    void FireGun()
    {
        if(Time.time >= nextShotTimestamp)
        {
            GameObject projectile = Instantiate(projectilePrefab, muzzle.transform.position, Quaternion.identity);
            Vector3 fireDirection = (aim.transform.position - muzzle.transform.position).normalized;
            projectile.GetComponent<Projectile>().FireInDirection(fireDirection);
            projectile.transform.forward = fireDirection;
            nextShotTimestamp = Time.time + secondsPerShot;
        }
    }
}

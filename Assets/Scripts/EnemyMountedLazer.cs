using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMountedLazer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> muzzles = new List<GameObject>();

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float shotsPerSecond = 5;

    float secondsPerShot;
    float nextShotTimestamp;

    Vector3 aimLocation;
    

    // Start is called before the first frame update
    void Start()
    {
        secondsPerShot = 1 / shotsPerSecond;
    }

    public void MoveAim(Vector3 targetLocation)
    {
        aimLocation = targetLocation;
    }


    public void FireGun()
    {
        if(Time.time >= nextShotTimestamp)
        {
            foreach (GameObject muzzle in muzzles)
            {
                GameObject projectile = Instantiate(projectilePrefab, muzzle.transform.position, Quaternion.identity);
                Vector3 fireDirection = (aimLocation - muzzle.transform.position).normalized;
                projectile.GetComponent<Projectile>().FireInDirection(fireDirection);
                projectile.transform.forward = fireDirection;
                
            }
            nextShotTimestamp = Time.time + secondsPerShot;
        }
    }
}

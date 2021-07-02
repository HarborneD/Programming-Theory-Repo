using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float travelSpeed = 300f;
    Vector3 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDirection * travelSpeed * Time.deltaTime;
        transform.forward = shootDirection;
    }

    public void FireInDirection(Vector3 direction)
    {
        shootDirection = direction;
    }
}

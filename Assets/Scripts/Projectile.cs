using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] int _damage;
    [SerializeField] GameObject meshObject;
    public int damage { get; private set; } = 10;
    [SerializeField]  float travelSpeed = 300f;
    protected Vector3 shootDirection;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        damage = _damage;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += shootDirection * travelSpeed * Time.deltaTime;
        transform.forward = shootDirection;
    }

    public void FireInDirection(Vector3 direction)
    {
        shootDirection = direction;
    }

    public virtual void HandleHit()
    {
        Destroy(meshObject);
        hitParticles.Play();
        Destroy(gameObject, 1f);
    }
}

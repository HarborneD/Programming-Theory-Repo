using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathFollower))]
public class Enemy : ThingWithHpAndShield
{
    public PathFollower pathFollower;

    public AiState state;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        HandleMovement();
    }

    protected virtual void Attack()
    {
        Debug.Log("Enemy Attack");
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            TakeDamage(projectile.damage);
            projectile.HandleHit();
        }
    }

    protected virtual void HandleMovement()
    {

    }


    
}

public enum AiState
{
    flyby,
    hovering,
    attacking
}
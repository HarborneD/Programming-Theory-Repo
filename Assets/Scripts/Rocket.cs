using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    GameObject target;

    protected override void Update()
    {
        UpdateShootDirection();

        base.Update();
    }

    public void FireAtTarget(GameObject target)
    {
        this.target = target;
        shootDirection = target.transform.position;
    }

    void UpdateShootDirection()
    {
        shootDirection = (target.transform.position - transform.position).normalized;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            projectile.HandleHit();
            HandleHit();
        }
    }
}

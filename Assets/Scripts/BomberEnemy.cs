using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Hit");
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            TakeDamage(projectile.damage);
            projectile.HandleHit();
        }
    }

}

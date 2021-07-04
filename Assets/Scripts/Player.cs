using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Player : ThingWithHpAndShield
{
    [SerializeField] ParticleSystem damagedFire;
    
    
    protected override void Start()
    {
        base.Start();

        Cursor.visible = false;

        GetComponent<PathFollower>().endOfPath.AddListener(HandleRoadPathStart);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(currentHp / maxHp <= 0.25)
        {
            damagedFire.Play();
        }

    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();

            TakeDamage(projectile.damage);
            projectile.HandleHit();

            Debug.Log("Player Health: " + currentHp);
        }
    }

    public override void Handle0Hp()
    {
        GetComponent<PathFollower>().speed = 0;
        Debug.Log("Game Over");
    }

    protected void HandleRoadPathStart()
    {
        currentShield = maxShield;
        healthUi.UpdateShields(currentShield, maxShield);
    }
}

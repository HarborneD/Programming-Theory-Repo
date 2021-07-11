using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathFollower))]
public class Enemy : ThingWithHpAndShield
{
    public GameManager gameManager;
    [SerializeField] int scoreValue;

    public PathFollower pathFollower;

    public AiState state = AiState.flyby;

    protected GameObject player;
    public EnemySpawnManager spawnManager;

    protected override void Start()
    {
        player = GameObject.Find("Player");

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

    public override void Handle0Hp()
    {
        gameManager.AddToScore(scoreValue);
        base.Handle0Hp();
    }

}

public enum AiState
{
    flyby,
    hovering,
    attacking
}
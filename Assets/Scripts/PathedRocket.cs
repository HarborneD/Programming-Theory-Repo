using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathFollower))]
public class PathedRocket : Projectile
{
    [SerializeField] PathFollower pathFollower;

    bool onPath = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!onPath)
        {
            UpdateShootDirection();
            base.Update();
        }
    }

    public void FireAlongPath(PathCreator path)
    {
        this.pathFollower.pathCreator = path;
        Vector3 differenceVector = path.path.GetPointAtDistance(0, EndOfPathInstruction.Stop) - transform.position;
        shootDirection = differenceVector.normalized;

        onPath = true;
    }

    void UpdateShootDirection()
    {
        Vector3 differenceVector = pathFollower.pathCreator.path.GetPointAtDistance(0, EndOfPathInstruction.Stop) - transform.position;
        shootDirection = differenceVector.normalized;

        if (differenceVector.magnitude <= 5f)
        {
            transform.position = pathFollower.pathCreator.path.GetPointAtDistance(0, EndOfPathInstruction.Stop);
            pathFollower.enabled = true;
            onPath = true;
        }
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

    public override void HandleHit()
    {
        base.HandleHit();
        pathFollower.enabled = false;
    }
}

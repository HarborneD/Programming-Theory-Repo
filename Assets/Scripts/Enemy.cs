using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathFollower))]
public class Enemy : ThingWithHpAndShield
{
    PathFollower pathFollower;


    protected override void Start()
    {
        base.Start();
    
        pathFollower = GetComponent<PathFollower>();
    }

    private void Update()
    {
    }
}

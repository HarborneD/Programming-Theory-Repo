using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    List<PathCreator> spawnPath;

    [SerializeField]
    GameObject laserEnemeyPrefab;
    [SerializeField]
    GameObject rocketEnemeyPrefab;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayerTransform();
    }

    void MoveToPlayerTransform()
    {
        transform.position = player.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, 0.5f);
    }
}

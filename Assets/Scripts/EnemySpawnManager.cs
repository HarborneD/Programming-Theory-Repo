using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    List<PathCreator> spawnPaths = new List<PathCreator>();

    [SerializeField]
    GameObject laserEnemeyPrefab;
    [SerializeField]
    GameObject rocketEnemeyPrefab;

    Dictionary<EnemyType, GameObject> enemyTypePrefabLookup = new Dictionary<EnemyType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        enemyTypePrefabLookup.Add(EnemyType.laser, laserEnemeyPrefab);
        enemyTypePrefabLookup.Add(EnemyType.rocket, rocketEnemeyPrefab);

        //SpawnRandomEnemyOnRandomPath();

        SpawnEnemy(0, laserEnemeyPrefab);
        SpawnEnemy(3, rocketEnemeyPrefab);
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


    void SpawnEnemy(int pathIndex, GameObject vehiclePrefab)
    {
        Vector3 spawnLocation = transform.position;
        spawnLocation.y += 100;

        GameObject enemyObject = Instantiate(vehiclePrefab, spawnLocation, Quaternion.identity);
        enemyObject.GetComponent<Enemy>().pathFollower.pathCreator = spawnPaths[pathIndex];
    }

    void SpawnEnemyOnRandomPath(GameObject vehiclePrefab)
    {
        int pathIndex = Random.Range(0, spawnPaths.Count);
        SpawnEnemy(pathIndex, vehiclePrefab);
    }

    void SpawnRandomEnemyOnRandomPath()
    {
        EnemyType enemyType = (EnemyType)Random.Range(0, (int)EnemyType.COUNT);
        SpawnEnemyOnRandomPath(enemyTypePrefabLookup[enemyType]);
    }


    enum EnemyType
    {
        laser,
        rocket,
        COUNT
    }
}

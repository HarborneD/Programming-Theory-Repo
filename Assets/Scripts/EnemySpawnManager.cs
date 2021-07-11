using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject player;

    [SerializeField] List<PathCreator> spawnPaths = new List<PathCreator>();
    [SerializeField] public List<PathCreator> rocketPaths = new List<PathCreator>();

    [SerializeField] GameObject laserEnemeyPrefab;
    [SerializeField] GameObject rocketEnemeyPrefab;

    [SerializeField] int numLoopsCompletedToIncreaseEnemyCount = 5;
    [SerializeField] float spawnRocketChance = 0.3f;

    Dictionary<EnemyType, GameObject> enemyTypePrefabLookup = new Dictionary<EnemyType, GameObject>();
        
    // Start is called before the first frame update
    void Start()
    {
        enemyTypePrefabLookup.Add(EnemyType.laser, laserEnemeyPrefab);
        enemyTypePrefabLookup.Add(EnemyType.rocket, rocketEnemeyPrefab);

        //SpawnRandomEnemyOnRandomPath();

        //SpawnEnemy(0, laserEnemeyPrefab);
        //SpawnEnemy(3, rocketEnemeyPrefab);
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveToPlayerTransform();

        if(gameManager.playerAlive && GameObject.FindObjectsOfType<Enemy>().Count() == 0)
        {
            SpawnBasedOnLoopCount();
        }
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
        enemyObject.GetComponent<Enemy>().spawnManager = this;
        enemyObject.GetComponent<Enemy>().gameManager = gameManager;
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

    void SpawnBasedOnLoopCount()
    {
        int maxEnemies = Mathf.Min(4, Mathf.FloorToInt(gameManager.loop / numLoopsCompletedToIncreaseEnemyCount) + 1);
        int maxRockets = Mathf.Min(4, gameManager.loop % 5);
        Debug.Log(maxEnemies + " " + maxRockets);
        List<int> availablePaths = new List<int>();
        availablePaths.AddRange(Enumerable.Range(0, spawnPaths.Count));

        for (int enemeyIndex = 0; enemeyIndex < maxEnemies; enemeyIndex++)
        {
            int pathIndex = Random.Range(0, availablePaths.Count);
            availablePaths.Remove(pathIndex);

            int numRocketSpawns = 0;
            float spawnRocketRoll = Random.Range(0f, 1f);
            Debug.Log(spawnRocketRoll);
            if (numRocketSpawns < maxRockets && spawnRocketRoll < spawnRocketChance)
            {
                SpawnEnemy(pathIndex, rocketEnemeyPrefab);
            }
            else
            {
                SpawnEnemy(pathIndex, laserEnemeyPrefab);
            }
        }

    }



    enum EnemyType
    {
        laser,
        rocket,
        COUNT
    }
}

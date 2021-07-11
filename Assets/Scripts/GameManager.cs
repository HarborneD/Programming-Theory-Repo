using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score { get; private set; }
    public int loop { get; private set; } = 1;

    [SerializeField] Player player;
    [SerializeField] EnemySpawnManager spawnManager;

    [SerializeField] Text loopDisplayText;
    [SerializeField] Text scoreDisplayText;
    [SerializeField] GameObject gameOverUi;

    public bool playerAlive { get; private set; } = true;


    // Start is called before the first frame update
    void Start()
    {
        player.gameObject.GetComponent<PathFollower>().endOfPath.AddListener(IncrementLoopCount);
        player.deathEvent.AddListener(HandlePlayerDeath);
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerAlive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleRestart();
        }
    }

    void IncrementLoopCount()
    {
        loop++;
        loopDisplayText.text = loop.ToString();
    }

    public void AddToScore(int scoreAdjustment)
    {
        score += scoreAdjustment;
        scoreDisplayText.text = score.ToString();
    }

    void HandlePlayerDeath()
    {
        gameOverUi.SetActive(true);
        playerAlive = false;
    }

    void HandleRestart()
    {
        SceneManager.LoadScene(1);
    }
}

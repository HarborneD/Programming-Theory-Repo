using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int score { get; private set; }
    public int loop { get; private set; } = 1;

    [SerializeField] Player player;
    [SerializeField] EnemySpawnManager spawnManager;

    [SerializeField] Text loopDisplayText;
    [SerializeField] Text scoreDisplayText;


    // Start is called before the first frame update
    void Start()
    {
        player.gameObject.GetComponent<PathFollower>().endOfPath.AddListener(IncrementLoopCount);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

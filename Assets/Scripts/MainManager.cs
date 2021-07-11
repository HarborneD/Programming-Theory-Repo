using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    bool hasStarted = false;
    public MainManager Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted && Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        hasStarted = true;
        SceneManager.LoadScene(1);
    }
}

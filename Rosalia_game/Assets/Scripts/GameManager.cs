using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AudioSource audio;
    EnemySpawnManager myEnemyManager;
    MoneyRespawnManager myMoneyRespawnManager;
    GameObject Player;
    GameObject[] fardosInGame;
    Coroutine myCoroutine;
    public GameObject textTochange;
    public GameObject MyCanvas;
    MoneyManager myMoneyManager;

    int numRound = 0;
    bool newRound = false;
    bool playing = true;

    // Start is called before the first frame update
    void Start()
    {
        myEnemyManager = this.GetComponent<EnemySpawnManager>();
        myMoneyRespawnManager = this.GetComponent<MoneyRespawnManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
        //StartNewRound();
        Invoke("StartNewRound", 3);
        myMoneyManager = GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<MoneyManager>();
        textTochange.GetComponent<Text>().text = myMoneyManager.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            if (fardosInGame != null)
            {
                if (!newRound)
                {
                    if (fardosInGame.Length <= 0)
                    {
                        newRound = true;
                        StartNewRound();
                    }
                }
                else
                {
                    if (fardosInGame.Length > 0)
                    {
                        newRound = false;
                    }
                }
            }
            if (!audio.isPlaying)
            {
                GameOver();
            }
        }
    }

    void StartNewRound()
    {
        numRound++;
        myEnemyManager.newRound(numRound);
        myMoneyRespawnManager.newRound(numRound);
        myCoroutine = StartCoroutine(checkForNewRound());
    }

    public void GameOver()
    {
        playing = false;
        textTochange.GetComponent<Text>().text = myMoneyManager.GetScore().ToString();
        MyCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator checkForNewRound()
    {
        while (Player!=null)
        {
            fardosInGame= GameObject.FindGameObjectsWithTag("Fardo");
            yield return new WaitForSeconds(2);
        }
    }
}

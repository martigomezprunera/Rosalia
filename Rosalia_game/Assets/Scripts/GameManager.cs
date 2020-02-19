using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EnemySpawnManager myEnemyManager;
    MoneyRespawnManager myMoneyRespawnManager;
    GameObject Player;
    GameObject[] fardosInGame;
    Coroutine myCoroutine;
    int numRound = 0;
    bool newRound = false;
    // Start is called before the first frame update
    void Start()
    {
        myEnemyManager = this.GetComponent<EnemySpawnManager>();
        myMoneyRespawnManager = this.GetComponent<MoneyRespawnManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        StartNewRound();
    }

    // Update is called once per frame
    void Update()
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
    void StartNewRound()
    {
        numRound++;
        myEnemyManager.newRound(numRound);
        myMoneyRespawnManager.newRound(numRound);
        myCoroutine = StartCoroutine(checkForNewRound());
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

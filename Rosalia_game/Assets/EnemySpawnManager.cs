using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject NormalEnemy;
    public GameObject FatEnemy;
    public GameObject LittleEnemy;

    int costeRonda=0;
    int dificultad = 2;
    int EnemigoPequeño = 1;
    int EnemigoNormal = 2;
    int EnemigoGordo=4;

    float refreshRate = 0.1f;
    // Coste ronda = 16 + (k*numronda)

    public GameObject[] respawns;
    // Start is called before the first frame update
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("PossibleSpawnEnemy");
        newRound(1);
    }

    void newRound(int numRound)
    {
        costeRonda = 16 + (numRound * dificultad);
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        int aux = Random.Range(1, 3);
        
        switch (aux)
        {
            case 1:
                Debug.Log("Se instancia enemigo 1");
                break;
            case 2:
                Debug.Log("Se instancia enemigo 2");
                break;
            case 3:
                Debug.Log("Se instancia enemigo 3");
                break;
            default:
                break;
        }

        //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);

        yield return new WaitForSeconds(2);
    }
}

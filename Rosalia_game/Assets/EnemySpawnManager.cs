using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject NormalEnemy;
    public GameObject FatEnemy;
    public GameObject LittleEnemy;

    int costeRonda=0;
    [SerializeField] int dificultad = 2;

    [SerializeField] int CosteEnemigoPequeño = 1;
    [SerializeField] int CosteEnemigoNormal = 2;
    [SerializeField] int CosteEnemigoGordo = 4;

    [SerializeField] float refreshRate = 4f;
    // Coste ronda = 16 + (k*numronda)

    public GameObject[] respawns;
    // Start is called before the first frame update
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("PossibleSpawnEnemy");
        newRound(3);
    }

    void newRound(int numRound)
    {
        costeRonda = 10 + (numRound * dificultad);
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        while (costeRonda > 0)
        {
            int aux = Random.Range(1, 3);
            int auxRandomPoint = Random.Range(0, 8);
            Enemy myEnem;
            switch (aux)
            {
                case 1:                    
                    myEnem = Instantiate(NormalEnemy, respawns[auxRandomPoint].transform.position, respawns[auxRandomPoint].transform.rotation).GetComponent<Enemy>();
                    costeRonda -= CosteEnemigoNormal;
                    if (Random.Range(0, 2) % 2 == 0)
                        myEnem.myType = Enemy.TypeOfEnemy.CARGA;
                    else
                        myEnem.myType = Enemy.TypeOfEnemy.NORMAL;
                    break;
                case 2:
                    myEnem = Instantiate(LittleEnemy, respawns[auxRandomPoint].transform.position, respawns[auxRandomPoint].transform.rotation).GetComponent<Enemy>();
                    costeRonda -= CosteEnemigoPequeño;
                    if (Random.Range(0, 2) % 2 == 0)
                        myEnem.myType = Enemy.TypeOfEnemy.CARGA;
                    else
                        myEnem.myType = Enemy.TypeOfEnemy.NORMAL;
                    break;
                case 3:
                    Instantiate(FatEnemy, respawns[auxRandomPoint].transform.position, respawns[auxRandomPoint].transform.rotation);
                    costeRonda -= CosteEnemigoGordo;
                    break;
                default:
                    break;
            }

            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
            Debug.Log("Coste actual = "+costeRonda);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject NormalEnemy;
    public GameObject FatEnemy;
    public GameObject LittleEnemy;

    int costeRonda=0;
    Coroutine myCoroutine;
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
    }

    
    public void newRound(int numRound)
    {
        if (myCoroutine != null)
            StopCoroutine(myCoroutine);
        costeRonda = 10 + (numRound * dificultad);
        myCoroutine = StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        while (costeRonda > 0)
        {
            int aux = Random.Range(1, 4);
            int auxRandomPoint = Random.Range(0, 8);
            Enemy myEnem;
            if (respawns.Length > 0)
            {
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
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}

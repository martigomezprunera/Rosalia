using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRespawnManager : MonoBehaviour
{
    //VARIABLES
    GameObject[] possibleSpawnSites;
    private int maxNumbers = 10;
    private List<int> uniqueNumbers;
    private List<int> finishedNumbers;

    [SerializeField]
    GameObject fajoDinero;

    // Start is called before the first frame update
    void Start()
    {
        uniqueNumbers = new List<int>();
        finishedNumbers = new List<int>();

        possibleSpawnSites = GameObject.FindGameObjectsWithTag("PossibleSpawnMoney");
        //LLAMADA FUNCIÓN RANDOM
        GenerateRandomList();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Numero de posibles sitios a spawnear" + possibleSpawnSites.Length);

        /*for(int i = 0; i < finishedNumbers.Count; i++)
        {
            Debug.Log("Numero del array: " + finishedNumbers[i]);
        }*/
    }

    //RANDOM ARRAY
    public void GenerateRandomList()
    {
        for (int i = 0; i < 28; i++)
        {
            uniqueNumbers.Add(i);
        }
        for (int i = 0; i < maxNumbers; i++)
        {
            int ranNum = uniqueNumbers[Random.Range(1, uniqueNumbers.Count)];
            finishedNumbers.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
    }

    public void newRound(int numRound)
    {
        //COMPROBACIONES
        Debug.Log("Numero de posibles sitios a spawnear " + possibleSpawnSites.Length);
        Debug.Log("Numeros dentro del array: " + finishedNumbers.Count);

        for (int i = 0; i < finishedNumbers.Count; i++)
        {
            Instantiate(fajoDinero, possibleSpawnSites[finishedNumbers[i]].transform.position, Quaternion.identity);
        }
        Debug.Log("Spawn dinero de la ronda:" + numRound);
    }

}


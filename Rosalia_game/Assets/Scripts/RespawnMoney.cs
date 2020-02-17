using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMoney : MonoBehaviour
{

    //VARIABLES
    public GameObject[] targetPoints;
    public GameObject money;
    
    
    // Start is called before the first frame update
    void Start()
    {
        targetPoints[0] = Instantiate(money, targetPoints[0].transform.position, Quaternion.identity);
    }

    //COLLISION DE OBJETO CON PERSONAJE


    // Update is called once per frame
    void Update()
    {

    }
}

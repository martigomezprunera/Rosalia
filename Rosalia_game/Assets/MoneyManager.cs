using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    float moneyScore;
    [SerializeField] PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(float currency)
    {
        moneyScore += (currency * player.GetMultipliyer());
        Debug.Log("Current Money:    " + moneyScore);
    }

    public float GetScore()
    {
        return moneyScore;
    }
}

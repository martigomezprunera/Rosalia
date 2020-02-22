using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    float moneyScore;
    [SerializeField] PlayerMovement player;
    [SerializeField] Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "F***ING MONEY:   " + moneyScore;
    }

    public void AddMoney(float currency)
    {
        moneyScore += (currency * player.GetMultipliyer());
    }

    public float GetScore()
    {
        return moneyScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyLogic : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GameObject moneyParticles;
    [SerializeField] Transform player;
    [SerializeField] float lifeTime;
    [SerializeField] float speedMoney;
    float timer;

    #endregion

    void Start()
    {
        timer = 0;
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            this.transform.DOMove(new Vector3(player.position.x - 1.5f, player.position.y, player.position.z) , speedMoney);
        }

    }

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddMoney();

            Destroy(Instantiate(moneyParticles, this.transform.position, Quaternion.identity), 2f);
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region ADD MONEY
    void AddMoney()
    {
        ///LLAMAR A LA FUNCION DE GAME MANAGER PARA  AÑADIR DINERO
        Debug.Log("DINERO CONSEGUIDO");
    }
    #endregion
}

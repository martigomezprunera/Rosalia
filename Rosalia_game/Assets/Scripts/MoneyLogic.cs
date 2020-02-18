using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyLogic : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GameObject moneyParticles;
    Transform player;
    [SerializeField] float lifeTime;
    [SerializeField] float speedMoney;
    [SerializeField] float currency;
    MoneyManager moneManager;

    bool Idle = true;
    [Header("ANIMATION")]
    [SerializeField] float timeFloating;
    [SerializeField] float distanceFloating;
    float timer;

    #endregion

    #region START
    void Start()
    {
        timer = 0;
        player = GameObject.Find("Rosalia").transform;
        GoUp();
        moneManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }
    #endregion

    #region UPDATE
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Idle = false;
            this.transform.DOMove(new Vector3(player.position.x - 1.5f, player.position.y, player.position.z) , speedMoney);
        }

    }
    #endregion

    #region GO UP
    void GoUp()
    {
        if (Idle)
        {
            this.transform.DOMoveY(this.transform.position.y + distanceFloating, timeFloating);
            Invoke("GoDown", timeFloating);
        }        
    }
    #endregion

    #region GO DOWN
    void GoDown()
    {
        if (Idle)
        {
            this.transform.DOMoveY(this.transform.position.y - distanceFloating, timeFloating);
            Invoke("GoUp", timeFloating);
        }
    }
    #endregion

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
        moneManager.AddMoney(currency);
    }
    #endregion
}

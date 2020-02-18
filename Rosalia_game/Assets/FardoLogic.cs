using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FardoLogic : MonoBehaviour
{
    #region VARIABLES
    [Header("SPAWNERS")]
    [SerializeField] Transform spawner1;
    [SerializeField] Transform spawner2;
    [SerializeField] Transform spawner3;
    [SerializeField] Transform spawner4;

    [Header("PREFAB")]
    [SerializeField] GameObject fajo;

    [Header("Variables")]
    [SerializeField] float speedSpawn;
    [SerializeField] float jumpSpawn;

    GameObject fajoSpawned1;
    GameObject fajoSpawned2;
    GameObject fajoSpawned3;
    GameObject fajoSpawned4;

    PlayerMovement player;

    int life = 3;
    #endregion


    #region START
    private void Start()
    {
        player = GameObject.Find("Rosalia").GetComponent<PlayerMovement>();
    }
    #endregion

    #region HIT FARDO
    public void HitFardo()
    {
        life--;

        if (life == 2)
        {
            fajoSpawned1 = Instantiate(fajo,this.transform.position, Quaternion.identity) as GameObject;
            fajoSpawned1.transform.DOJump(spawner1.position, jumpSpawn,1,speedSpawn);
        }
        else if (life == 1)
        {
            fajoSpawned2 = Instantiate(fajo, this.transform.position, Quaternion.identity) as GameObject;
            fajoSpawned2.transform.DOJump(spawner2.position, jumpSpawn, 1, speedSpawn);
        }
        else if (life == 0)
        {
            fajoSpawned3 = Instantiate(fajo, this.transform.position, Quaternion.identity) as GameObject;
            fajoSpawned3.transform.DOJump(spawner3.position, jumpSpawn, 1, speedSpawn);

            fajoSpawned4 = Instantiate(fajo, this.transform.position, Quaternion.identity) as GameObject;
            fajoSpawned4.transform.DOJump(spawner4.position, jumpSpawn, 1, speedSpawn);

            player.IncreaseMultipliyer();

            Destroy(this.gameObject);
        }
    }
    #endregion
}

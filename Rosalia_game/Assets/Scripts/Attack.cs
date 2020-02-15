using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    public enum Side
    {
        RIGHT,
        LEFT
    }

    [Header("TRANSFORMS")]
    [SerializeField] Transform player;
    [SerializeField] Transform centreOfAttack;
    [SerializeField] Transform rightSpawner;
    [SerializeField] Transform leftSpawner;

    [SerializeField] PlayerMovement myPlayerMovement;

    
    [Header("VARIABLES")]    
    [SerializeField] GameObject attackObject;
    [SerializeField] float speedAttack;
    Quaternion lastRotation;
    float angleToRotate = 120;
    Side sideAttack = Side.RIGHT;
    bool canAttack = true;


    #region UPDATE
    void Update()
    {      
        //////Detectamos el input
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            switch (sideAttack)
            {
                case Side.RIGHT:
                    {
                        ////Instanciar el ataque
                        Debug.Log("Right Attack");
                        Destroy(Instantiate(attackObject,rightSpawner), speedAttack);
                        attackObject.transform.SetParent(rightSpawner);

                        ///rotar el brazo
                        ///
                        lastRotation = centreOfAttack.rotation;
                        centreOfAttack.DORotate(new Vector3(    centreOfAttack.rotation.eulerAngles.x, 
                                                                centreOfAttack.rotation.eulerAngles.y + angleToRotate, 
                                                                centreOfAttack.rotation.eulerAngles.z), 
                                                                speedAttack);

                        ///coolDown
                        canAttack = false;
                        Invoke("RestartAttack", speedAttack);

                        ///Cambiar brazo
                        sideAttack = Side.LEFT;
                        break;
                    }
                case Side.LEFT:
                    {
                        ////Instanciar el ataque
                        Debug.Log("Left Attack");
                        Destroy(Instantiate(attackObject, leftSpawner), speedAttack);
                        attackObject.transform.parent = leftSpawner;

                        ///rotar el brazo
                        centreOfAttack.DORotate(new Vector3(centreOfAttack.rotation.eulerAngles.x,
                                                                centreOfAttack.rotation.eulerAngles.y - angleToRotate,
                                                                centreOfAttack.rotation.eulerAngles.z),
                                                                speedAttack);

                        ///coolDown
                        canAttack = false;
                        Invoke("RestartAttack", speedAttack+0.2f);

                        ///Cambiar brazo
                        sideAttack = Side.RIGHT;
                        break;
                    }
                default:
                    {
                        Debug.Log("ALGO NO FUNCIONA");
                        break;
                    }
            }
        }
    }
    #endregion

    #region RESTART ATTACK
    void RestartAttack()
    {
        canAttack = true;
        centreOfAttack.DORotate(player.rotation.eulerAngles,0.1f);
        //centreOfAttack.rotation = lastRotation;
    }
    #endregion
   

}

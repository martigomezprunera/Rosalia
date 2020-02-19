using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    int lifes=3;

    [Header("MOVEMENT")]
    float horizontalMove;
    float verticalMove;
    private Vector3 playerInput;
    public Vector3 movePlayer;
    private bool stopped = false;

    [SerializeField] CharacterController player;
    [SerializeField] float playerSpeed;

    [SerializeField] Transform centre;


    [Header("GRAVITY")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float fallVelocity;

    [Header("CAMERA")]
    [SerializeField] Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    [Header("RESPAWNMONEY")]
    private int counterMoneyTake;
    private int counterMoney;

    [Header("PARTICLES")]
    [SerializeField] GameObject deathParticles;
    [SerializeField] GameObject walkParticles;
    [SerializeField] Transform spawnerWalkingParticles;
    float walkingTimer;
    [SerializeField] float maxTimeWalking;

    [Header("MULTIPLIYER")]
    [SerializeField] float multipliyerFactor;
    float multipliyer = 1f;
    bool onCombo = false;
    [SerializeField] float maxTimeCombo;
    float timeCombo = 0;
    [SerializeField] Text multiplyText;
    #endregion

    #region UPDATE
    void Update()
    {
        ///TEXT 
        multiplyText.text = "x" + multipliyer;

        //GET AXIS
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        //GETTING CAMERA DIRECTION
        CamDirection();

        // CALCULATING CHARACTER MOVEMENT
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer *= playerSpeed;

        ///CHECKING IF IS STOPPED   
        if (movePlayer == new Vector3(0, 0, 0))
        {
            stopped = true;
        }
        else
        {
            stopped = false;
        }


        if (!stopped)
        {          

            // GRAVITY
            SetGravity();

            // MOVING CHARACTER
            player.Move(movePlayer * Time.deltaTime);

            walkingTimer += Time.deltaTime;

            if (walkingTimer > maxTimeWalking)
            {
                walkingTimer = 0;
                Destroy(Instantiate(walkParticles, spawnerWalkingParticles.position, Quaternion.identity ), 0.5f);
            }

        }

        ////CHECKING IF THE COMBO CONTINUES
        if (onCombo)
        {
            timeCombo += Time.deltaTime;

            if (timeCombo >= maxTimeCombo)
            {
                RestartMultipliyer();
            }
        }


        /// CHARACTER ROTATION (LOOK AT)
        //player.transform.LookAt(player.transform.position + movePlayer);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            point.y += 4.75f;
            player.transform.LookAt(point);
        }        
    }
#endregion

    #region CAMERA DIRECTION
    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    #endregion

    #region SET GRAVITY
    void SetGravity()
    {

        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }

    }
    #endregion

    #region OnTriggerStay
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Fardo")
        {
            counterMoneyTake++;
            counterMoney++;
            //Debug.Log("Dinero cogido" + counterMoneyTake);
            //Debug.Log("Fardo siguiente" + counterMoney);
        }
    }
    #endregion

    #region HITTED
    public void Hitted()
    {
        lifes--;
        if (lifes <= 0)
        {
            DeathPlayer();
        }
    }
    #endregion

    #region DEATH
    void DeathPlayer()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #region INCREASE MULTIPLIYER
    public void IncreaseMultipliyer()
    {
        multipliyer += multipliyerFactor;
        timeCombo = 0;
        onCombo = true;
        Debug.Log("Increasing Multipliyer");
        Debug.Log("COMBO =    x" + multipliyer);
    }
    #endregion

    #region RESTART TIME COMBO
    public void RestartTimeCombo()
    {
        timeCombo = 0;
    }
    #endregion

    #region GET MULTIPLIYER
    public float GetMultipliyer()
    {
        return multipliyer;
    }
    #endregion

    #region RESTART MULTIPLAYER
    public void RestartMultipliyer()
    {
        multipliyer = 1;
        timeCombo = 0;
        onCombo = false;
        Debug.Log("RESTARTING COMBO" );
    }
    #endregion

}

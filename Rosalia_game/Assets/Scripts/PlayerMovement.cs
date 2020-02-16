using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("MOVEMENT")]
    float horizontalMove;
    float verticalMove;
    private Vector3 playerInput;
    public Vector3 movePlayer;
    private bool stopped = false;

    [SerializeField] CharacterController player;
    [SerializeField] float playerSpeed;


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

    #endregion

    #region UPDATE
    void Update()
    {
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

            if (!stopped)
            {
                // CHARACTER ROTATION (LOOK AT)
                player.transform.LookAt(player.transform.position + movePlayer);

                // GRAVITY
                SetGravity();

                // MOVING CHARACTER
                player.Move(movePlayer * Time.deltaTime);

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
            Debug.Log("Dinero cogido" + counterMoneyTake);
            Debug.Log("Fardo siguiente" + counterMoney);
        }
    }
    #endregion

}

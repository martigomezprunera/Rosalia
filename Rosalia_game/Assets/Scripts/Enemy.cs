using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public enum TypeOfEnemy {NORMAL, CARGA};

    public TypeOfEnemy myType;

    public float distanceCanCharge = 5;

    NavMeshAgent pathfinder;

    Transform target;
    Vector3 auxTarget;
    Rigidbody myRigidBody;
    bool stop = false;
    bool reset = false;
    [SerializeField]
    float countdownTime = 0.5f;
    [SerializeField]
    int distanceToChase  = 10;
    float countDownToAttack ;
    float countDownToReset = 0.5f;
    Coroutine myCoroutine;
    [SerializeField]
    int vida = 2;
    bool chase = false;

    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        myRigidBody = GetComponent<Rigidbody>();
        if(GameObject.FindGameObjectWithTag("Player")!=null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        countDownToAttack = countdownTime;

        player = GameObject.Find("Rosalia").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {
            if (myType == TypeOfEnemy.CARGA)
            {
                if (reset)
                {
                    countDownToReset -= Time.deltaTime;
                    if (countDownToReset < 0)
                    {
                        reset = false;
                        countDownToReset = 0.5f;
                        if (!(Vector3.Distance(target.position, this.transform.position) < 3))
                        {
                            pathfinder.isStopped = false;
                            myCoroutine = StartCoroutine(UpdatePath());
                        }
                        else
                        {
                            stop = true;
                            auxTarget = target.position;
                        }
                    }
                }
                else if ((Vector3.Distance(target.position, this.transform.position) < distanceCanCharge) && !stop)
                {
                    StopCoroutine(myCoroutine);
                    stop = true;
                    pathfinder.isStopped = true;
                    auxTarget = target.position;
                }
                else if (stop)
                {
                    countDownToAttack -= Time.deltaTime;
                    if (countDownToAttack < 0)
                    {
                        ChargeAttack();
                        countDownToAttack = countdownTime;
                        stop = false;
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(target.position, this.transform.position) < distanceToChase)
            {
                chase = true;
                myCoroutine = StartCoroutine(UpdatePath());
            }
        }
    }  

    void ChargeAttack()
    {       
        this.transform.DOJump(auxTarget, 2, 1, 0.2f,false);
        reset = true;        
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!stop)
            {
                pathfinder.SetDestination(targetPosition);
                yield return new WaitForSeconds(refreshRate);
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement myPlayer = other.GetComponent<PlayerMovement>();
            myPlayer.Hitted();
        }
    }
  

    public void Hitted()
    {
        vida--;
        if (vida <= 0)
        {
            player.IncreaseMultipliyer();
            Destroy(this.gameObject);
        }
    }
}

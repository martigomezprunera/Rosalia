using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public enum TypeOfEnemy {NORMAL, CARGA};

    public TypeOfEnemy myType;

    NavMeshAgent pathfinder;

    Transform target;
    Rigidbody myRigidBody;
    bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        myRigidBody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        if (myType == TypeOfEnemy.CARGA)
        {
            if (Vector3.Distance(target.position, this.transform.position) < 6)
            {
                StopAllCoroutines();
                stop = true;
                Debug.Log("dede");
            }
        }
    }

    void ChargeAttack()
    {
        Vector3 direction = target.position-this.transform.position;
        direction.Normalize();
        myRigidBody.AddForce(direction*10);
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
}

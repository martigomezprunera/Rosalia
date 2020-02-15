using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            rb.AddForce(new Vector3(0, force, 0));

            Debug.Log("Atacando");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float pushForce;
    [SerializeField] GameObject hitParticles;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ////PRIMERA ITERACIÓN ATAQUE
            // Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            // rb.AddForce(new Vector3(0, force, 0));
            // Debug.Log("Atacando");

            Vector3 direction =  other.transform.position - this.transform.position ;
            direction *= pushForce;
            direction.y = 0;

            //PARTICLES SYSTEM HIT
            Destroy(Instantiate(hitParticles,other.transform.position, Quaternion.identity), 1.5f);

            //MOVER AL ENEMIGO HACIA ATRÁS
            other.transform.DOJump(other.transform.position + direction, 1.5f, 1, 1);

            //Le haces daño
            Enemy attacked = other.GetComponent<Enemy>();
            //attacked.Hitted();
        }
    }
}

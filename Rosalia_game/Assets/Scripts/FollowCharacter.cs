using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowCharacter : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform mainCamera;
    [SerializeField] float speed;
    [SerializeField] float maxDistance;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera.LookAt(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.position) >= maxDistance)
        {
            this.transform.DOMove(player.position, speed);
        }
        
    }
}

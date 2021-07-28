using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 0.05f;

    bool isLooking;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        //if (isLooking)
        //else
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
    }
}
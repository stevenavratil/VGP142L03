using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerInstance;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Respawn()
    {
        playerInstance.transform.position = spawnLocation.position;
    }
}
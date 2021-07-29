using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Destroy(collider.gameObject);
            Debug.Log("You have Died!");
            Invoke("PlayerDead", 0f);
        }
    }

    public void PlayerDead()
    {
        SceneManager.LoadScene("Crypt");
    }
}
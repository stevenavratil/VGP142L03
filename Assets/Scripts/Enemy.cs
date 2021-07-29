using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 0.05f;
    [SerializeField] Material fadeMaterial;
    [SerializeField] Material[] booMaterial;

    [Header("Weapon Settings")]
    public float projectileForce;
    public float projectileFireRate;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;
    float timeSinceLastFire = 0f;
    bool canFire = false;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dot = Vector3.Dot(player.transform.forward, (transform.position - player.transform.position).normalized);

        // points towards the player
        transform.LookAt(player.transform.position);

        if (dot >= 0.9f)
        {
            foreach (Material mat in booMaterial)
            {
                Color color = mat.color;
                color.a = 1f;
                mat.color = color;
                //Debug.Log("I'm visable!");
            }
            canFire = true;
        }
        else
        {
            foreach (Material mat in booMaterial)
            {
                Color color = mat.color;
                color.a = 0f;
                mat.color = color;
                //Debug.Log("Can't See Me!");
            }
            canFire = false;
            
            // moves towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
        }

        if (Time.time >= timeSinceLastFire + projectileFireRate)
        {
            if (canFire)
            {
                timeSinceLastFire = Time.time;
                fire();
            }
        }
    }

    public void fire()
    {
        Debug.Log("Incoming Attack from Boo!");

        if (projectileSpawnPoint && projectilePrefab)
        {
            // Make the Projectile
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // Shoot Projectile
            temp.AddForce(Camera.main.transform.forward * -projectileForce, ForceMode.Impulse);

            // Destroy Projectile after 2.0s
            Destroy(temp.gameObject, 2.0f);
        }
    }

}
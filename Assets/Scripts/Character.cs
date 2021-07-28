using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    CharacterController controller;

    [Header("PlayerSettings")]
    [Space(2)]
    [Tooltip("speed value must be between 1 and 6.")]
    [Range(1.0f, 6.0f)]
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;

    Vector3 moveDirection;

    bool showLogs = false;

    enum ControllerType { SimpleMove, Move };
    [SerializeField] ControllerType type;

    [Header("Weapon Settings")]
    public float projectileForce;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();
            controller.minMoveDistance = 0.0f;

            if (speed <= 0)
            {
                speed = 6.0f;

                Debug.LogWarning(name + ": speed not set. Defaulting to " + speed);
            }

            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;

                Debug.LogWarning(name + ": jumpSpeed not set. Defaulting to " + jumpSpeed);
            }

            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;

                Debug.LogWarning(name + ": roationSpeed not set. Defaulting to " + rotationSpeed);
            }

            if (gravity <= 0)
            {
                gravity = 9.81f;

                Debug.LogWarning(name + ": gravity not set. Defaulting to " + gravity);
            }

            if (projectileForce <= 0)
            {
                projectileForce = 10.0f;

                Debug.LogWarning(name + ": projectileForce not set. Defaulting to " + projectileForce);
            }

            if (!projectilePrefab)
                Debug.LogWarning(name + "Missing projectilePrefab");

            if (!projectileSpawnPoint)
                Debug.LogWarning(name + "Missing projectileSpawnPoint");

            moveDirection = Vector3.zero;
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        finally
        {
            Debug.LogWarning("Always get called");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ControllerType.SimpleMove:

                //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

                controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                break;

            case ControllerType.Move:

                if (controller.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    moveDirection *= speed;

                    moveDirection = transform.TransformDirection(moveDirection);

                    if (Input.GetButtonDown("Jump"))
                        moveDirection.y = jumpSpeed;
                }

                moveDirection.y -= gravity * Time.deltaTime;

                controller.Move(moveDirection * Time.deltaTime);

                break;
        }

        if (Input.GetButtonDown("Fire1"))
            fire();
    }

    public void fire()
    {
        if (showLogs)
            Debug.Log("Pew Pew");

        if (projectileSpawnPoint && projectilePrefab)
        {
            // Make the Projectile
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // Shoot Projectile
            temp.AddForce(Camera.main.transform.forward * projectileForce, ForceMode.Impulse);

            // Destroy Projectile after 2.0s
            //Destroy(temp.gameObject, 2.0f);
        }
    }

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }
}
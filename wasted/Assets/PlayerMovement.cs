using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6f;
    public float JetpackThrust = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float decay = 1f;
    public float percentdecay = 1.02f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Transform playerBody;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("left shift") && !isGrounded)
        {

            //  Vector3 fly = Camera.main.transform.forward * 2 * JetpackThrust * Time.deltaTime;

            //   fly.y = 0;

            velocity.y = Mathf.Sqrt(jumpHeight * -6f * gravity);

            //  controller.Move(fly);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        //controller.Move(move);

        velocity.x = velocity.x + (move.x * (float)0.5);
        velocity.z = velocity.z + (move.z * (float)0.5);


        if (Mathf.Abs(velocity.x) > 0.01)
        {
            velocity.x = velocity.x / percentdecay;

        }
        else
        {
            velocity.x = 0;
        }

        if (Mathf.Abs(velocity.x) > 0.01)
        {
            velocity.y = velocity.y / percentdecay;

        }
        else
        {
            velocity.x = 0;
        }

        if (Mathf.Abs(velocity.z) > 0.01)
        {
            velocity.z = velocity.z / percentdecay;

        }
        else
        {
            velocity.z = 0;
        }




        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
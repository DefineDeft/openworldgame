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

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Transform playerBody;
    public Rigidbody rb;

    

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
       
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("left shift") && !isGrounded)
        {

            Vector3 fly = Camera.main.transform.forward * 2 * JetpackThrust * Time.deltaTime;

            fly.y = 0;

            rb.velocity += Mathf.Sqrt(jumpHeight * -2f * gravity) * transform.up;

            rb.velocity += fly;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Camera.main.transform.right * x + Camera.main.transform.forward * z;

        rb.velocity += move;

       

      //  controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //  velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            rb.velocity += Mathf.Sqrt(jumpHeight * -2f * gravity) * transform.up;
        }
        //rb.velocity += velocity;
        //  velocity.y += gravity * Time.deltaTime;

        //  controller.Move(velocity * Time.deltaTime);

    }
}
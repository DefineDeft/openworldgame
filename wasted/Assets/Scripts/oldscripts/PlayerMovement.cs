using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public AudioClip impact;
    public AudioSource audioSource;

    public float speed;
    public float JetpackThrust;
    public float gravity;
    public float jumpHeight;
    public float jetPackDelay;
    public float speedlimit;

    private bool canFly = true;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;

    public Transform playerBody;
    public Rigidbody rb;



    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();

    }

    private IEnumerator jetpackboost()
    {
        canFly = false;

        Vector3 fly = Camera.main.transform.forward * 2 * JetpackThrust;

        fly.y = Mathf.Abs(fly.y);

        rb.AddForce(fly, ForceMode.Impulse);

        yield return new WaitForSeconds(jetPackDelay);

        canFly = true;

    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("left shift"))
        {

            if (canFly)
            {
                StartCoroutine(jetpackboost());

                //audioSource.PlayOneShot(impact, 0.5f);

                audioSource.Play();
            }

        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Camera.main.transform.right * x + Camera.main.transform.forward * z;

        move.y = 0;

        rb.velocity += move * (Time.deltaTime) * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            
            rb.velocity += Mathf.Sqrt(jumpHeight * -2f * gravity) * transform.up;
        }


    }
}
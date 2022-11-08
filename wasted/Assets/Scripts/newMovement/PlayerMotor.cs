using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    //
    private CharacterController controller;

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

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessMove(Vector2 input)
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = input.x;
        float z = input.y; //used for z movement here (2 dimension vector)

        Vector3 move = Camera.main.transform.right * x + Camera.main.transform.forward * z;

        move.y = 0;

        rb.velocity += move * (Time.deltaTime) * speed;

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

    public void Jetpack(float isActive)
    {
        if (isActive > 0 && canFly)
        {
            canFly = false;
            StartCoroutine(jetpackboost());

            //audioSource.PlayOneShot(impact, 0.5f);

            audioSource.Play();
        }

    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity += Mathf.Sqrt(jumpHeight * -2f * gravity) * transform.up;
        }
    }

}

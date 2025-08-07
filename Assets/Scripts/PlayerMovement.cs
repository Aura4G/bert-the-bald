using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    //Serialize Fields to edit variables like speed and Jump Force in the Editor and set ground materials
    [SerializeField] public float MoveSpeed = 6f;
    [SerializeField] float JumpForce = 6f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask Ground;

    [SerializeField] AudioSource jumpSound;

    public float HorizontalInput;
    public float VerticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");

        if (!GetComponent<PlayerAbilities>().isCharging)
        {
            rb.velocity = new Vector3(HorizontalInput * MoveSpeed, rb.velocity.y, VerticalInput * MoveSpeed);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    //Subroutine to Jump if grounded
    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x , JumpForce, rb.velocity.z);
        jumpSound.Play();
    }

    //Subroutine to check if the Player is on Ground
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, Ground);     
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.parent.gameObject);
            Jump();
            GetComponent<ItemCollector>().score += 500;
        }
    }
}

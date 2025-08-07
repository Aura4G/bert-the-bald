using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] float ChargeMultiplier = 2.5f;
    public bool isCharging = false;

    Rigidbody rigidB;
    PlayerMovement pm;

    [SerializeField] public AudioSource crash;

    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            isCharging = true;

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                rigidB.velocity = new Vector3(0f, rigidB.velocity.y, pm.MoveSpeed * ChargeMultiplier);
            }

            else
            {
                rigidB.velocity = new Vector3(pm.HorizontalInput * ChargeMultiplier * pm.MoveSpeed, rigidB.velocity.y, pm.VerticalInput * ChargeMultiplier * pm.MoveSpeed);
            }
        }

        else
        {
            isCharging = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weak") && isCharging)
        {
            Destroy(collision.gameObject);
            crash.Play();
        }
    }
}

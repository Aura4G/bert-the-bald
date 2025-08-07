using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] float deathPlane = -3f;
    ItemCollector ic;

    bool isDamageable;
    bool isDead;

    [SerializeField] AudioSource fallen;
    [SerializeField] AudioSource hit;
    [SerializeField] AudioSource death;

    // Start is called before the first frame update
    void Start()
    {
        ic = GetComponent<ItemCollector>();
        isDamageable = true;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= deathPlane && !isDead)
        {
            Fallen();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GetComponent<PlayerAbilities>().isCharging)
            {
                Destroy(collision.transform.parent.gameObject);
                GetComponent<ItemCollector>().score += 500;
                GetComponent<PlayerAbilities>().crash.Play();
            }

            else
            {
                if (isDamageable)
                {
                    ic.healthPoints--;
                    if (ic.healthPoints == 0)
                    {
                        Die();
                    }
                    isDamageable = false;
                    Invoke(nameof(triggerDamageable), 3f);
                    hit.Play();
                }
            }
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            if (isDamageable)
            {
                ic.healthPoints--;
                if (ic.healthPoints == 0)
                {
                    Die();
                }
                isDamageable = false;
            }
            isDamageable = false;
            GetComponent<PlayerMovement>().Jump();
            Invoke(nameof(triggerDamageable), 3f);
            hit.Play();
        }
    }

    void triggerDamageable()
    {
        isDamageable = true;
    }

    //Called upon when the player falls into the death plane. The player is still visible and falling, but cannot be controlled.
    void Fallen()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAbilities>().enabled = false;
        isDead = true;
        Invoke(nameof(ReloadLevel), 2f);
        fallen.Play();
    }

    //Called upon when the player is killed by a spike of enemy, disabling controls, movement, and the mesh renderer.
    void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAbilities>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        Invoke(nameof(ReloadLevel), 2f);
        death.Play();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

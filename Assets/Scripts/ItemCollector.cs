using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public int coins;
    public int healthPoints;
    public bool gemGet;
    public int score;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI heartsText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image gemTick;

    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource heartSound;
    [SerializeField] AudioSource gemSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.transform.parent.gameObject);
            coins++;
            score += 100;
            coinSound.Play();
        }

        if (other.gameObject.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            healthPoints++;
            score += 300;
            heartSound.Play();
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            gemGet = true;
            score += 1000;
            gemSound.Play();
        }
    }

    void Start()
    {
        coins = 0;
        healthPoints = 3;
        gemGet = false;
        score = 0;
        gemTick.enabled = false;
    }

    void Update()
    {
        coinsText.text = ": " + coins;
        heartsText.text = ": " + healthPoints;
        scoreText.text = score.ToString();
        
        if (gemGet)
        {
            gemTick.enabled = true;
        }
    }
}

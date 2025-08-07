using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] GameObject LevelEndScreen;

    [SerializeField] TextMeshProUGUI coinResults;
    [SerializeField] TextMeshProUGUI heartResults;
    [SerializeField] TextMeshProUGUI scoreResults;
    [SerializeField] Image gemResult;

    [SerializeField] AudioSource finishJingle;

    ItemCollector ic;

    bool levelComplete;

    void Start()
    {
        ic = GetComponent<ItemCollector>();
        LevelEndScreen.SetActive(false);
        gemResult.enabled = false;
        levelComplete = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal") && !levelComplete)
        {
            notPlaying();
            Invoke(nameof(ShowLevelEndScreen), 2f);
            levelComplete = true;
            finishJingle.Play();
        }
    }

    void notPlaying()
    {
        GetComponent<PlayerAbilities>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void ShowLevelEndScreen()
    {
        GameObject.Find("HUD").SetActive(false);
        LevelEndScreen.SetActive(true);
        coinResults.text = ic.coins.ToString();
        heartResults.text = ic.healthPoints.ToString();
        scoreResults.text = ic.score.ToString();

        if (ic.gemGet)
        {
            gemResult.enabled = true;
        }
    }
}

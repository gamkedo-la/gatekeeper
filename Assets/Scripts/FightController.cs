using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject warningText;
    [SerializeField] private GameObject bossHealthUI;
    private bool isWarningTextActive = false;
    [SerializeField] private int flashCount = 10;
    private bool flashingFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().getPickUpDeaddrop() && !flashingFinished) 
        {
            StartCoroutine("WarningFlash",.5f);
            flashingFinished = true;
        }
    }

    IEnumerator WarningFlash(float duration)
    {
        for(int i = 0; i < flashCount; i++)
        {
            isWarningTextActive = !isWarningTextActive;
            warningText.SetActive(isWarningTextActive);
            yield return new WaitForSeconds(duration);
            
        }

        GetComponent<BossController>().setBossFightStarted(true);
        bossHealthUI.SetActive(true);
    }



}

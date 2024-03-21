using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CivilianController : NpcController
{
    [SerializeField]
    private GameObject scannerPanel;

    [SerializeField]
    private GameObject scannerDiseaseImage;


    [SerializeField]
    private bool isDiseased;

    public void setScannerPanel(GameObject scannerPanel)
    {
        this.scannerPanel = scannerPanel;
    }

    public void setScannerDiseaseImage(GameObject scannerDiseaseImage)
    {
        this.scannerDiseaseImage = scannerDiseaseImage;
    }

    private void OnMouseDown()
    {

        if (scannerPanel.activeSelf)
        {
            scannerPanel.GetComponentInChildren<TMP_Text>().text = "Name: " + npcName;


        }
        else
        {
            scannerPanel.GetComponentInChildren<TMP_Text>().text = "Name: " + npcName;
            scannerPanel.SetActive(true);

        }

        if (isDiseased)
        {
            scannerDiseaseImage.SetActive(true);
        }
        else
        {
            scannerDiseaseImage.SetActive(false);
        }

    }

    void Start()
    {
        int randomNum = Random.Range(0, 3);
        if (randomNum == 1)
        {
            isDiseased = true;
        }
        else
        {
            isDiseased = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveToTransform(movementTarget);
        }
    }
}

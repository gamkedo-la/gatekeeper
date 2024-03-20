using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcController : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    public Transform movementTarget { get; private set; }
    public bool isMoving = true;

    [SerializeField]
    public string npcName { get; set; }

    [SerializeField]
    private GameObject scannerPanel;

    [SerializeField]
    private GameObject scannerDiseaseImage;


    [SerializeField]
    private bool isDiseased;

    public void Start()
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

    public void setScannerPanel(GameObject scannerPanel)
    {
        this.scannerPanel = scannerPanel;
    }

    public void setScannerDiseaseImage(GameObject scannerDiseaseImage)
    {
        this.scannerDiseaseImage = scannerDiseaseImage;
    }

    public void MoveToTransform(Transform target)
    {
        if(movementTarget != target)
        {
            isMoving = true;
            movementTarget = target;
        }

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );

        if (transform.position.x == target.position.x)
        {
            isMoving = false;
        }

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

    void Update()
    {
        if (isMoving)
        {
            MoveToTransform(movementTarget); 
        }
    }
}

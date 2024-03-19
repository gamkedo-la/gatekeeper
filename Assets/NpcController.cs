using System.Collections;
using System.Collections.Generic;
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

    public void setScannerPanel(GameObject scannerPanel)
    {
        this.scannerPanel = scannerPanel;
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
        Debug.Log("active");
        if (scannerPanel.activeSelf)
        {
            scannerPanel.SetActive(false);
        }
        else
        {
            scannerPanel.GetComponentInChildren<TMP_Text>().text = "Name: " + npcName;
            scannerPanel.SetActive(true);
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

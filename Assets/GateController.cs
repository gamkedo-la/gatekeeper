using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GateController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI uiGoldAmount;
    [SerializeField]
    private TextMeshProUGUI taxAmount;
    private int goldAmount = 0;
    private bool peasantAccepted = false;
    [SerializeField]
    private Transform finalPoint;
    [SerializeField]
    private Transform gatepoint;

    void Start()
    {
        //why does this need to be enabled before the game starts
        taxAmount.enabled = false;
    }

    void Update()
    {

        uiGoldAmount.text = "Gold: " + goldAmount.ToString();
        if (Input.GetButtonDown("Jump"))
        {
            peasantAccepted = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Peasant")
        {
            collision.gameObject.GetComponent<PeasantController>().movementTarget = gatepoint;
            taxAmount.text = "Tax: " + collision.gameObject.GetComponent<PeasantController>().taxValue;
            taxAmount.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (peasantAccepted)
        {
            goldAmount += collision.gameObject.GetComponent<PeasantController>().taxValue;
            collision.gameObject.GetComponent<PeasantController>().movementTarget = finalPoint;
            collision.gameObject.GetComponent<PeasantController>().isMoving = true;
            Debug.Log("Peasant Accepted");
            peasantAccepted = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Peasant")
        {
            taxAmount.enabled = false;
            
        }
    }
}

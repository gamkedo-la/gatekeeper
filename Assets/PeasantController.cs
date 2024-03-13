using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PeasantController : MonoBehaviour
{
    public float speed = 1f;
    public float offsetX = 0f;
    private string name = "John";
    private string nationality = "English";
    private string faction = "Bakers Guild";
    public int taxValue;

    public Transform movementTarget; 
    public bool isMoving = true;


    public void Awake()
    {
        taxValue = Random.Range(1, 38);
    }

    public void MoveToTransform(Transform target)
    {
        if(movementTarget.tag == "Peasant")
        {
            offsetX = 1f;
        }
        else
        {
            offsetX = 0f;
        }

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, target.position.x - offsetX, speed * Time.deltaTime),
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
    }


    void Update()
    {
        if (isMoving)
        {
            MoveToTransform(movementTarget); 
        }
    }
}

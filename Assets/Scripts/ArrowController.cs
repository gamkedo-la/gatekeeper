using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float bobPace = 0.5f;
    [SerializeField] private float bobHeight = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Cos(Time.timeSinceLevelLoad * bobPace) * bobHeight;
    }
}

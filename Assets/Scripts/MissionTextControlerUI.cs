using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionTextControlerUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().getPickUpDeaddrop() == true)
        {
            gameObject.GetComponent <TMP_Text>().text = "Current Mission: SURVIVE!";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoNodeController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] private TMP_Text infoNodeText;
    [SerializeField] private string text;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoNodeText.text = text;
            panel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panel.SetActive(false);
        }
    }
}

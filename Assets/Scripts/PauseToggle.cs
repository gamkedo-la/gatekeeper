using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject player;
    public bool isPaused= false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        // If the game is paused, timeScale is set to 0, else set to 1
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
        {
            player.SetActive(false);
            pausePanel.SetActive(true);
            
        }
        else
        {
            pausePanel.SetActive(false);
            player.SetActive(true);
        }
    }
}

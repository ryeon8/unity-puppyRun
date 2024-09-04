using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;

    private bool isPaused = false;

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        ToggleButton();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        ToggleButton();
    }

    void ToggleButton()
    {
        pauseButton.gameObject.SetActive(!isPaused);
        resumeButton.gameObject.SetActive(isPaused);
    }
}
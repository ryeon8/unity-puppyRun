using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private GameObject countDownPannel;
    [SerializeField]
    private TextMeshProUGUI countDownText;

    private bool isPaused = false;

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        countDownText.gameObject.SetActive(false);
        countDownPannel.SetActive(true);
        ToggleButton();
    }

    public void Resume()
    {
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        countDownText.gameObject.SetActive(true);

        int countDown = 3;
        while (countDown > 0)
        {
            countDownText.SetText((countDown--).ToString());
            yield return new WaitForSecondsRealtime(1);
        }

        isPaused = false;
        Time.timeScale = 1;
        countDownText.gameObject.SetActive(false);
        countDownPannel.SetActive(false);
        ToggleButton();
    }

    void ToggleButton()
    {
        pauseButton.gameObject.SetActive(!isPaused);
        resumeButton.gameObject.SetActive(isPaused);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class FruitTextPair
{
    public GameObject fruit;
    public TextMeshProUGUI countText;
    public int count = 0;
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider healthPointSlider;
    [SerializeField]
    public List<FruitTextPair> fruitTextPairs;
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Canvas gameDoneCanvas;
    [SerializeField]
    private Canvas fadeInOutCanvas;
    [SerializeField]
    private Image fadeInOutImage;

    public static GameManager instance = null;

    private void Awake()
    { // Start보다 먼저 호출됨.
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseFruitCount(GameObject fruit)
    {
        // int fruitIndex = Array.IndexOf(fruits, fruit);
        FruitTextPair fruitTextPair = GetFruitTextPair(fruit);
        if (fruitTextPair != null)
        {
            fruitTextPair.count += 1;
            fruitTextPair.countText.SetText(fruitTextPair.count.ToString());
        }
    }

    FruitTextPair GetFruitTextPair(GameObject fruit)
    {
        // FindIndex도 있음.
        return fruitTextPairs.Find(e => fruit.gameObject.name.StartsWith(e.fruit.name));
    }

    public int GetDamegeFruitIndex()
    {
        return fruitTextPairs.Count - 1; // TODO 과일과 버섯을 구분할 방법이 필요.
    }

    public void SyncHealthPointSlider(int maxValue)
    {
        healthPointSlider.maxValue = maxValue;
    }

    public void UpdateHealthPointSlider(int healthPoint)
    {
        healthPointSlider.value = healthPoint;
    }

    public void GameOvered()
    {
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void GameDone()
    {
        Time.timeScale = 0;
        gameDoneCanvas.gameObject.SetActive(true);
    }

    public IEnumerator FadeIn()
    {
        fadeInOutCanvas.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1f));
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(Fade(0f));
        fadeInOutCanvas.gameObject.SetActive(false);
    }

    IEnumerator Fade(float targetOpacity)
    {
        float fadeDuration = 1f; // fade time, second
        float startOpacity = fadeInOutImage.color.a;
        float elapsed = 0f; // 경과 시간
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, targetOpacity, elapsed / fadeDuration);
            ChangeFadeInOutImageOpacity(newOpacity);
            yield return null;
        }

        ChangeFadeInOutImageOpacity(targetOpacity);
    }

    void ChangeFadeInOutImageOpacity(float opacity)
    {
        Color color = fadeInOutImage.color;
        color.a = opacity;
        fadeInOutImage.color = color;
    }
}

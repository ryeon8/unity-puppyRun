using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
}

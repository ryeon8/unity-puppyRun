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

    public static GameManager instance = null;
    private float timer = 0f;

    private void Awake()
    { // Start보다 먼저 호출됨.
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Debug.Log(fruitTextPairs[0].countText);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            healthPointSlider.value -= 1f;
            timer = 0f;
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public List<FruitTextPair> fruitTextPairs;

    public static GameManager instance = null;

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
        // Debug.Log(fruit.gameObject.name);
        // Debug.Log(fruitTextPairs[0].fruit.name);
        // return fruitTextPairs[0];
    }
}

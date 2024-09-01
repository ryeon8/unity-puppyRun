using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fruits;
    [SerializeField]
    private float spawnIntervalSecond;
    [SerializeField]
    private int maxFruitCount = 50;
    [SerializeField]
    private int jumpHeight;

    private List<List<Tuple<int, int, int>>> coordsList;
    private Tuple<int, int, int> defaultCoord = new Tuple<int, int, int>(20, -5, 0);

    private string spawnFruitRoutineName = "SpawnFruitRoutine";
    private System.Random random = new System.Random();

    void Start()
    {
        InitCoordsList();
        AddRandomCoords();
        ClearRandomCoords();
        StartCoroutine(spawnFruitRoutineName);
    }

    void Update()
    {

    }

    void InitCoordsList()
    {
        coordsList = new List<List<Tuple<int, int, int>>>();

        for (int i = 0; i < maxFruitCount; i++)
        {
            coordsList.Add(new List<Tuple<int, int, int>> { defaultCoord });
        }
    }

    void AddRandomCoords()
    {
        foreach (List<Tuple<int, int, int>> coords in coordsList)
        {
            if (random.Next(0, 10) < 3)
            { // 30% 확률로 새로운 y축 요소 추가.
                coords.Add(new Tuple<int, int, int>(defaultCoord.Item1, defaultCoord.Item2 + jumpHeight, PickRandomFruitIndex()));
            }
        }
    }

    int PickRandomFruitIndex()
    {
        int randomIndex = random.Next(0, 10);
        if (randomIndex < 2)
        {
            return 2;
        }
        else if (randomIndex >= 2 && randomIndex < 5)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    void ClearRandomCoords()
    {
        foreach (List<Tuple<int, int, int>> coords in coordsList)
        {
            if (random.Next(0, 10) < 3)
            {
                coords.Clear();
            }
        }
    }

    IEnumerator SpawnFruitRoutine()
    {
        foreach (List<Tuple<int, int, int>> coords in coordsList)
        {
            SpawnFruit(coords);
            yield return new WaitForSeconds(spawnIntervalSecond);
        }
    }

    void SpawnFruit(List<Tuple<int, int, int>> coords)
    {
        foreach (Tuple<int, int, int> coord in coords)
        {
            // 3번째 인자는 오브젝트의 회전을 설정함. identity: 회전 없음 / 기본 회전값(0,0,0)
            Instantiate(fruits[coord.Item3], new Vector3(coord.Item1, coord.Item2, 0), Quaternion.identity);
        }
    }
}

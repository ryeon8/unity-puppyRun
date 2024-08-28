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

    private List<List<float>> yCoordsList;

    private string spawnFruitRoutineName = "SpawnFruitRoutine";

    void Start()
    {
        InitYCoordsList();
        StartCoroutine(spawnFruitRoutineName);
    }

    void Update()
    {

    }

    void InitYCoordsList()
    {
        yCoordsList = new List<List<float>>();
        // yCoordsList.AddRange(Enumerable.Repeat(new List<float> { -5f }, 50)); // Enumerable.Repeat은 동일 객체를 참조함.
        for (int i = 0; i < maxFruitCount; i++)
        {
            yCoordsList.Add(new List<float> { -5f });
        }

        System.Random random = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            int randomIndex = random.Next(0, yCoordsList.Count); // 마지막 요소는 포함되지 않음. 즉 50은 출력되지 않는다.
            yCoordsList[randomIndex].Add(-1f);
        }
    }

    IEnumerator SpawnFruitRoutine()
    {
        foreach (List<float> yCoords in yCoordsList)
        {
            SpawnFruit(yCoords);
            yield return new WaitForSeconds(spawnIntervalSecond);
        }
    }

    void SpawnFruit(List<float> yCoords)
    {
        foreach (float yCoord in yCoords)
        {
            // 3번째 인자는 오브젝트의 회전을 설정함. identity: 회전 없음 / 기본 회전값(0,0,0)
            Instantiate(fruits[0], new Vector3(transform.position.x, yCoord, 0), Quaternion.identity);
        }
    }
}

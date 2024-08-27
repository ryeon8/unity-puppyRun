using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fruits;
    [SerializeField]
    private float spawnIntervalSecond;

    private string spawnFruitRoutineName = "SpawnFruitRoutine";

    void Start()
    {
        StartCoroutine(spawnFruitRoutineName);
    }

    void Update()
    {

    }

    IEnumerator SpawnFruitRoutine()
    {
        while (true)
        {
            SpawnFruit();
            yield return new WaitForSeconds(spawnIntervalSecond);
        }
    }

    void SpawnFruit()
    {
        Instantiate(fruits[0], transform.position, Quaternion.identity); // 3번째 인자는 오브젝트의 회전을 설정함. identity: 회전 없음 / 기본 회전값(0,0,0)
    }
}

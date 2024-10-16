using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FruitSpawnInfo
{
    public float x;
    public float y;
    public int fruitTextPairIndex;

    public FruitSpawnInfo(float x, float y, int fruitTextPairIndex)
    {
        this.x = x;
        this.y = y;
        this.fruitTextPairIndex = fruitTextPairIndex;
    }
}

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner instance;

    [SerializeField]
    private float spawnIntervalSecond;
    [SerializeField]
    private int maxFruitCount = 50;
    [SerializeField]
    private int jumpHeight;
    [SerializeField]
    private FruitSpawnInfo defaultFruitSpawnInfo;

    private List<List<FruitSpawnInfo>> fruitSpawnInfosList;

    private bool isAllFruitsSpawned = false;
    private DateTime timeLastFruitSpawned;
    private System.Random random = new System.Random();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        bool fruitExistOnScreen = GameObject.FindGameObjectWithTag("Fruit") != null;
        if (!fruitExistOnScreen)
        {
            InitCoordsList();
            AddRandomCoords();
            AddRandomEnemyCoords();
            ClearRandomCoords();
            StartCoroutine(SpawnFruitRoutine());
        }
    }

    void Update()
    {

    }

    void InitCoordsList()
    {
        fruitSpawnInfosList = new List<List<FruitSpawnInfo>>();

        for (int i = 0; i < maxFruitCount; i++)
        {
            fruitSpawnInfosList.Add(new List<FruitSpawnInfo> { defaultFruitSpawnInfo });
        }
    }

    void AddRandomCoords()
    {
        foreach (List<FruitSpawnInfo> spawnInfos in fruitSpawnInfosList)
        {
            if (random.Next(0, 10) < 3)
            { // 30% 확률로 새로운 y축 요소 추가.
                spawnInfos.Add(new FruitSpawnInfo(defaultFruitSpawnInfo.x, defaultFruitSpawnInfo.y + jumpHeight, PickRandomFruitIndex()));
            }
        }
    }

    void AddRandomEnemyCoords()
    {
        foreach (List<FruitSpawnInfo> spawnInfos in fruitSpawnInfosList)
        {
            if (random.Next(0, 10) < 2)
            {
                int index = Mathf.Clamp(random.Next(0, 2), 0, spawnInfos.Count - 1);
                spawnInfos[index] = new FruitSpawnInfo(spawnInfos[index].x, spawnInfos[index].y - 1, GameManager.instance.GetDamegeFruitIndex());
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
        foreach (List<FruitSpawnInfo> fruitSpawnInfos in fruitSpawnInfosList)
        {
            if (random.Next(0, 10) < 3)
            {
                fruitSpawnInfos.Clear();
            }
        }
    }

    IEnumerator SpawnFruitRoutine()
    {
        foreach (List<FruitSpawnInfo> fruitSpawnInfos in fruitSpawnInfosList)
        {
            SpawnFruits(fruitSpawnInfos);
            yield return new WaitForSeconds(spawnIntervalSecond);
        }

        isAllFruitsSpawned = true;
        timeLastFruitSpawned = DateTime.Now;
    }

    void SpawnFruits(List<FruitSpawnInfo> fruitSpawnInfos)
    {
        foreach (FruitSpawnInfo fruitSpawnInfo in fruitSpawnInfos)
        {
            // 3번째 인자는 오브젝트의 회전을 설정함. identity: 회전 없음 / 기본 회전값(0,0,0)
            Instantiate(
                GameManager.instance.fruitTextPairs[fruitSpawnInfo.fruitTextPairIndex].fruit,
                new Vector3(fruitSpawnInfo.x, fruitSpawnInfo.y, 0),
                Quaternion.identity
            );
        }
    }

    public bool IsThereNoMoreFruits()
    {
        if (isAllFruitsSpawned)
        {
            if (timeLastFruitSpawned != null && (DateTime.Now - timeLastFruitSpawned).TotalSeconds >= 5)
            { // 과일이 스폰되지 않은 지 5초가 지났다면 이번 스폰은 끝난 것으로 가정.
                return true;
            }
        }

        return false;
    }

    public void InitSpawner()
    {
        isAllFruitsSpawned = false;
        Start(); // 이래도... 되나?
    }
}

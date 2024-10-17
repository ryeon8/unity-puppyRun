using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int healthPoint;
    [SerializeField]
    private int moveSpeed;

    private int stageLevel = 1;
    private bool onGround = true;
    private int jumpCount = 0;
    private int maxJumpCount = 2;
    private bool hasReachedWorldEnd = false;
    private Vector3 initPosition;
    private int backgroundChangedCount = 0;
    private bool isOnMapChanging = false;

    void Start()
    {
        initPosition = gameObject.transform.position;
        hasReachedWorldEnd = false;
        FruitSpawner.instance.InitSpawner();
        GameManager.instance.SyncHealthPointSlider(healthPoint);
    }

    void Update()
    {
        if (IsJumpable() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (hasReachedWorldEnd)
        {
            if (IsEndJourney())
            {
                GameManager.instance.GameDone();
            }
            else
            {
                isOnMapChanging = true;
                hasReachedWorldEnd = false;
                backgroundChangedCount += 1;
                StartCoroutine(ChangeBackground());
            }
        }
        else if (!isOnMapChanging && FruitSpawner.instance.IsThereNoMoreFruits()) // 맵 페이드 인, 아웃 중에는 캐릭터가 이동하지 않도록.
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    bool IsEndJourney()
    {
        return backgroundChangedCount >= 1;
    }

    IEnumerator ChangeBackground()
    {
        // SceneManager.LoadScene("SecondWorld");

        yield return StartCoroutine(GameManager.instance.FadeIn());
        GameObject[] currentBackgrounds = GameObject.FindGameObjectsWithTag("Background");
        currentBackgrounds.ToList().ForEach(e =>
        {
            foreach (Transform child in e.transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        });
        transform.position = initPosition;
        yield return StartCoroutine(GameManager.instance.FadeOut());
        FruitSpawner.instance.InitSpawner();
    }

    bool IsJumpable()
    {
        return jumpCount < maxJumpCount;
    }

    void Jump()
    {
        onGround = false;
        jumpCount++;
        // impulse: 즉각적이고 짧은 시간 동안 힘이 가해짐. 점프, 총알 발사 등.
        // force: 지속적인 힘. 시간이 지남에 따라 물체에 누적되며 힘을 꾸준히 가하는 상황에서 사용.
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            hasReachedWorldEnd = true;
            stageLevel += 1;
        }
    }

    public void Damaged(int damage)
    {
        healthPoint -= damage;
        GameManager.instance.UpdateHealthPointSlider(healthPoint);
        if (healthPoint < 1)
        {
            GameManager.instance.GameOvered();
        }
    }
}

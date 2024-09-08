using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int healthPoint;

    private bool onGround = true;
    private int jumpCount = 0;
    private int maxJumpCount = 2;

    void Start()
    {

    }

    void Update()
    {
        if (IsJumpable() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
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

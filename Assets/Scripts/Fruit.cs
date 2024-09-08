using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.left;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float leftEdge = -25f;
    [SerializeField] private int damege = 0;

    void Start()
    {

    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            if (this.damege > 0)
            {
                Player p = other.gameObject.GetComponent<Player>();
                p.Damaged(damege);
            }
            else
            {
                GameManager.instance.IncreaseFruitCount(gameObject);
            }
        }
    }
}

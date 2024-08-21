using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveDirection = Vector3.left;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float width = 40f;
    [SerializeField]
    private Transform nextBackground;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if (transform.position.x <= -width)
        {
            transform.position = nextBackground.position - moveDirection * width;
        }
    }
}

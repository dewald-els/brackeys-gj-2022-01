using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 move;
    [SerializeField] private float speed = 1.0f;

    [Header("Components")]
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        move = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * speed, move.y * speed);
    }

}

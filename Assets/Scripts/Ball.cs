using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpSpeed;
    public float moveForce;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity += Vector2.up * jumpSpeed;
        }

        var hor = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(hor, 0) * moveForce);

        var ver = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector2(ver, 0) * moveForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Teleporter")
        {
            FindObjectOfType<GameManager>().Win();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;

        if (other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().Lose();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    Rigidbody2D rb;
    private Vector2 moveInput;
    CircleCollider2D bodyColl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyColl = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        // Change player's speed
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
    }

    void OnJump(InputValue value)
    {
        bool isStanding = bodyColl.IsTouchingLayers(LayerMask.GetMask("Ground", "Subplatform"));
        if (value.isPressed && isStanding)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("Complete level!");
        }
    }
}

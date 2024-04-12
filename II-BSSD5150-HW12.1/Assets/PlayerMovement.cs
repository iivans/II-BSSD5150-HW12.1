using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
    }

    private void Update()
    {
        // Get horizontal and vertical input values (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the input values
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize the movement vector to ensure consistent movement speed diagonally
        movement = movement.normalized;

        // Move the player using Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }
}
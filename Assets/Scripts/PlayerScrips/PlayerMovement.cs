using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private PlayerInputHandler inputHandler;    // este tiene el MovementInput
    private Rigidbody2D rb;
    private Vector2 movement;

     // Events for animation and other system
    public event Action<Vector2> OnMove;


    private void Awake()
    {
        
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //HandleDash();
        movement = inputHandler.MovementInput;
        OnMove?.Invoke(movement);
    }

    private void FixedUpdate()
    {
       Move();

    }

    private void Move()
    {
        rb.velocity = movement * moveSpeed;
    }
}
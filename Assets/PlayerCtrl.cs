using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Evento para notificar el movimiento
    public event Action<Vector2> OnMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Obtener la entrada de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Invocar el evento para la animación
        OnMove?.Invoke(movement);
    }

    private void FixedUpdate()
    {
        // Mover al jugador
        rb.velocity = movement * moveSpeed;
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interaction system

/*
public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private float interactionRadius = 2f;
    [SerializeField] private LayerMask interactableLayer;

    private PlayerInputHandler inputHandler;
    private IInteractable currentInteractable;

    private void Awake() {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update() {
        DetectInteractables();
        HandleInteraction();
    }

    private void DetectInteractables() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayer);
        currentInteractable = null;

        // Get closest interactable
        float closestDistance = float.MaxValue;
        foreach (var collider in colliders) {
            if (collider.TryGetComponent<IInteractable>(out var interactable)) {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    currentInteractable = interactable;
                }
            }
        }
    }

    private void HandleInteraction() {
        if (inputHandler.IsInteractPressed && currentInteractable != null) {
            currentInteractable.Interact();
        }
    }
}
*/
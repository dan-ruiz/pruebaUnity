using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Combat system
public class PlayerCombat : MonoBehaviour {
    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    private PlayerInputHandler inputHandler;
    private float nextAttackTime;

    public event Action OnAttackStart;
    public event Action OnAttackEnd;

    private void Awake() {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update() {
        HandleAttack();
    }

    private void HandleAttack() {
        if (inputHandler.IsAttackPressed && Time.time >= nextAttackTime) {
            PerformAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void PerformAttack() {
        OnAttackStart?.Invoke();

        // Perform attack logic (e.g., raycast, overlap circle, etc.)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (var hit in hits) {
            /*
            if (hit.TryGetComponent<IDamageable>(out var damageable)) {
                damageable.TakeDamage(1);
            }
            */
        }

        // End attack animation after a delay
        StartCoroutine(EndAttackAnimation());
    }

    private System.Collections.IEnumerator EndAttackAnimation() {
        yield return new WaitForSeconds(0.2f); // Adjust based on animation length
        OnAttackEnd?.Invoke();
    }
}
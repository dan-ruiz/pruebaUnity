using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class AiDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius; // Radio de visión
    public Transform viewCheckPosition; // Posicion de objetivo si colisiona con el player
    public LayerMask playerLayer; // Capa del jugador
    public bool targetInRange; // Estado de si el objetivo está en rango
    private Collider2D playerCollider; // Collider del jugador detectado
    private void Update()
    {
        CheckRange();
    }
    
    public void CheckRange(){
        playerCollider = Physics2D.OverlapCircle(viewCheckPosition.position, viewRadius, playerLayer);
        targetInRange = playerCollider != null; // Actualiza el estado de targetInRange
        if (targetInRange)
        {
             //  Método SetTarget en FollowPlayer
            if (TryGetComponent<FollowPlayer>(out var followPlayer)) followPlayer.SetTarget(playerCollider.transform);
            // Verifica si posee FireController
            if (TryGetComponent<FireController>(out var fireController)) fireController.CheckFireRange();
        }
        // Si no hay objetivo, inicia el patrullaje
        else GetComponent<AiPatrol>().Patrol(); 
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(viewCheckPosition.position, viewRadius);
    }
}
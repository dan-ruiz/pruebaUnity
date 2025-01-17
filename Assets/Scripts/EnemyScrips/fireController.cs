using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireController : MonoBehaviour
{

    [Range(1, 15)]
    [SerializeField]
    private float viewRadius;
    public Transform fireCheckRange;
    public Transform playerPos;
    public float fireDelay, timeLastShoot, rateOfFire;
    public LayerMask playerLayer;
    public bool playerInRange;
    public GameObject enemySpell;

    private void Start()
    {
        playerLayer = GetComponent<AiDetector>().playerLayer;
    }

    // Update is called once per frame
    public void CheckFireRange(){
        playerInRange = Physics2D.OverlapCircle(fireCheckRange.position, viewRadius, playerLayer);
        if (playerInRange)
        {
            if (TryGetComponent<FollowPlayer>(out var followPlayer))
            {
                if (followPlayer != null)
                {
                    followPlayer.FollowPlayerPosition();
                }
            }
            playerPos = FireAngle().transform;

            Vector2 direction = playerPos.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (Time.time > fireDelay + timeLastShoot)
            {
                timeLastShoot = Time.time;
                Invoke(nameof(Fire), rateOfFire);
            }
        }
    }
    private void Fire()
    {
        GameObject spellInvoke = Instantiate(enemySpell, fireCheckRange.position, Quaternion.identity);
        Vector2 shootDir = (playerPos.position - transform.position).normalized;
        spellInvoke.GetComponent<EnemySpell>().SetDirection(shootDir);
    }
    public Collider2D FireAngle()
    {
        return Physics2D.OverlapCircle(fireCheckRange.position, viewRadius, playerLayer);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(fireCheckRange.position, viewRadius);
    }
}

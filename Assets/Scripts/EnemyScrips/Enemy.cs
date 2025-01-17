using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyHealth; // Salud del enemigo
    [SerializeField] private int initialHealth; // Salud inicial del enemigo
    [SerializeField] private int damage; // Daño que el enemigo puede infligir
    [SerializeField] private float speed; // Velocidad de movimiento del enemigo
    [SerializeField] private GameObject deathSpritePrefab; // Prefab del sprite que aparecerá al morir

    private SpawnEnemyPool enemyPool;
    private SpawnEnemy spawnManager;


    void Start()
    {
        initialHealth = enemyHealth; // Guardar la salud inicial
    }

    void OnEnable()
    {
        enemyHealth = initialHealth; // Restablecer la salud cuando el enemigo se active
    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Calcular la probabilidad basada en los enemigos restantes y los sprites de muerte restantes
        int enemiesRemaining = SpawnEnemy.totalEnemiesSpawned - SpawnEnemy.deathSpritesGenerated;
        int deathSpritesRemaining = spawnManager.maxDeathSprites - SpawnEnemy.deathSpritesGenerated;
        float deathSpriteProbability = (float)deathSpritesRemaining / enemiesRemaining;

        // Verificar si se cumplen las condiciones para generar un sprite de muerte
        if (Random.value < deathSpriteProbability)
        {
            // Instanciar el sprite de muerte en la posición del enemigo
            Instantiate(deathSpritePrefab, transform.position, Quaternion.identity);
            SpawnEnemy.deathSpritesGenerated++;
        }

        enemyPool.ReturnEnemyToPool(gameObject); // Desactivar el enemigo del pool
    }

    // Método para establecer el pool de enemigos
    public void SetPool(SpawnEnemyPool pool)
    {
        enemyPool = pool;
    }

    // Método para establecer el manager de spawn
    public void SetSpawnManager(SpawnEnemy manager)
    {
        spawnManager = manager;
    }

    // Método para infligir daño (opcional)
    public void Attack(PlayerHealth player)
    {
        player.TakeDamage(damage);
    }

    // Método para obtener el daño
    public int GetDamage()
    {
        return damage;
    }
}

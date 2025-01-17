using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private GameManager gameManager;
    private Enemy enemy;

    public int maxPlayerHealth; // Salud maxima del jugador
    [SerializeField] public int currentPlayerHealth; // Salud actual del jugador
    private float timeSinceLastDamage = 0f;
    private float regenInterval = 5f; // Intervalo de tiempo para regenerar vida

    public UnityEvent<int> changeHealth;
    void Start()
    {
        // Asignar la referencia de GameManager
        gameManager = FindObjectOfType<GameManager>();
        // Asignar la referencia del enemigo
        enemy = FindObjectOfType<Enemy>();

        currentPlayerHealth = maxPlayerHealth; // Inicializar la salud actual al máximo
        changeHealth.Invoke(currentPlayerHealth);
        StartCoroutine(RegenerateHealth());
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(enemy.GetDamage());
        }
    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {

        currentPlayerHealth -= amount;
        changeHealth.Invoke(currentPlayerHealth);
        timeSinceLastDamage = 0f; // Reiniciar el contador de tiempo sin recibir daño
        if (currentPlayerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí se puede agregar lógica para cuando el jugador muere, como reproducir una animación o sonido
        gameManager.GameOver();
    }

    // Método para regenerar vida
    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);
            if (timeSinceLastDamage >= regenInterval && currentPlayerHealth > 0 && currentPlayerHealth < maxPlayerHealth)
            {
                currentPlayerHealth++;
                Debug.Log("Player health regenerated: " + currentPlayerHealth);
            }
            changeHealth.Invoke(currentPlayerHealth);
        }
    }
}

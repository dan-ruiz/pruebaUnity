using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritStar : MonoBehaviour
{
    private InventorySystem inventorySystem;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener la referencia al InventorySystem
        inventorySystem = FindObjectOfType<InventorySystem>();
        // Obtener la referencia al GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                // Llamar a la función PickUp del InventorySystem
                inventorySystem.PickUp(gameObject);
                // Llamar a la función CollectSpirit del GameManager
                gameManager.CollectSpirit();
                // Desactivar el GameObject
                gameObject.SetActive(false);
            }
        }
    }
}

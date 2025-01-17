using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField] private float shootOffset;
    private Vector2 lastInput = Vector2.down;

    private GameManager gameManager;

    // Para modificar el ui de los dulces
    private CandyUI candyUI;
    private CandySelectorUI candySelectorUI;

    // Variables de animacion
    public bool hasShot = false;
    private PlayerAnimator playerAnimator;
    private float attackDuration = 0.1f; // Duración de la animación
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    // Variables de Audio
    public AudioClip shootClip;

    // Último power-up recogido
    private string lastCandyType = "ChocolateCandy";
    private List<string> candyTypes = new List<string> { "ChocolateCandy", "YellowCandy", "BlueCandy", "RedCandy" };
    private int currentCandyIndex = 0;

    // Cantidad de dulces disponibles
    private Dictionary<string, int> candyCounts = new Dictionary<string, int>
    {
        { "ChocolateCandy", int.MaxValue }, // Ilimitado
        { "YellowCandy", 0 },
        { "BlueCandy", 0 },
        { "RedCandy", 0 }
    };

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        inputHandler = FindObjectOfType<PlayerInputHandler>();
        if (inputHandler == null)
        {
            Debug.LogError($"No PlayerInputHandler found in scene for {gameObject.name}");
            enabled = false;
        }

        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    void Start()
    {
        candyUI = FindObjectOfType<CandyUI>();
        candySelectorUI = FindObjectOfType<CandySelectorUI>();

        // Asegurarse de que el círculo del dulce por defecto esté activado
        candySelectorUI.UpdateCandySelection(lastCandyType);
    }


    void LateUpdate()
    {
        if (gameManager.isGameActive)
        {
            Shoot();
        }


        if (inputHandler.MovementInput != Vector2.zero)
        {
            lastInput = inputHandler.MovementInput;
        }

        if (inputHandler.SwitchCandy)
        {
            SwitchCandyType();
        }
    }

    public void Shoot()
    {
        if (inputHandler.IsAttackPressed)
        {
            if (candyCounts[lastCandyType] > 0)
            {
                hasShot = true;
                if (playerAnimator != null)
                {
                    playerAnimator.GetComponent<Animator>().SetBool(IsAttackingHash, true);
                }
                StartCoroutine(ResetShootState());

                // Instanciar el candy en la posición del punto de disparo
                GameObject candy = CandyPool.Instance.RequestCandy(lastCandyType);
                candy.transform.position = (Vector2)transform.position + shootOffset * inputHandler.MovementInput;
                candy.TryGetComponent(out Candy shootCandy);
                candy.SetActive(true);
                shootCandy?.SetDirection(inputHandler.MovementInput != Vector2.zero ? inputHandler.MovementInput : lastInput);

                Debug.Log($"Candy {lastCandyType} instantiated at position {candy.transform.position}");
                Debug.Log($"Candy active state: {candy.activeSelf}");
                AudioManager.Instance.PlaySFX(shootClip);

                // Reducir la cantidad de dulces disponibles
                if (lastCandyType != "ChocolateCandy")
                {
                    candyCounts[lastCandyType]--;
                    candyUI.RemoveCandies(lastCandyType, 1); // Actualizar la UI
                }
            }
            else
            {
                // Si no hay dulces disponibles, disparar chocolate
                lastCandyType = "ChocolateCandy";
                Shoot();
            }
        }
        hasShot = false;
    }
    private IEnumerator ResetShootState()
    {
        yield return new WaitForSeconds(attackDuration);
        hasShot = false;
        if (playerAnimator != null)
        {
            playerAnimator.GetComponent<Animator>().SetBool(IsAttackingHash, false);
        }
    }

    // Método para actualizar el tipo de dulce basado en el último power-up recogido
    public void UpdateLastCandyType(string candyType)
    {
        lastCandyType = candyType;
        candySelectorUI.UpdateCandySelection(candyType);
    }

    // Método para cambiar el tipo de dulce
    private void SwitchCandyType()
    {
        int initialIndex = currentCandyIndex;
        do
        {
            currentCandyIndex = (currentCandyIndex + 1) % candyTypes.Count;
            lastCandyType = candyTypes[currentCandyIndex];
        } while (candyCounts[lastCandyType] == 0 && currentCandyIndex != initialIndex);

        Debug.Log($"Candy type switched to: {lastCandyType}");
        candySelectorUI.UpdateCandySelection(lastCandyType);
    }

    // Método para obtener la cantidad de un tipo específico de dulce
    public int GetCandyQuantity(string candyType)
    {
        if (candyCounts.ContainsKey(candyType))
        {
            return candyCounts[candyType];
        }
        return 0;
    }

    // Método para agregar dulces a la cantidad disponible
    public void AddCandies(string candyType, int amount)
    {
        if (candyCounts.ContainsKey(candyType))
        {
            candyCounts[candyType] += amount;
        }
    }

}

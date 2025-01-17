using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySelectorUI : MonoBehaviour
{
    [SerializeField] private GameObject yellowCandyCircle;
    [SerializeField] private GameObject blueCandyCircle;
    [SerializeField] private GameObject redCandyCircle;
    [SerializeField] private GameObject chocolateCandyCircle;

    private Dictionary<string, GameObject> candyCircles;
    private PlayerShooting playerShooting;

    void Awake()
    {
        candyCircles = new Dictionary<string, GameObject>
        {
            { "YellowCandy", yellowCandyCircle },
            { "BlueCandy", blueCandyCircle },
            { "RedCandy", redCandyCircle },
            { "ChocolateCandy", chocolateCandyCircle }
        };

        // Desactivar todos los círculos inicialmente
        foreach (var circle in candyCircles.Values)
        {
            circle.SetActive(false);
        }
    }

    void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
        // Asegurarse de que el círculo del dulce por defecto esté activado
        UpdateCandySelection("ChocolateCandy");
    }

    public void UpdateCandySelection(string candyType)
    {
        // Desactivar todos los círculos
        foreach (var circle in candyCircles.Values)
        {
            circle.SetActive(false);
        }

        // Activar el círculo correspondiente al tipo de dulce
        if (candyCircles.ContainsKey(candyType))
        {
            candyCircles[candyType].SetActive(true);
        }
    }
}

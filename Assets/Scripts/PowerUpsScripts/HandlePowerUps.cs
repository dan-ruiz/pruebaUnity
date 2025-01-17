using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePowerUps : MonoBehaviour
{
    [SerializeField] private List<GameObject> yellowCandyList = new List<GameObject>();
    [SerializeField] private List<GameObject> blueCandyList = new List<GameObject>();
    [SerializeField] private List<GameObject> redCandyList = new List<GameObject>();

    private PlayerShooting playerShooting;
    private CandySelectorUI candySelectorUI;
    private CandyUI candyUI;

    void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
        candySelectorUI = FindObjectOfType<CandySelectorUI>();
        candyUI = FindObjectOfType<CandyUI>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            PowerUps powerUp = collision.GetComponent<PowerUps>();
            if (powerUp != null)
            {
                AddCandiesToList(powerUp.yellowCandies, yellowCandyList);
                AddCandiesToList(powerUp.blueCandies, blueCandyList);
                AddCandiesToList(powerUp.redCandies, redCandyList);

                // Actualizar el tipo de dulce en PlayerShooting, CandySelectorUI y CandyUI
                if (powerUp.yellowCandies.Count > 0)
                {
                    playerShooting.AddCandies("YellowCandy", powerUp.yellowCandies.Count);
                    candyUI.AddCandies("YellowCandy", powerUp.yellowCandies.Count);
                }
                if (powerUp.blueCandies.Count > 0)
                {
                    playerShooting.AddCandies("BlueCandy", powerUp.blueCandies.Count);
                    candyUI.AddCandies("BlueCandy", powerUp.blueCandies.Count);
                }
                if (powerUp.redCandies.Count > 0)
                {
                    playerShooting.AddCandies("RedCandy", powerUp.redCandies.Count);
                    candyUI.AddCandies("RedCandy", powerUp.redCandies.Count);
                }

                // Asegurarse de que el círculo del dulce chocolate esté activado por defecto
                candySelectorUI.UpdateCandySelection("ChocolateCandy");

                // Destruir el power-up después de recogerlo
                Destroy(collision.gameObject);
                Debug.Log(yellowCandyList.Count);
                Debug.Log(blueCandyList.Count);
                Debug.Log(redCandyList.Count);
            }
        }
    }

    private void AddCandiesToList(List<GameObject> sourceList, List<GameObject> targetList)
    {
        foreach (var candy in sourceList)
        {
            targetList.Add(candy);
        }
    }
}

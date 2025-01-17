using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CandyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI yellowCandyText;
    [SerializeField] private TextMeshProUGUI blueCandyText;
    [SerializeField] private TextMeshProUGUI redCandyText;

    private int yellowCandyCount = 0;
    private int blueCandyCount = 0;
    private int redCandyCount = 0;

    private CandySelectorUI candySelectorUI;

    private void Start()
    {
        candySelectorUI = FindObjectOfType<CandySelectorUI>();
        UpdateCandyText();
    }

    public void AddCandies(string candyType, int amount)
    {
        switch (candyType)
        {
            case "YellowCandy":
                yellowCandyCount += amount;
                break;
            case "BlueCandy":
                blueCandyCount += amount;
                break;
            case "RedCandy":
                redCandyCount += amount;
                break;
        }
        UpdateCandyText();
    }

    public void RemoveCandies(string candyType, int amount)
    {
        switch (candyType)
        {
            case "YellowCandy":
                yellowCandyCount = Mathf.Max(0, yellowCandyCount - amount);
                if (yellowCandyCount == 0)
                {
                    candySelectorUI.UpdateCandySelection("ChocolateCandy");
                }
                break;
            case "BlueCandy":
                blueCandyCount = Mathf.Max(0, blueCandyCount - amount);
                if (blueCandyCount == 0)
                {
                    candySelectorUI.UpdateCandySelection("ChocolateCandy");
                }
                break;
            case "RedCandy":
                redCandyCount = Mathf.Max(0, redCandyCount - amount);
                if (redCandyCount == 0)
                {
                    candySelectorUI.UpdateCandySelection("ChocolateCandy");
                }
                break;
        }
        UpdateCandyText();
    }

    private void UpdateCandyText()
    {
        yellowCandyText.text = "= " + yellowCandyCount;
        blueCandyText.text = "= " + blueCandyCount;
        redCandyText.text = "= " + redCandyCount;
    }
}

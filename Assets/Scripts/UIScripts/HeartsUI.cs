using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public List<Image> heartsList;
    public GameObject heartPref;
    public PlayerHealth playerHealth;
    public int currentIndex;
    public Sprite heart;
    void Awake()
    {
        playerHealth.changeHealth.AddListener(ChangeHearts);
    }

    private void ChangeHearts(int currentHealth)
    {
        if (!heartsList.Any())
        {
            CreateHearts(playerHealth.maxPlayerHealth);
        }
        ChangeHealth(currentHealth);
    }

    private void CreateHearts(int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPref, transform);
            heartsList.Add(heart.GetComponent<Image>());
        }
        currentIndex = maxHealth - 1;
    }

    private void ChangeHealth(int currentHealth)
    {
        for (int i = 0; i < heartsList.Count; i++)
        {
            if (i < currentHealth)
            {
                heartsList[i].sprite = heart;
                heartsList[i].gameObject.SetActive(true);
            }
            else
            {
                heartsList[i].gameObject.SetActive(false);
            }
        }
        currentIndex = currentHealth - 1;
    }


}

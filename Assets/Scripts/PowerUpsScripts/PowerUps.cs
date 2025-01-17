using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject yellowCandyPrefab;
    public GameObject blueCandyPrefab;
    public GameObject redCandyPrefab;

    public List<GameObject> yellowCandies = new List<GameObject>();
    public List<GameObject> blueCandies = new List<GameObject>();
    public List<GameObject> redCandies = new List<GameObject>();

    void Start()
    {
        AddRandomCandies(yellowCandyPrefab, yellowCandies);
        AddRandomCandies(blueCandyPrefab, blueCandies);
        AddRandomCandies(redCandyPrefab, redCandies);
    }

    void AddRandomCandies(GameObject candyPrefab, List<GameObject> candyList)
    {
        int count = Random.Range(0, 6); // Generar un número aleatorio entre 0 y 5
        for (int i = 0; i < count; i++)
        {
            GameObject candy = Instantiate(candyPrefab);
            candyList.Add(candy);
        }
    }
}

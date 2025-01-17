using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPool : MonoBehaviour
{
    [SerializeField] private GameObject yellowCandyPref;
    [SerializeField] private GameObject blueCandyPref;
    [SerializeField] private GameObject redCandyPref;
    [SerializeField] private GameObject chocolateCandyPref;
    [SerializeField] private int poolSize = 20;
    private List<GameObject> candyList = new List<GameObject>();

    // Patron Singleton
    private static CandyPool instance;
    public static CandyPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddCandiesToPool(poolSize, yellowCandyPref);
        AddCandiesToPool(poolSize, blueCandyPref);
        AddCandiesToPool(poolSize, redCandyPref);
        AddCandiesToPool(poolSize, chocolateCandyPref);
    }


    private void AddCandiesToPool(int amount, GameObject candyPrefab)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject candy = Instantiate(candyPrefab, transform);
            candy.GetComponent<Candy>().SetPool(this);
            candy.SetActive(false);
            candyList.Add(candy);
        }
    }

    // Funcion que permite instanciar un candy desde otro script 
    public GameObject RequestCandy(string candyType)
    {
        foreach (var candy in candyList)
        {
            if (!candy.activeSelf && candy.CompareTag(candyType))
            {
                return candy;
            }
        }
        GameObject candyPrefab = GetCandyPrefab(candyType);
        AddCandiesToPool(1, candyPrefab);
        candyList[candyList.Count - 1].SetActive(true);
        return candyList[candyList.Count - 1];
    }

    private GameObject GetCandyPrefab(string candyType)
    {
        switch (candyType)
        {
            case "YellowCandy":
                return yellowCandyPref;
            case "BlueCandy":
                return blueCandyPref;
            case "RedCandy":
                return redCandyPref;
            case "ChocolateCandy":
                return chocolateCandyPref;
            default:
                return chocolateCandyPref;
        }
    }

    public void ReturnCandyToPool(GameObject candy)
    {
        candy.SetActive(false);
    }
}

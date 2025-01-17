using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private int poolSize = 60;
    [SerializeField] private List<GameObject> enemyList;

    // Patron Singleton
    private static SpawnEnemyPool instance;
    public static SpawnEnemyPool Instance { get { return instance; } }

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
        AddEnemiesToPool(poolSize);
    }


    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(enemyPref, transform);
            enemy.GetComponent<Enemy>().SetPool(this); // Crear un Script para enemy para poder obetener el componente 
            enemy.SetActive(false);
            enemyList.Add(enemy);
        }
    }

    // Funcion que permite instanciar un enemy desde otro script 
    public GameObject RequestEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeSelf)
            {

                return enemyList[i];
            }
        }
        AddEnemiesToPool(5); // El valor enviado por parametro va a depender de cuantos spawn este ubicados en el mapa
        enemyList[enemyList.Count - 1].SetActive(true);
        return enemyList[enemyList.Count - 1];
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}

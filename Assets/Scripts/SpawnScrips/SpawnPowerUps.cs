using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject powerUp;
    void Start()
    {
        foreach (var spawn in spawns)
        {
            Instantiate(powerUp, spawn.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

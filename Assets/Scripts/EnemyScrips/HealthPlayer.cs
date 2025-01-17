using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    // Variables
    public int lifePoints;

    public void TakeDamage(int damage)
    {
        lifePoints -= damage;
        if (lifePoints <= 0)
        {
            Destroy(gameObject);
        }
    } 
}

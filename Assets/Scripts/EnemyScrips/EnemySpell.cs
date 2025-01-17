using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpell : MonoBehaviour
{
    // Variables
    public float speed, delayFire ;
    public int damage;
    public Vector2 direction;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction );
        StartCoroutine(SelfDestruct());
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HealthPlayer>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

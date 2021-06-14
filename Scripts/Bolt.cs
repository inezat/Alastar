using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 1;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log(hitInfo.name);
        Vampire enemy = hitInfo.GetComponent<Vampire>();
        Shadow enemy2 = hitInfo.GetComponent<Shadow>();
        Boss enemy3 = hitInfo.GetComponent<Boss>();
        if(enemy != null){
            enemy.TakeDamage(damage);
        }
        if(enemy2 != null){
            enemy2.TakeDamage(damage);
        }
        if(enemy3 != null){
            enemy3.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

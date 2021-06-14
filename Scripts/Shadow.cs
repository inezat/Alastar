using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Animator anim;
    public bool awake = false;
    public bool dead = false;
    public float awakeRange;
    private Player player;
    Vector3 myVector;
    public int currentHealth;
    public int maxHealth;
    public float fireRate;
    public float nextFire;
    public GameObject vampProj;

    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
        fireRate = 5f;
        nextFire = Time.time;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Awake(){
        anim = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        anim = gameObject.GetComponent<Animator>();
        dead = true;
        StartCoroutine(wait(.5f));
    }
    public IEnumerator wait(float counter){
        yield return new WaitForSeconds(counter);
        Destroy(gameObject);
    }
    
    public void Attack(){
        if(Time.time > nextFire && awake && dead == false){
            Instantiate(vampProj, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("dead", dead);
        Attack();
    }
    void OnTriggerStay2D(Collider2D col){
        if(dead == false){    
            if(col.CompareTag("Player")){
                awake = true;
                StartCoroutine(damage(0.25f));
            }
        }
    }

    public IEnumerator damage(float counter){
        yield return new WaitForSeconds(counter);
        player.Damage(1,1);
        myVector = new Vector3(0.75f, 0.25f, 0.0f);
        //player.transform.position
        StartCoroutine(player.KnockBack(0.01f, 100, myVector));
    }
}

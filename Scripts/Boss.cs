using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public float distance;
    public float aggroRange;
    public float fireRate;
    public float nextFire;
    Seeker seeker;
    Rigidbody2D rb;
    Path path;
    public float speed = 300f;
    public float nextWaypointDistance = 3f;
    public ForceMode2D fMode;
    public bool aggro = false;
    public bool dead = false;
    
    bool pathIsEnded = false;

    public GameObject vampProj;
    public Transform target;
    public Animator anim;
    int currentWaypoint = 0;
    //public GameObject deathEffect;
    

    void Start(){
        anim = gameObject.GetComponent<Animator>();
        if(dead == false){
            currentHealth = maxHealth;
            fireRate = 2f;
            nextFire = Time.time;
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            InvokeRepeating("UpdatePath",0f,.5f);
        }
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
        StartCoroutine(wait(1.75f));
    }
    public IEnumerator wait(float counter){
        yield return new WaitForSeconds(counter);
        Destroy(gameObject);
    }

    void UpdatePath(){
        if(seeker.IsDone() && aggro == true){
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }
    
    public void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate(){
        anim.SetBool("dead", dead);
        if(dead == false){
            RangeCheck();
            Attack();
            if(path == null){
                return;
            }
            if(currentWaypoint >= path.vectorPath.Count){
                pathIsEnded = true;
                return;
            }else{
                pathIsEnded = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if(distance < nextWaypointDistance){
                currentWaypoint++;
            }
            if(target.transform.position.x < transform.position.x){
                transform.localScale = new Vector3(-10, 10, 10);
            }else{
                transform.localScale = new Vector3(10, 10, 10);
            }
            if(currentHealth <= 0){
                Die();
            }
        }
    }

     void RangeCheck(){
         distance = Vector3.Distance(transform.position, target.transform.position);
         if(distance < aggroRange){
             aggro = true;
         }else{
             aggro = false;
         }
     }

     public void Attack(){
         if(Time.time > nextFire && aggro){
             Instantiate(vampProj, transform.position, Quaternion.identity);
             nextFire = Time.time + fireRate;
         }
     }

}

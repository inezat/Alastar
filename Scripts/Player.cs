using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 50f;
    public float maxSpeed = 3;
    public float jumpPower = 150f;
    public bool grounded;
    public bool canDoubleJump;
    public bool invuln = false;
    public bool dead = false;
    public int currentHealth;
    public int maxHealth = 4;
    
    private Rigidbody2D rigid2d;
    private Animator ani;
    public GameObject vampProj;
    public bool rotated = false;

    void Start()
    {
        rigid2d = gameObject.GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        ani.SetBool("Grounded", grounded);
        ani.SetFloat("Speed", Mathf.Abs(rigid2d.velocity.x));
        ani.SetBool("dead", dead);
        if(dead == false){
            if((Input.GetAxis("Horizontal") < -0.1f) && rotated == false){
                transform.Rotate(0f, 180f, 0f);
                rotated = true;
            }
            if((Input.GetAxis("Horizontal") > 0.1f) && rotated == true){
                transform.Rotate(0f, 180f, 0f);
                rotated = false;
            }
            if(Input.GetButtonDown("Jump")){
                if(grounded){
                    rigid2d.AddForce(Vector2.up * jumpPower);
                    canDoubleJump = true;
                }else{
                    if(canDoubleJump){
                        canDoubleJump = false;
                        rigid2d.velocity = new Vector2(rigid2d.velocity.x,0);
                        rigid2d.AddForce(Vector2.up * jumpPower/1.75f);
                    }
                }
            }
            if(currentHealth > maxHealth){
                currentHealth = maxHealth;
            }
            if(currentHealth <= 0){
                StartCoroutine(Die(2));
            }
        }
    }


    void FixedUpdate()
    {
        Vector3 easeVelocity = rigid2d.velocity;
        easeVelocity.y = rigid2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");
        //easing the x speed of player
        if(grounded)
        {
            rigid2d.velocity = easeVelocity;
        }
        //moving the player
        rigid2d.AddForce((Vector2.right * speed) * h);
        //limiting player speed
        if(rigid2d.velocity.x > maxSpeed){
            rigid2d.velocity = new Vector2(maxSpeed, rigid2d.velocity.y);
        }
        if(rigid2d.velocity.x < -maxSpeed){
            rigid2d.velocity = new Vector2(-maxSpeed, rigid2d.velocity.y);
        }
    }

    public IEnumerator Die(float counter){
        ani = gameObject.GetComponent<Animator>();
        dead = true;
        yield return new WaitForSeconds(counter);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Damage(int dmg, float counter){
        if(invuln == false){
        currentHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("flash");
        invuln = true;
        StartCoroutine(Invulnerability(counter));
        }
     }


    public IEnumerator Invulnerability(float counter){
        yield return new WaitForSeconds(counter);
        invuln = false;
    }

    public IEnumerator KnockBack(float knockDur, float knockBackPwr, Vector3 knockBackDir){
        
        float timer = 0;
        rigid2d.velocity = new Vector2(rigid2d.velocity.x, 0);

        while(knockDur > timer){
            timer += Time.deltaTime;
            rigid2d.AddForce(new Vector3(knockBackDir.x * -50, knockBackDir.y + knockBackPwr, transform.position.z));
        }
        yield return 0;
    }
}

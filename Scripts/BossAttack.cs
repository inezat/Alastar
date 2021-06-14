using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Boss boss;
    
    void Aggro(){
        boss  = gameObject.GetComponentInParent<Boss>();
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Player")){
            boss.Attack();
        }
    }
}

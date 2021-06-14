using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampAttack : MonoBehaviour
{
    public Vampire vampire;
    
    void Aggro(){
        vampire  = gameObject.GetComponentInParent<Vampire>();
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Player")){
            vampire.Attack();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Player player;
    Vector3 myVector;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void OnTriggerStay2D(Collider2D col){
        if(col.CompareTag("Player")){
            player.Damage(1,1);
            myVector = new Vector3(0.0f, 1.0f, 0.0f);
            StartCoroutine(player.KnockBack(0.02f, 350, myVector));
        }
    }
}

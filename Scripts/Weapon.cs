using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject boltPrefab;
    public float fireRate;
    public float nextFire;


    void Start(){
        fireRate = 0.5f;
        nextFire = Time.time;
    }
    void Update()
    {
        shoot(); 
    }

    void shoot(){
        if(Input.GetButtonDown("Fire1")){
            if(Time.time > nextFire){
                Instantiate(boltPrefab, firePoint.position, firePoint.rotation);    
                nextFire = Time.time + fireRate;
            }
        }
    }
}

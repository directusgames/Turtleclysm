﻿using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {
    
    //sizeAddition is the amount of size to add to the turtle when eaten
    public float speed, minSize, maxSize, size, sizeAddition;
    public GameObject deathEffect;    
    private Rigidbody2D rigid;
      
	// Use this for initialization
	void Start () {
        
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * speed; 
          
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnDestroy()
    {
        GameObject exp = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}

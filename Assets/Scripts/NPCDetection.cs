﻿using UnityEngine;
using System.Collections;

public class NPCDetection : MonoBehaviour {
    
    public PlayerInput pi;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        pi.OnTriggerEnter2D(coll);
    }
}
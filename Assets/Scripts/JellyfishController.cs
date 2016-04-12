using UnityEngine;
using System.Collections;

public class JellyfishController : MonoBehaviour {
    
    public float speed;
    public Rigidbody2D rigid;
      
	// Use this for initialization
	void Start () {
        
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = transform.up * speed; 
          
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

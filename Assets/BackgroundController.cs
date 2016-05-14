using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
    
    public GameObject player;
    
	// Use this for initialization
	void Start () {
    
        transform.position = player.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        transform.position = player.transform.position;
        
	}
}

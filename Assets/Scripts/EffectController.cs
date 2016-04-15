using UnityEngine;
using System.Collections;

public class EffectController : MonoBehaviour {

    public float lifetime;
    
    private float curTime;

	// Use this for initialization
	void Start () {
    
        curTime = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
    
        curTime += Time.deltaTime;
        if(curTime >= lifetime)
        {
            Destroy (this.gameObject);
        }
	
	}
}

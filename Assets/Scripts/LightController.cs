using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
    
    public PlayerInput playerInput;
    public float intensityDecreaseRate;
    
    private Light oceanBrightness;
    private float maxIntensity;
    
    
	// Use this for initialization
	void Start () {
        
        oceanBrightness = GetComponent<Light>();
        maxIntensity = oceanBrightness.intensity;
	
	}
	
	// Update is called once per frame
	void Update () {
    
        oceanBrightness.intensity = Mathf.Clamp(maxIntensity - (playerInput.currentDepth / intensityDecreaseRate), 0f, maxIntensity);


	
	}
}

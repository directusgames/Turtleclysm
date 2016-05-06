using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInput2 : MonoBehaviour
{
    public Transform m_playerPos;
    public Transform turtleImage;
    public Rigidbody2D m_rigidBody;
    public ParticleSystem bubbles;
    public Light oceanBrightness;
    public Text debugText;
    
    public Animator anim;
    
    public bool m_cooldown = false; // True: in cooldown, False: not in cooldown.
    public float m_cooldownLength = 2.3f; // Length of cooldown time.
    private float m_timeWaited = 0f; // Time the user has waited.
    
    private bool moving;
    
    private Vector2 m_initialTouch;
    public float maxSpeed = 1f;
    public float currentDepth;
    public float originDepth;
    public float smoothTime = 0.8F;
    
    private ParticleSystem.EmissionModule em;
    
    private Vector2 touchPos;
    private Vector2 currentVel = Vector2.zero;
    public float slowDownThreshold, stopThreshold;
    
    
    
    // Use this for initialization
    void Start ()
    {
        moving = false;
    }
    
    void Update() {
        
        currentDepth = originDepth - m_playerPos.position.y;
        
        if(!moving)
        {
            CheckInput();
        }
        else
        {
            //transform.position = Vector3.SmoothDamp(transform.position, touchPos, ref currentVel, smoothTime);
            transform.position = Vector2.SmoothDamp(transform.position, touchPos, ref currentVel, smoothTime, maxSpeed);
            debugText.text = "Velocity magnitude: " + currentVel.magnitude;
            
            if(currentVel.magnitude <= slowDownThreshold)
            {
                anim.SetBool ("propelled", false);
                anim.SetBool ("slowingDown", true);
                
                if(currentVel.magnitude <= stopThreshold)
                {
                    moving = false;
                    anim.SetBool ("idle", true);
                    anim.SetBool ("slowingDown", false);
                    anim.CrossFade ("Idle", 0f);
                }
            }   
        }
        
      
    }
    
    void CheckInput() {
    
        if(Input.GetMouseButton(0))
        {
            moving = true;
            
            //Get turtle's current position and the clicked position.
            Vector2 currentPos = m_playerPos.position;
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //find vector between two positions
            Vector2 diffPos = new Vector2(touchPos.x, touchPos.y) - currentPos;
            
            //calculate angle and rotation based off vector
            float angle = Mathf.Atan2(diffPos.y, diffPos.x) * Mathf.Rad2Deg;
            turtleImage.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            anim.SetBool ("slowingDown", false);
            anim.SetBool ("idle", false);
            anim.SetBool ("propelled", true);
            anim.CrossFade ("Propelling", 0f);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "npc")
        {
            if(coll.gameObject != null)
            {
                Debug.Log ("Entered");
                StartCoroutine("DestroyNPC", coll);
                anim.CrossFade("Chomp", 0f);
            }
        }
    }
    
    IEnumerator DestroyNPC(Collider2D coll)
    {
        yield return new WaitForSeconds(0.25f);
        NPCController npc = coll.gameObject.GetComponent<NPCController>();
        npc.DisplayDeathEffect();
        Destroy (coll.gameObject);
    }
}

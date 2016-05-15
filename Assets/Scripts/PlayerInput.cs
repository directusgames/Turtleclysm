using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInput : MonoBehaviour
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

	private Vector2 m_initialTouch;
	public float m_speed = 1f;
    public float currentDepth;
    public float originDepth;
    
    private float curAngle;
    
    private ParticleSystem.EmissionModule em;
    
    private bool facingRight;
    
    public float angle;
    


	// Use this for initialization
	void Start ()
	{
        curAngle = 0;
        //To control bubbles emission, need to get the emitter
        em = bubbles.emission;
        em.enabled = false;
        
        originDepth = m_playerPos.position.y;
		//m_speed = 5f;
		m_cooldownLength = 2.3f;
		m_cooldown = false;
        
        facingRight = true;
        
	}

	void Update() {
    
        currentDepth = originDepth - m_playerPos.position.y;
        debugText.text = "Velocity: " + m_rigidBody.velocity;
        
		if (m_cooldown) {
			if (m_timeWaited >= m_cooldownLength) {
				m_cooldown = false;
                
                em.enabled = false;
                
                anim.SetBool ("idle", true);
                anim.SetBool ("slowingDown", false);
                anim.CrossFade ("Idle", 0f);
			} else {
				m_timeWaited += Time.deltaTime;
                if(m_timeWaited >= m_cooldownLength / 3)
                {
                    anim.SetBool ("propelled", false);
                    anim.SetBool ("slowingDown", true);
                }
			}
		}
        
        //testing
//        if(Input.GetKey (KeyCode.DownArrow))
//        {
//            currentDepth += 0.1f;
//        }
    }

	// Update is called once prior to each 'physics step'.
	void FixedUpdate ()
	{
		if (!m_cooldown) {
			//Debug.Log ("cooldown disegaged");
            TouchInput();
            
		} else {
			//Debug.Log ("cooldown engaged");
		}
	}

	void TouchInput() {
		if (Input.touchCount == 1) {
            em.enabled = true;
			Touch currentTouch = Input.GetTouch(0);
			Vector2 currentPos = m_playerPos.position;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(currentTouch.position);
			Vector2 diffPos = new Vector2(worldPos.x, worldPos.y) - currentPos;
            angle = Mathf.Atan2(diffPos.y, diffPos.x) * Mathf.Rad2Deg;			
            
            m_rigidBody.velocity = diffPos.normalized * m_speed;
            
            turtleImage.transform.Rotate(0,0,angle - curAngle);
            curAngle = angle;
            
            
            Debug.Log ("Rotate to: " + angle);

            
            anim.SetBool ("slowingDown", false);
            anim.SetBool ("idle", false);
            anim.SetBool ("propelled", true);
            anim.CrossFade ("Propelling", 0f);
            
			m_cooldown = true;
			m_timeWaited = 0;
            
            FlipRotation();
            
            //turtleImage.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    	}
	}
    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "npc")
        {
            anim.CrossFade("Chomp", 0f);
            NPCController npc = coll.gameObject.GetComponent<NPCController>();
            npc.DisplayDeathEffect();
            Destroy (coll.gameObject);
            
        }
    }
    
    void FlipRotation()
    {
        if((m_rigidBody.velocity.x > 0 && m_rigidBody.velocity.y > 0) || (m_rigidBody.velocity.x > 0 && m_rigidBody.velocity.y < 0))
        {
            Debug.Log ("Pressed on the right");
            if(!facingRight)
            {
                turtleImage.localScale = new Vector3(turtleImage.localScale.x, -turtleImage.localScale.y, turtleImage.localScale.z);
                Debug.Log ("Turtle wasn't facing right");
                facingRight = true;
            }
        }
        else
        {
            if(facingRight)
            {
                Debug.Log ("Pressed on the left");
                //Debug.Log ("Turtle was facing right");
                //Quaternion newRot = new Quaternion(turtleImage.transform.rotation.x, -turtleImage.transform.rotation.y, turtleImage.transform.rotation.z, -turtleImage.transform.rotation.w);
                //transform.eulerAngles = new Vector3(0f,180f,0f);
                //turtleImage.transform.eulerAngles = Vector3.Reflect(turtleImage.transform.rotation.eulerAngles, Vector3.up);
                turtleImage.localScale = new Vector3(turtleImage.localScale.x, -turtleImage.localScale.y, turtleImage.localScale.z);
                facingRight = false ;
            }
            //                else
            //                {
            //                    turtleImage.transform.eulerAngles = new Vector3(0,180,turtleImage.transform.rotation.eulerAngles.z);
            //                }
        }
    }
}

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
    
    public bool touchHeld;
    
    private float angle;
    public float curveSpeed;


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
        
        if(!m_cooldown) {
            //Debug.Log ("cooldown disegaged");
            if(!touchHeld)
            {
                MoveTurtle();
            }
            else
            {
                if(Input.touchCount == 1)
                {
                    if(Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        touchHeld = false;
                    }   
                }
            }
            
        }        
		else if (m_cooldown) {
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
    }

	// Update is called once prior to each 'physics step'.
	void FixedUpdate ()
	{
        if(m_cooldown)
        {
            CurveTurtle();
        }
	}

	void MoveTurtle() {
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
            
 
            anim.SetBool ("slowingDown", false);
            anim.SetBool ("idle", false);
            anim.SetBool ("propelled", true);
            anim.CrossFade ("Propelling", 0f);
            
			m_cooldown = true;
			m_timeWaited = 0;
            
            FlipRotation();
            
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
    
    //If the turtle is moving left, we need to flip the sprite orientation so that the turtle remains the 'right way up'
    void FlipRotation()
    {
        //If turtle moves right
        if(m_rigidBody.velocity.x > 0 )
        {
            //if turtle was not already facing right, flip them
            if(!facingRight)
            {
                turtleImage.localScale = new Vector3(turtleImage.localScale.x, -turtleImage.localScale.y, turtleImage.localScale.z);
                facingRight = true;
            }
        }
        //Else if turtle moves left
        else
        {
            //if turtle was not already facing left, flip them
            if(facingRight)
            {
                turtleImage.localScale = new Vector3(turtleImage.localScale.x, -turtleImage.localScale.y, turtleImage.localScale.z);
                facingRight = false ;
            }
        }
    }
    
    void CurveTurtle()
    {
        if (Input.touchCount == 1)
        {
            touchHeld = true;
            Touch currentTouch = Input.GetTouch(0);
            Vector2 currentPos = m_playerPos.position;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(currentTouch.position);
            
            //worldPos.Normalize();
            
            Vector2 diffPos = worldPos - m_playerPos.position;
            diffPos.Normalize();
            
            angle = Mathf.Atan2(diffPos.y, diffPos.x) * Mathf.Rad2Deg;
            float ang = Vector2.Angle(turtleImage.transform.right, diffPos);
            //angle = Mathf.Atan2(diffPos.y, diffPos.x) * Mathf.Rad2Deg;
            //Debug.Log (Vector2.Angle(new Vector2(worldPos.x,worldPos.y), new Vector2(turtleImage.transform.up.x, turtleImage.transform.up.y)));
//            Debug.DrawLine (m_playerPos.position, m_playerPos.position + (diffPos*100f));
//            Debug.DrawLine (m_playerPos.position, turtleImage.transform.position + (turtleImage.transform.right * 100f));
            Vector3 cross = Vector3.Cross (turtleImage.transform.right, diffPos);
            
            if(cross.z > 0)
            {
                ang = 360 - ang;
            }
            
            if(facingRight)
            {
                if(ang > 0 && ang < 180)
                {
                    //Force down
                    //Debug.Log ("Push down!");
                    //m_rigidBody.AddForce(-transform.up * (m_cooldownLength - m_timeWaited) * curveSpeed);
                    //m_playerPos.position += (-turtleImage.transform.up * curveSpeed);
                    //m_rigidBody.velocity += new Vector2(-turtleImage.transform.up.x, -turtleImage.transform.up.y) * curveSpeed;
                    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y - (turtleImage.transform.up.y*curveSpeed));
                   
                }
                else
                {
                    //Force up
                    //Debug.Log ("Push up!");
                    //m_rigidBody.velocity = diffPos.normalized * m_speed;
                    //m_rigidBody.AddForce(transform.up * (m_cooldownLength - m_timeWaited) * curveSpeed);
                    //m_playerPos.position += (turtleImage.transform.up * curveSpeed);
                    //m_rigidBody.velocity += new Vector2(turtleImage.transform.up.x, turtleImage.transform.up.y) * curveSpeed;
                    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y + (turtleImage.transform.up.y*curveSpeed));
                }
            }
            else
            {
                if(ang > 0 && ang < 180)
                {
                    //Force up
                    //Debug.Log ("Push up!");
                    //m_rigidBody.AddForce(transform.up * (m_cooldownLength - m_timeWaited) * curveSpeed);
                    //m_playerPos.position += (turtleImage.transform.up * curveSpeed);
                    //m_rigidBody.velocity += new Vector2(-turtleImage.transform.up.x, -turtleImage.transform.up.y) * curveSpeed;
                    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y - (turtleImage.transform.up.y*curveSpeed));
                }
                else
                {
                    //Force Down
                    //Debug.Log ("Push down!");
                    //m_rigidBody.AddForce(-transform.up * (m_cooldownLength - m_timeWaited) * curveSpeed);
                    //m_playerPos.position += (-turtleImage.transform.up * curveSpeed); 
                    //m_rigidBody.velocity += new Vector2(turtleImage.transform.up.x, turtleImage.transform.up.y) * curveSpeed;
                    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y + (turtleImage.transform.up.y*curveSpeed));
                }
            }   
                        
        }
        else
        {
            touchHeld = false;
        }
    }
    

}

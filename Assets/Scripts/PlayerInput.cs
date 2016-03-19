using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public Player m_player;
	public float m_speed;
	public Transform turtleImage;
	public Rigidbody2D m_rigidBody;
    public Animator anim;

	public bool m_cooldown; // True: in cooldown, False: not in cooldown.
	public float m_cooldownLength; // Length of cooldown time.
	public float m_timeWaited; // Time the user has waited.

	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;

	public bool m_debugKeyboard;

	// Use this for initialization
	void Start ()
	{
		//m_speed = 5f;
		m_cooldownLength = 2.3f;
		m_cooldown = false;
	}

	void Update() {
		if (m_cooldown) {
			if (m_timeWaited >= m_cooldownLength) {
				m_cooldown = false;
			} else {
				m_timeWaited += Time.deltaTime;
			}
	    }
    }

	// Update is called once prior to each 'physics step'.
	void FixedUpdate ()
	{
		if (m_debugKeyboard) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			m_rigidBody.velocity = new Vector3 (
				moveHorizontal * m_speed,
				moveVertical * m_speed,
				0f
			);
		}

		if (!m_cooldown) {
			Debug.Log ("cooldown disegaged");
			swipeInput();
		} else {
			Debug.Log ("cooldown engaged");
		}
	}

	void swipeInput() {
		// http://forum.unity3d.com/threads/swipe-in-all-directions-touch-and-mouse.165416.
		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch (0);
			if (t.phase == TouchPhase.Began) {
				//save began touch 2d point
				firstPressPos = new Vector2 (t.position.x, t.position.y);
			}
			if (t.phase == TouchPhase.Ended) {
				//save ended touch 2d point
				secondPressPos = new Vector2 (t.position.x, t.position.y);
				
				//create vector from the two points
				currentSwipe = new Vector3 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y, 0);
			
				//normalize the 2d vector
				currentSwipe.Normalize ();
				
				bool swipe = false;
				
				//swipe upwards
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipe = true;
					// Debug.Log ("up swipe");
				}
				//swipe down
				if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipe = true;
					// Debug.Log ("down swipe");
				}
				//swipe left
				if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipe = true;
					// Debug.Log ("left swipe");
				}
				//swipe right
				if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipe = true;
					// Debug.Log ("right swipe");
				}
				
				if (swipe) {
					m_timeWaited = Time.deltaTime;
					m_cooldown = true;
					m_rigidBody.velocity = new Vector3 (
						currentSwipe.x * m_speed,
						currentSwipe.y * m_speed,
						0f
					);
					swipe = false;
				}
			}
		}
	}
}

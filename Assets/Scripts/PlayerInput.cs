using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public Player m_player;
	public float m_speed;
	public Transform m_playerPos;
	public Rigidbody2D m_rigidBody;

	// Use this for initialization
	void Start () {
		m_speed = 5f;
	}
	
	// Update is called once prior to each 'physics step'.
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		m_rigidBody.velocity = new Vector3(
			moveHorizontal * m_speed,
			moveVertical * m_speed,
			0f
		);
		// if (Input.GetKeyDown(KeyCode.W)) {
		// }
	}
}

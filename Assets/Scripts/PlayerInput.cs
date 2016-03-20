﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public Transform m_playerPos;
	public Player m_player;
	public Transform turtleImage;
	public Rigidbody2D m_rigidBody;
    public Animator anim;

	public bool m_cooldown = false; // True: in cooldown, False: not in cooldown.
	public float m_cooldownLength = 2.3f; // Length of cooldown time.
	private float m_timeWaited = 0f; // Time the user has waited.

	private Vector2 m_initialTouch;
	public float m_speed = 1f;
	public float m_distanceThreshold = 115f;

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
		if (!m_cooldown) {
			Debug.Log ("cooldown disegaged");
			swipeInput();
		} else {
			Debug.Log ("cooldown engaged");
		}
	}

	void swipeInput() {
		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			Vector2 currentPos = m_playerPos.position;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(currentTouch.position);
			Vector2 diffPos = new Vector2(worldPos.x, worldPos.y) - currentPos;
			m_rigidBody.velocity = diffPos.normalized * m_speed;
			m_cooldown = true;
			m_timeWaited = 0;
		}
	}
}

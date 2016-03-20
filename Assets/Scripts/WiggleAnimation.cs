using UnityEngine;
using System.Collections;

public class WiggleAnimation : MonoBehaviour {

	private Transform m_fishBody;

	public bool m_animate = true; // Whether to animate fish wiggle.

	public bool m_wiggleLeft = true;

	public bool m_wiggleCooldown = false;
	public float m_wiggleTime; // Time between rotations.
	public float m_wiggleItr; // Current time expired between cooldowns.

	public float m_angleLeft = 10f;
	public float m_angleRight = -10f;

	void Start() {
		m_fishBody = this.GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {
		if (m_animate) {
			if (m_wiggleCooldown) {
				if (m_wiggleItr >= m_wiggleTime) {
					m_wiggleCooldown = false;
				} else {
					m_wiggleItr += Time.deltaTime;
				}
			} else {
				if (m_wiggleLeft) {
					m_wiggleItr = 0f;
					m_fishBody.Rotate(new Vector3(0f, 0f, m_angleLeft));
					m_wiggleCooldown = true;
					m_wiggleLeft = false;
					m_fishBody.Rotate(new Vector3(0f, 0f, 0));
				} else { // Wiggle right.
					m_wiggleItr = 0f;
					m_fishBody.Rotate(new Vector3(0f, 0f, m_angleRight));
					m_wiggleCooldown = true;
					m_wiggleLeft = true;
					m_fishBody.Rotate(new Vector3(0f, 0f, 0));
				}
			}
		}
	}
}

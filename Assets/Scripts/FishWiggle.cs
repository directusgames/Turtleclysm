using UnityEngine;
using System.Collections;

public class FishWiggle : MonoBehaviour {

	public Transform m_fishBody;
	public bool m_wiggleLeft = true;
	public float m_wiggleRate = 0.1f;
	public float m_leftWiggle = -5f;
	public float m_rightWiggle = 5f;

	// Use this for initialization
	void Start () {
		m_fishBody.eulerAngles = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_wiggleLeft) {
			// Hasn't finished wiggling left.
			Debug.Log (m_fishBody.rotation.eulerAngles.z);
			if (m_fishBody.eulerAngles.z >= m_leftWiggle) {
				// Wiggle more to the left.
				m_fishBody.eulerAngles = new Vector3(
						m_fishBody.eulerAngles.x,
						m_fishBody.eulerAngles.y,
						m_wiggleRate * -1
				);
			} else {
				m_wiggleLeft = false;
			}
		} else { // Wiggle right.
			// Hasn't finished wiggling left.
			if (m_fishBody.eulerAngles.z <= m_leftWiggle) {
				// Wiggle more to the left.
				m_fishBody.eulerAngles = new Vector3(
					m_fishBody.eulerAngles.x,
					m_fishBody.eulerAngles.y,
					m_wiggleRate
				);
			} else {
				m_wiggleLeft = true;
			}
		}
	}
}

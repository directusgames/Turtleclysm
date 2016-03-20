using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public float m_scale; // Size / scale.

	// If true, always a relatively small NPC, if false, always a relatively large NPC.
	public bool m_alwaysSmall;

	private Transform m_transform;

	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		m_transform.localScale = m_transform.localScale * m_scale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void changeScale(float scale) {
		m_transform.localScale = m_transform.localScale * scale;
		m_scale = scale;
	}
}

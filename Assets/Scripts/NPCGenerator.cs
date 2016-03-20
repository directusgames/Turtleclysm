using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCGenerator : MonoBehaviour {

	public List<GameObject> m_npcList;

	// Use this for initialization
	void Start () {
		if (m_npcList.Count == 0) {
			populatePrefabs();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void populatePrefabs() {
		m_npcList = new List<GameObject>();
		Object[] temp = Resources.LoadAll("Prefabs/NPCs"); 
		for (int itr = 0; itr < temp.Length; ++itr) {
			m_npcList.Add((GameObject) temp.GetValue(itr));
		}
	}
}

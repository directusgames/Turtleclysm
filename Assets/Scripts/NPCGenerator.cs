using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCGenerator : MonoBehaviour {

	public List<GameObject> m_npcList;
    public List<Transform> spawnLocations;
    
    public float spawnInterval;
    
    private float currentTime;

	public bool m_spawn = true;

	//public int m_spawnPosItr;
	//public Vector3[] m_spawnPositions;

	// Use this for initialization
	void Start () {
    
        currentTime = 0;
//		if (m_npcList.Count == 0) {
//			populatePrefabs();
//		}

//		m_spawnPosItr = 0;
//		m_spawnPositions = new Vector3[] {
//			new Vector3(11f, 7f, -1f),
//			new Vector3(11f, -7f, -1f),
//			new Vector3(-11f, 7f, -1f),
//			new Vector3(-11f, -7f, -1f)
//		};
	}
	
	// Update is called once per frame
	void Update () {
    
        currentTime += Time.deltaTime;
        
        if(currentTime >= spawnInterval)
        {
            m_spawn = true;
            currentTime = 0;
        }
    
        if(m_spawn)
        {
            m_spawn = false;
            //Get random spawn location
            int spawnLocIndex = Random.Range (0, spawnLocations.Count);
            
            //Get random enemy;
            int npcIndex = Random.Range (0, m_npcList.Count);
            
            //Instantiate enemy
            GameObject npc = (GameObject) Instantiate(m_npcList[npcIndex], spawnLocations[spawnLocIndex].position, spawnLocations[spawnLocIndex].rotation);
            npc.transform.position = new Vector3(npc.transform.position.x, npc.transform.position.y, -1f);
            
           
        }
        
//		if (m_spawn) {
//			for (int itr = 0; itr < m_npcList.Count; ++itr) {
//				GameObject go = (GameObject) Instantiate(
//					m_npcList[itr],
//					m_spawnPositions[m_spawnPosItr],
//					Quaternion.identity
//				);
//				if (m_spawnPosItr < 3) {
//					++m_spawnPosItr;
//				} else {
//					m_spawnPosItr = 0;
//				}
//			}
//			m_spawn = false;
//		}
	}

//	void populatePrefabs() {
//		m_npcList = new List<GameObject>();
//		Object[] temp = Resources.LoadAll("Prefabs/NPCs"); 
//		for (int itr = 0; itr < temp.Length; ++itr) {
//			m_npcList.Add((GameObject) temp.GetValue(itr));
//		}
//	}
}

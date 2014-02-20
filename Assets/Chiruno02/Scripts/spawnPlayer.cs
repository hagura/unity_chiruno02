using UnityEngine;
using System.Collections;

public class spawnPlayer : MonoBehaviour {


	public GameObject player;


	void Awake () {

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnPlayer() {

		Vector3 _pos = new Vector3(Random.Range(0f,20f) - 10f,1,Random.Range(0f,20f) - 10f);
		Instantiate(player,_pos,Quaternion.identity);
	}
}

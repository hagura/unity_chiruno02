using UnityEngine;
using System.Collections;

public class spawnEnemy : MonoBehaviour {


	public GameObject enemy;
	public GameObject enemyBoss;


	public int TIME_SPAWN = 5;
	public int NUMBER_SPAWN = 10;


	void Awake () {

	}

	// Use this for initialization
	void Start () {
	
		//TEST
//		SpawnEnemy(new Vector3(0,2,0));

		//TEST
		InvokeRepeating("SpawnMulti",1,TIME_SPAWN);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnMulti() {

		for (int i=0; i<NUMBER_SPAWN; i++) {
			Spawn();
		}
	}

	void Spawn () {

		Vector3 _pos = new Vector3(Random.Range(0f,20f) - 10f,10,Random.Range(0f,20f) - 10f);
		Instantiate(enemy,_pos,Quaternion.identity);
	}

	public void SpawnBoss () {

		Vector3 _pos = new Vector3(Random.Range(0f,20f) - 10f,20,Random.Range(0f,20f) - 10f);
		Instantiate(enemyBoss,_pos,Quaternion.identity);
	}
}

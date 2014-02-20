using UnityEngine;
using System.Collections;

public class spawnRubble : MonoBehaviour {


	public GameObject rubble;


	void Awake () {

//		string _path = AssetDatabase.GetAssetPath("rubble");
//		Debug.Log(_path);

		//		rubble	= GameObject.Find("rubble");//BAD
		//		rubble	= GameObject.Find("rubblePrefab");//BAD
	}

	// Use this for initialization
	void Start () {
	
		//TEST
//		SpawnRubble();

		//TEST
//		SpawnRubbleMulti(5,new Vector3(0,10,0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnRubbleMulti (int _number, Vector3 _pos) {

		while (true) {

			Vector3 _pos_random = new Vector3(_pos.x + Random.Range(0.0f,1.0f) - 0.5f,
			                                  _pos.y,
			                                  _pos.z + Random.Range(0.0f,1.0f) - 0.5f);
			SpawnRubble(_pos_random);

			_number--;
			if (_number <=0) {
				break;
			}
		}
	}

	public void SpawnRubble (Vector3 _pos) {

		//GameObject _obj = Instantiate(rubble,_pos,Quaternion.identity) as GameObject;
		Instantiate(rubble,_pos,Quaternion.identity);

		///TODO set rubble-color
		//_obj.GetComponent<>().SetColor();
	}
}

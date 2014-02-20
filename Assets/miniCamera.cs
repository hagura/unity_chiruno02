using UnityEngine;
using System.Collections;

public class miniCamera : MonoBehaviour {


	public GameObject player;


	void Awake () {

		player = GameObject.Find("player");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 _pos		= player.transform.position;
		_pos.y = 20;

		transform.position	= _pos;
	}
}

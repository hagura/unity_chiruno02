using UnityEngine;
using System.Collections;

public class behaviorRubble : MonoBehaviour {

	public float WAIT_DESTROY = 10;

	public float POWER_EXPLOSION = 10;

	// Use this for initialization
	void Start () {
	
		Invoke("DestroySelf", WAIT_DESTROY);

		Vector3 _power = new Vector3(Random.Range(0f,POWER_EXPLOSION) -POWER_EXPLOSION/2,
		                             Random.Range(0f,POWER_EXPLOSION) -POWER_EXPLOSION/2,
		                             Random.Range(0f,POWER_EXPLOSION) -POWER_EXPLOSION/2);
		rigidbody.velocity = _power;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DestroySelf () {

		Destroy(gameObject);
	}
}

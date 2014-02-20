using UnityEngine;
using System.Collections;

public class behaviorBullet : MonoBehaviour {


	const int STRONG_DEFAULT = 10;


	private int m_strong;


	void Awake () {

		m_strong	= STRONG_DEFAULT;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision _col) {

		m_strong--;
		if (m_strong<=0) {
			Destroy(gameObject);
		}
	}

	public void SetStrong (int _strong) {

		m_strong	= _strong;
	}

}

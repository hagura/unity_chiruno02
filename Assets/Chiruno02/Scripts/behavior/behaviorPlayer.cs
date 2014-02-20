using UnityEngine;
using System.Collections;

public class behaviorPlayer : MonoBehaviour {


	public GameObject bullet;


	const int POWER_SHOOT_MAX = 100;
	public float POWER_MOVE = 200;
	public float POWER_JUMP = 5;
	const int WAIT_SHOOT = 5;
	const float SPEED_BULLET = 50;


//	private float angleTurrent = 0;
	public int m_powerShoot;
	private int waitShoot;
	private bool m_onGround;
//	private bool m_onJump;


	void Awake () {

		//bullet = (GameObject)Resources.Load("Assets/Prefabs/iBullet.prefab");//BAD
		//bullet = (GameObject)Resources.Load("Assets/Prefabs/iBullet");//BAD
		//bullet = Resources.Load("Assets/Prefabs/iBullet.prefab") as GameObject;//BAD

//		bullet	= GameObject.Find("iBullet");//BAD
	}


	// Use this for initialization
	void Start () {

		m_powerShoot		= POWER_SHOOT_MAX;

//		m_onJump		= false;
		m_onGround		= false;
	}

	void FixedUpdate () {

		if (Input.GetKeyDown("z")) {
			Warp();
		}

		if (Input.GetKey("space")) {

			if (m_powerShoot >= POWER_SHOOT_MAX) {
				waitShoot		= WAIT_SHOOT;
				m_powerShoot		= 0;

				Shoot ();
			}
		}

		if (m_onGround) {
			{
				float _angle;
				Vector3 _move;
				transform.rotation.ToAngleAxis(out _angle,out _move);
				_move = Vector3.forward;
				_move *= POWER_MOVE * Time.deltaTime * Input.GetAxis("Vertical");
				_move = transform.TransformDirection(_move);
				rigidbody.velocity = _move;
			}
			{
				Vector3 _rot = new Vector3(0,1,0);
				_rot *= Input.GetAxis("Horizontal");
				Quaternion _deltaRotation = Quaternion.Euler(_rot);
				rigidbody.MoveRotation(rigidbody.rotation * _deltaRotation);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (m_powerShoot < POWER_SHOOT_MAX) {
			m_powerShoot++;
		}

		if (waitShoot > 0) {
			waitShoot--;
		}
	}

	public void Shoot () {
//		Debug.Log(this.name+":Shoot()");

		//powerShoot
		GameObject _gunfire = GameObject.Find("player/gunfire");

		GameObject _obj = Instantiate(bullet, _gunfire.transform.position, this.transform.rotation) as GameObject;
		//GameObject _obj = Instantiate(Resources.Load("Assets/Prefabs/iBullet.prefab"), _gunfire.transform.position, this.transform.rotation) as GameObject;//BAD
		//GameObject _obj = Instantiate(Resources.Load("Prefabs/iBullet"), _gunfire.transform.position, this.transform.rotation) as GameObject;//BAD

//			_obj.GetComponent<behaviorBullet>().SetStrong(2);

		Vector3 _direction = (_gunfire.transform.position - transform.position).normalized;
		_obj.rigidbody.velocity = _direction * SPEED_BULLET;

	}

	public void Warp () {

		Vector3 _pos	= new Vector3(Random.Range(0,100f) - 100f/2,
		                           3,
		                           Random.Range(0,100f) - 100f/2);

		transform.position	= _pos;

		transform.rotation	= Quaternion.identity;

//		rigidbody.velocity += new Vector3(0,POWER_JUMP,0);
		
//		rigidbody.MoveRotation(Quaternion.identity);//DUMMY
		
//		m_onJump	= true;


	}

	public int GetPowerShootMax () {

		return POWER_SHOOT_MAX;
	}

	public int GetPowerShoot () {

		return m_powerShoot;
	}

	void OnCollisionEnter (Collision _col) {
		
		if (_col.gameObject.tag == "stage"
		    || _col.gameObject.tag == "enemy"
		    || _col.gameObject.tag == "rubble") {
			m_onGround	= true;
		} else {
			m_onGround	= false;
		}
	}

}

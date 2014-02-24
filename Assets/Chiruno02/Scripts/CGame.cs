using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour {


	public GameObject player;
	public GameObject textScore;
	public GameObject spawnerEnemy;
	public GameObject textClear;
	public GameObject server;

	public Texture2D texPowerBarFull;
	public Texture2D texPowerBarEmpty;


	const int SCORE_BROKEN_ENEMY = 100;
	const int SCORE_BROKEN_PLAYER = 500;
	public int CONDITION_SCORE_BOSS = 500;


	public enum GAMEMODE {
		MODE_TITLE,
		MODE_GAME,
		MODE_CLEAR,
		MODE_GAMEOVER,

		MODE_MAX
	};

	private GAMEMODE iMode = GAMEMODE.MODE_TITLE;
	private GAMEMODE iModeNext = GAMEMODE.MODE_TITLE;

	private int m_score;
	private bool m_isSpawnBoss;
	private float m_powerBar;


	void Awake () {

		player			= GameObject.Find("player");
		textScore		= GameObject.Find("textScore");
		spawnerEnemy	= GameObject.Find("spawnerEnemy");
		textClear		= GameObject.Find("textClear");
		server			= GameObject.Find("ServerScripts");
	}

	// Use this for initialization
	void Start () {

		InitGame();

		textScore.guiText.text = System.String.Format("score:{0:D5}",m_score);

		//TEST
//		behaviorPlayer _com = GameObject.Find("player").GetComponent<behaviorPlayer>();
//		_com.Shoot();

		//TEST
//		GameObject.Find("player").GetComponent<behaviorPlayer>().Shoot();
	}

	void FixedUpdate () {

		float _power_max	= player.GetComponent<behaviorPlayer>().GetPowerShootMax();
		float _power		= player.GetComponent<behaviorPlayer>().GetPowerShoot();

		m_powerBar = _power / _power_max;

		switch (iMode) {
		case GAMEMODE.MODE_CLEAR:
			if (Input.anyKeyDown) {
				ChangeMode( GAMEMODE.MODE_TITLE );
			}
			break;
		default:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {


		/*
		checkGarbagePlayer();
		checkGarbageEnemy();
		checkGarbageBullet();
		checkGarbageRubble();
*/
	
		switch (iMode) {
		case GAMEMODE.MODE_TITLE:
			break;
		case GAMEMODE.MODE_GAME:
			break;
		case GAMEMODE.MODE_CLEAR:
			break;
		case GAMEMODE.MODE_GAMEOVER:
			break;
		default:
			break;
		}
	}

	public void InitGame () {

		m_score = 0;

		m_isSpawnBoss	= false;

		textClear.SetActive(false);


		//DUMMY
		Vector3 _pos	= new Vector3();
		Vector3 _rot	= new Vector3();
		server.GetComponent<CServer>().Add(_pos,_rot);//DUMMY
	}

	public void AddScore (int _score) {

		m_score += _score;
		textScore.guiText.text	= System.String.Format("score:{0:D5}",m_score);

		if (m_score >= CONDITION_SCORE_BOSS) {
			if (!m_isSpawnBoss) {
				spawnerEnemy.GetComponent<spawnEnemy>().SpawnBoss();
				m_isSpawnBoss	= true;
			}
		}
	}

	public void ChangeMode (GAMEMODE _mode) {

		iModeNext	= _mode;

		switch (iModeNext) {
		case GAMEMODE.MODE_TITLE:
			InitGame();
			break;
		case GAMEMODE.MODE_CLEAR:
			textClear.SetActive(true);
			break;
		}

		iMode		= iModeNext;
	}


	/*
	void checkGarbagePlayer () {

	}

	void checkGarbageEnemy () {

	}

	void checkGarbageRubble () {

	}

	void checkGarbageBullet () {

	}

	void spawnPlayer () {

	}

	void spawnEnemy () {

	}
	*/

	private void OnGUI () {

		//	GUI.BeginGroup(new Rect(20,Screen.height/2 +50,20,300));
		//	GUI.Box (Rect (0,0, 20, 300),healthBarFull);
		GUI.BeginGroup(new Rect(20,50,20,100));
		GUI.Box(new Rect(0,0, 20, 100),texPowerBarFull);
		
		//	GUI.BeginGroup (new Rect (0, 0, 20, 300 * barDisplay)); //barDisplayが0.1なら300の10%がダメージとなり塗りつぶされる
		//	GUI.Box (Rect (0,0, 20, 300),healthBarEmpty);
		GUI.BeginGroup (new Rect (0, 0, 20, 100 - 100 * m_powerBar)); //barDisplayが0.1なら300の10%がダメージとなり塗りつぶされる
		GUI.Box ( new Rect (0,0, 20, 100),texPowerBarEmpty);
		GUI.EndGroup ();
		GUI.EndGroup ();

	}

}

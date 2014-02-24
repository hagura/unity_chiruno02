using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class CServer : MonoBehaviour {


	const string URL_SERVER = "http://192.168.1.44:8081";
//	const string URL_SERVER = "http://joker.luna.ddns.vc:8081";
	const string URL_SERVER_ADD = URL_SERVER + "/add";
	const string URL_SERVER_SYNC = URL_SERVER + "/sync";
	const string URL_SERVER_REMOVE = URL_SERVER + "/remove";


	public float TIMER_START_SYNC = .01f;
	public float TIMER_REPEAT_SYNC = .5f;


	private string m_session_id = null;
	private long m_session_count = 0;
	private long m_session_time = 0;
	private Vector3 m_pos	= new Vector3(0.0f,0.0f,0.0f);
	private Vector3 m_rot	= new Vector3(0.0f,0.0f,0.0f);
	private bool m_shoot	= false;


	public class Player {

		public string	session_id;
		public long		session_time;
		public Vector3	pos;
		public Vector3	rot;
		public bool		shoot;

		public Player(string _session_id) {

		}

		public void Update(Vector3 _pos, Vector3 _rot, bool _shoot, long _session_time) {
			pos		= _pos;
			rot		= _rot;
			shoot	= _shoot;
			session_time	= _session_time;
		}
	}

	private ArrayList m_list;



	void Awake () {

	}

	// Use this for initialization
	void Start () {

		m_list	= new ArrayList();

		StartCoroutine("Sync");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Add (Vector3 _pos, Vector3 _rot) {

		m_pos	= _pos;
		m_rot	= _rot;

		StartCoroutine("_add");
	}
	
	IEnumerator _add () {

		WWWForm form = new WWWForm();
		form.AddField("pos_x", m_pos.x.ToString());
		form.AddField("pos_y", m_pos.y.ToString());
		form.AddField("pos_z", m_pos.z.ToString());
		form.AddField("rot_x", m_rot.x.ToString());
		form.AddField("rot_y", m_rot.y.ToString());
		form.AddField("rot_z", m_rot.z.ToString());
		WWW www = new WWW(URL_SERVER_ADD, form);
		
		yield return www;

		// もし、何らかのエラーがあったら
		if(!string.IsNullOrEmpty(www.error)){
			// エラー内容を表示
			Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
			yield break; // コルーチンを終了
		}

		// webサーバからの内容を文字列変数に格納
		string json	= www.text; 
		Debug.Log("json:"+json);//DEBUG
		
		IList data	= (IList)Json.Deserialize(json);
		foreach(IDictionary result in data){
			bool status	= (bool)result["status"];
//			Debug.Log("status:"+status.ToString());//DEBUG

			long session_count	= (long)result["session_count"];
//			Debug.Log("session_count:" + session_count.ToString());//DEBUG
			m_session_count	= session_count;

			string session_id	= (string)result["session_id"];
//			Debug.Log("session_id:"+session_id);//DEBUG
			m_session_id	= session_id;

			Debug.Log("m_session_id:"+m_session_id);//DEBUG

			long session_time	= (long)result["session_time"];
//			Debug.Log("session_time:"+session_time.ToString());//DEBUG
			m_session_time	= session_time;

			IList list_pos = (IList)result["pos"];
			IList list_rot = (IList)result["rot"];
			bool shoot = (bool)result["shoot"];

			float pos_x	= 0.0f;
			float pos_y	= 0.0f;
			float pos_z	= 0.0f;
			float.TryParse((string)list_pos[0], out pos_x);
			float.TryParse((string)list_pos[1], out pos_y);
			float.TryParse((string)list_pos[2], out pos_z);
			m_pos	= new Vector3(pos_x,pos_y,pos_z);

			float rot_x = 0.0f;
			float rot_y = 0.0f;
			float rot_z = 0.0f;
			float.TryParse((string)list_rot[0], out rot_x);
			float.TryParse((string)list_rot[1], out rot_y);
			float.TryParse((string)list_rot[2], out rot_z);
			m_rot	= new Vector3(rot_x,rot_y,rot_z);

		}
	}

	IEnumerator Sync () {

		while (true) {
			if (m_session_id != null) {
				StartCoroutine("GetJSON");
			}
			yield return new WaitForSeconds(TIMER_REPEAT_SYNC);
		}
	}

	IEnumerator GetJSON () {
		//		Debug.Log("GetJSON");
		
		// webサーバへアクセス
		WWWForm form = new WWWForm();
		form.AddField("session_id", m_session_id);
		form.AddField("session_count", m_session_count.ToString());
		form.AddField("pos_x", m_pos.x.ToString());
		form.AddField("pos_y", m_pos.y.ToString());
		form.AddField("pos_z", m_pos.z.ToString());
		form.AddField("rot_x", m_rot.x.ToString());
		form.AddField("rot_y", m_rot.y.ToString());
		form.AddField("rot_z", m_rot.z.ToString());
		form.AddField("flag_shoot", m_shoot.ToString());
		WWW www = new WWW(URL_SERVER_SYNC, form);
		// webサーバから何らかの返答があるまで停止
		
		yield return www;

		// もし、何らかのエラーがあったら
		if(!string.IsNullOrEmpty(www.error)){
			// エラー内容を表示
			Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
			yield break; // コルーチンを終了
		}
		
		// webサーバからの内容を文字列変数に格納
		string json = www.text; 
		Debug.Log("json:"+json);//DEBUG
		
		IList data	= (IList)Json.Deserialize(json);
		foreach(IDictionary result in data){
			bool status	= (bool)result["status"];
			if (status) {

				long session_count	= (long)result["session_count"];
				m_session_count	= session_count;
				
				string session_id	= (string)result["session_id"];
				
				long session_time	= (long)result["session_time"];
				m_session_time	= session_time;
				
				IList list_pos = (IList)result["pos"];
				IList list_rot = (IList)result["rot"];
				bool shoot	= (bool)result["shoot"];
				
				float pos_x	= 0.0f;
				float pos_y	= 0.0f;
				float pos_z	= 0.0f;
				float.TryParse((string)list_pos[0], out pos_x);
				float.TryParse((string)list_pos[1], out pos_y);
				float.TryParse((string)list_pos[2], out pos_z);
				m_pos	= new Vector3(pos_x,pos_y,pos_z);
				
				float rot_x = 0.0f;
				float rot_y = 0.0f;
				float rot_z = 0.0f;
				float.TryParse((string)list_rot[0], out rot_x);
				float.TryParse((string)list_rot[1], out rot_y);
				float.TryParse((string)list_rot[2], out rot_z);
				m_rot	= new Vector3(rot_x,rot_y,rot_z);
				
				long player_count	= (long)result["player_count"];

				ArrayList list_sync = new ArrayList();
				IList list_player = (IList)result["player_list"];
				foreach (IDictionary player in list_player) {

					list_pos = (IList)result["pos"];
					list_rot = (IList)result["rot"];

					Player player_update = new Player((string)player["session_id"]);
					pos_x	= 0.0f;
					pos_y	= 0.0f;
					pos_z	= 0.0f;
					float.TryParse((string)list_pos[0], out pos_x);
					float.TryParse((string)list_pos[1], out pos_y);
					float.TryParse((string)list_pos[2], out pos_z);
					Vector3 pos = new Vector3(pos_x,pos_y,pos_z);

					rot_x = 0.0f;
					rot_y = 0.0f;
					rot_z = 0.0f;
					float.TryParse((string)list_rot[0], out rot_x);
					float.TryParse((string)list_rot[1], out rot_y);
					float.TryParse((string)list_rot[2], out rot_z);
					Vector3 rot = new Vector3(rot_x,rot_y,rot_z);
					player_update.Update(pos,rot,(bool)player["shoot"],(long)player["session_time"]);

					list_sync.Add(new Player((string)player["session_id"]));
				}
				m_list	= list_sync;
			}

		}

	}

	private void OnApplicationQuit () {

		Remove();
	}

	public void Remove () {

		if (m_session_id != null) {
			StartCoroutine("_remove");
		}
	}

	IEnumerator _remove () {

		WWWForm form = new WWWForm();
		form.AddField("session_id", m_session_id);
		WWW www = new WWW(URL_SERVER_REMOVE, form);
		
		yield return www;
		
		// もし、何らかのエラーがあったら
		if(!string.IsNullOrEmpty(www.error)){
			// エラー内容を表示
			Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
			yield break; // コルーチンを終了
		}

		m_session_id	= null;

		// webサーバからの内容を文字列変数に格納
		string json = www.text; 
		Debug.Log("json:"+json);//DEBUG
		
		IList data = (IList)Json.Deserialize(json);
		foreach(IDictionary result in data){
			string status = (string)result["status"];
			Debug.Log("status:"+status);//DEBUG
		}
	}

}

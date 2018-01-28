// https://qiita.com/oishihiroaki/items/bb2977c72052f5dd5bd9

using UnityEngine;
using System.Collections;
using System;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine.SceneManagement;

public class socket_test: MonoBehaviour {

	[System.Serializable]
	public class Person_Data{
		public string Name, Message;
	}

	WebSocket ws;

	public GameObject yoko_prefab;
	private bool loggingIn;
	private bool meThere = true;
	private bool othersThere = false;

	private bool addMeButton = false;
	private bool hideMeButton = false;
	private bool changeMotionButton = false;
	private bool displayOthersButton = false;
	private bool hideOthersButton = false;
	private int buttonNo = 0;

	public int flg;
	public string pname;

	void Start()
	{
		flg = 0;
		//Login状態をかくにん
		if (!UserAuth.getUserId ().IsNullOrEmpty) {
			loggingIn = true;
		} else {
			loggingIn = false;
		}

		if (loggingIn) {
			ws = new WebSocket ("ws://localhost:8080/ws");
			//ws = new WebSocket("ws://billboard-wsserver.herokuapp.com/ws");
			ws.OnOpen += (sender, e) => {
				Debug.Log ("WebSocket Open");
			};

			ws.OnMessage += (sender, e) => {
				Debug.Log (e.Data);
				Person_Data recieve_message = new Person_Data ();
				recieve_message = JsonUtility.FromJson<Person_Data> (e.Data);
				//Debug.Log(recieve_message.Name);
				pname = recieve_message.Name;
				flg = 1;
			};

			ws.OnError += (sender, e) => {
				Debug.Log ("WebSocket Error Message: " + e.Message);
			};

			ws.OnClose += (sender, e) => {
				Debug.Log ("WebSocket Close");
			};

			ws.Connect ();
		} else {
			logout ();
		}

	}

	void Update()
	{
		//addMeボタンによる処理/サーバへのメッセージ送信
		if (addMeButton){
			//string send_message = ChangeJson ();

			Person_Data mydata = new Person_Data ();
			mydata.Name = "Yoko";
			mydata.Message = "1 to 10 !!";

			string send_message = JsonUtility.ToJson (mydata);
			Debug.Log (send_message);

			ws.Send(send_message);
			addMeButton = false;
		}
		//サーバからメッセージを受け取ったあとでaddMeを実際に行う
		if (flg == 1) {
			if (pname == "Yoko") {
				UnityEngine.Quaternion appear_rotation = UnityEngine.Quaternion.identity;
				Instantiate (yoko_prefab, new Vector3 (UnityEngine.Random.Range (-2.0f, 2.0f), 1.5f, -1.5f), appear_rotation);
			} else {
			}
			flg = 0;
		}

		//logoutを検知した時
		if (!loggingIn) {
			logout ();
		}
	}

	void OnGUI(){
		//Logoutボタンは常に表示
		if(GUI.Button( new Rect(Screen.width*1/2 + 40, Screen.height*1/4 - 10, 60, 20), "Log Out" ) {
			loggingInt = false;
		}
			if(GUI.Button(new 

	}
		
	void appear_person ()
	{
		Vector3 appear_position = new Vector3 (-2.0f, 1.5f, -1.5f);
		UnityEngine.Quaternion appear_rotation = UnityEngine.Quaternion.identity;
		Instantiate(yoko_prefab, appear_position, appear_rotation);
	}
	
	void OnDestroy()
	{
		ws.Close();
		ws = null;
	}

	void logout (){
		SceneManager.LoadScene ("Login",LoadSceneMode.Single);
	}

	void drawAddHideMe(){
		//自分がすでに表示されてるとき
		//if (meThere) {
			// ボタンの設置
			int btnW = 150, btnH = 50;
			GUI.skin.button.fontSize = 20;
			addMeButton = GUI.Button( new Rect(Screen.width*1/2 - btnW - 10, Screen.height*3/4 + btnH*1/3, btnW, btnH), "Add Me" );
			hideMeButton = GUI.Button( new Rect(Screen.width*1/2 + btnW + 10, Screen.height*3/4 + btnH*1/3, btnW, btnH), "Hide Me" );
		//}
	}

	void drawChangeMotion(){
		// ボタンの設置
		int btnW = 180, btnH = 50;
		GUI.skin.button.fontSize = 20;
		changeMotionButton = GUI.Button( new Rect(Screen.width*1/2 - btnW * 1/2, Screen.height*3/4 + btnH*1/3, btnW, btnH), "Change Motion" );

	}

	void showHideOthers(){
		//他人がすでに表示されてるとき
		if (othersThere) {
			// ボタンの設置
			int btnW = 180, btnH = 50;
			GUI.skin.button.fontSize = 20;
			hideOthersButton = GUI.Button (new Rect (Screen.width * 1 / 2 + btnW * 1 / 2, Screen.height * 3 / 4 + btnH * 1 / 3, btnW, btnH), "Hide Others");
		} else {
			//他人が表示されていない時
			int btnW = 180, btnH = 50;
			GUI.skin.button.fontSize = 20;
			displayOthersButton = GUI.Button( new Rect(Screen.width*1/2 - btnW * 1/2, Screen.height*3/4 + btnH*1/3, btnW, btnH), "Display others" );
		}
	}



}



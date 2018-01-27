// https://qiita.com/oishihiroaki/items/bb2977c72052f5dd5bd9

using UnityEngine;
using System.Collections;
using System;
using WebSocketSharp;
using WebSocketSharp.Net;

public class socket_test: MonoBehaviour {

	[System.Serializable]
	public class Person_Data{
		public string Name, Message;
	}

	WebSocket ws;

	public GameObject yoko_prefab;

	public int flg;
	public string pname;

	void Start()
	{
		flg = 0;

		ws = new WebSocket("ws://localhost:8080/ws");
		//ws = new WebSocket("ws://billboard-wsserver.herokuapp.com/ws");
		ws.OnOpen += (sender, e) =>
		{
			Debug.Log("WebSocket Open");
		};

		ws.OnMessage += (sender, e) =>
		{
			Debug.Log(e.Data);
			Person_Data recieve_message = new Person_Data ();
			recieve_message = JsonUtility.FromJson<Person_Data>(e.Data);
			//Debug.Log(recieve_message.Name);
			pname = recieve_message.Name;
			flg = 1;
		};

		ws.OnError += (sender, e) =>
		{
			Debug.Log("WebSocket Error Message: " + e.Message);
		};

		ws.OnClose += (sender, e) =>
		{
			Debug.Log("WebSocket Close");
		};

		ws.Connect();

	}

	void Update()
	{

		if (Input.GetKeyUp("s"))
		{
			//string send_message = ChangeJson ();

			Person_Data mydata = new Person_Data ();
			mydata.Name = "Yoko";
			mydata.Message = "1 to 10 !!";

			string send_message = JsonUtility.ToJson (mydata);
			Debug.Log (send_message);

			ws.Send(send_message);
		}

		if (flg == 1) {
			if (pname == "Yoko") {
				UnityEngine.Quaternion appear_rotation = UnityEngine.Quaternion.identity;
				Instantiate (yoko_prefab, new Vector3 (UnityEngine.Random.Range (-2.0f, 2.0f), 1.5f, -1.5f), appear_rotation);
			} else {
			}
			flg = 0;
		}

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
}



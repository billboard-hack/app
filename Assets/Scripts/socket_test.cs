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

	void Start()
	{
		ws = new WebSocket("ws://localhost:8080/");

		ws.OnOpen += (sender, e) =>
		{
			Debug.Log("WebSocket Open");
		};

		ws.OnMessage += (sender, e) =>
		{
			Debug.Log(e.Data);
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
			mydata.Message = "1->10 !!";

			string send_message = JsonUtility.ToJson (mydata);
			Debug.Log (send_message);

			ws.Send(send_message);
		}

	}

	/*
	string ChangeJson()
	{
		Person_Data mydata = new Person_Data ();
		mydata.Name = "Yoko";
		mydata.Message = "1->10 !!";

		string message = JsonUtility.ToJson (mydata);
		return message;
		Debug.Log ("message");
	}
	*/
	
	void OnDestroy()
	{
		ws.Close();
		ws = null;
	}
}



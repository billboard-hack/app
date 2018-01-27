// https://qiita.com/oishihiroaki/items/bb2977c72052f5dd5bd9

using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;

public class socket_test: MonoBehaviour {

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
			ws.Send("message");
		}

	}

	void OnDestroy()
	{
		ws.Close();
		ws = null;
	}
}

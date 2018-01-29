using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using WebSocketSharp.Net;




public class LoginManager : MonoBehaviour {
	[System.Serializable]
	public class LoginData{
		public string Name, Message;
	}

	//private GameObject guiTextLogin;
	private GameObject loginFailureText;

	private bool loginFailure;
	private bool loginButton;
	private string id = "";
	private string pw = "";
	private WebSocket ws;
	private string person;
	private bool login = false;


	// Use this for initialization
	void Start () {
		//guiTextLogin = GameObject.FindGameObjectWithTag ("GUITextLogIn");
		loginFailureText = GameObject.FindGameObjectWithTag ("Failure");
		//guiTextLogin.SetActive (true);
		//初めはログイン失敗のメッセージ表示させない
		loginFailureText.SetActive (false);

		//初めログインボタンは押されていない状態
		loginButton = false;
		//初めは常に成功
		loginFailure = false;

		ws = new WebSocket("ws://localhost:8080/ws");
		//ws = new WebSocket("ws://billboard-wsserver.herokuapp.com/ws");
		ws.OnOpen += (sender, e) =>
		{
			Debug.Log("WebSocket Open");
		};

		ws.OnMessage += (sender, e) =>
		{
			Debug.Log(e.Data);
			LoginData recieve_message = new LoginData ();
			recieve_message = JsonUtility.FromJson<LoginData>(e.Data);
			if(recieve_message.Name =="tomo" && recieve_message.Message == "tomo"){
				Debug.Log("Login:succeeded");
				UserAuth.setUserId(recieve_message.Name);
				Debug.Log("LoginUser:" + UserAuth.getUserId());
				login = true;
			}
			else {
				loginFailure = true;
			}
			person = recieve_message.Name;

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

	void Update() {
		if (login) {
			SceneManager.LoadScene ("test",LoadSceneMode.Single);
		}
	}

	void OnGUI(){//再生中GUIをずっと見張るメソッド
		if (loginFailure == false) {
			loginFailureText.SetActive (false);
		} else {
			loginFailureText.SetActive (true);
		}
		drawLoginMenu ();

		//ログインボタンが押された時の処理
		if (loginButton) {
			//ここでユーザ情報をサーバとやりとりwith socket
			LoginData data = new LoginData ();
			data.Name = id;
			data.Message = pw;
			string send_message = JsonUtility.ToJson (data);
			Debug.Log (send_message);

			ws.Send (send_message);

			if(loginFailure){
				loginFailureText.SetActive (true);
			}
		}
	}

	void drawLoginMenu(){
		// テキストボックスの設置と入力値の取得
		GUI.skin.textField.fontSize = 20;
		int txtW = 150, txtH = 40;
		id = GUI.TextField     (new Rect(Screen.width*1/2 - txtW* 1/8, Screen.height*2/5+ txtH, txtW, txtH), id);
		pw = GUI.PasswordField (new Rect(Screen.width*1/2 - txtW * 1/8, Screen.height*4/7 + txtH, txtW, txtH), pw, '*');

		// ボタンの設置
		int btnW = 180, btnH = 50;
		GUI.skin.button.fontSize = 20;
		loginButton      = GUI.Button( new Rect(Screen.width*1/2 - btnW*1/2, Screen.height*3/4 + btnH*1/3, btnW, btnH), "Log In" );
	}

}

//ユーザーID を別のsceneから取得するためのpublic クラス
public class UserAuth : MonoBehaviour{
	private static string userId = null;

	//setter
	public static void setUserId(string id){
		userId = id;
	}

	//getter
	public static string getUserId(){
		return userId;
	}
}


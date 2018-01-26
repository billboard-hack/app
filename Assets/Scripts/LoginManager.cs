using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour {
	//private GameObject guiTextLogin;
	private GameObject loginFailureText;

	private bool loginFailure;
	private bool loginButton;
	public string id;
	public string pw;


	// Use this for initialization
	void Start () {
		//guiTextLogin = GameObject.FindGameObjectWithTag ("GUITextLogIn");
		loginFailureText = GameObject.FindGameObjectWithTag ("Failure");
		//guiTextLogin.SetActive (true);
		//初めはログイン失敗のメッセージ表示させない
		loginFailureText.SetActive (false);

		//初めログインボタンは押されていない状態
		loginButton = false;
		//成功した時だけ失敗メッセージを消す
		loginFailure = true;

	}
	
	void OnGUI(){//再生中GUIをずっと見張るメソッド
		
		loginFailureText.SetActive (false);

		drawLoginMenu ();

		//ログインボタンが押された時の処理
		if (loginButton) {
			//ここでユーザ情報をサーバとやりとりwith socket


			if (loginFailure == false) {
				//ログイン成功。scene切り替える
				SceneManager.LoadScene ("test");
			} else {
				loginFailureText.SetActive (true);
			}
				
		}

	}

	void drawLoginMenu(){
		// テキストボックスの設置と入力値の取得
		GUI.skin.textField.fontSize = 20;
		int txtW = 150, txtH = 40;
		id = GUI.TextField     (new Rect(Screen.width*1/2, Screen.height*1/3 - txtH*1/2, txtW, txtH), id);
		pw = GUI.PasswordField (new Rect(Screen.width*1/2, Screen.height*1/2 - txtH*1/2, txtW, txtH), pw, '*');

		// ボタンの設置
		int btnW = 180, btnH = 50;
		GUI.skin.button.fontSize = 20;
		loginButton      = GUI.Button( new Rect(Screen.width*1/4 - btnW*1/2, Screen.height*3/4 - btnH*1/2, btnW, btnH), "Log In" );
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


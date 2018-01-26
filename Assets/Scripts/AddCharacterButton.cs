using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCharacterButton : MonoBehaviour {

	void OnGUI () {
		// ボタンを表示する
		if (GUI.Button (new Rect (20, 20, 100, 50), "Add Me")) {
			Debug.Log ("New Character Created");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_message : MonoBehaviour {

	Text myText;
	Canvas mycanvas;
	Rigidbody parents_body;

	// Use this for initialization
	void Start () {
		myText = GetComponentInChildren<Text>();//UIのテキストの取得の仕方
		myText.text = "Shiba";//テキストの変更
		mycanvas = GetComponent<Canvas>(); 
		parents_body = GetComponentInParent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("j")) {
			myText.text = "Good Job!";
		}
		if(Input.GetKeyUp("h")) {
			myText.text = "Hello!!";
		}

		// to fix the rotation of text (error)

		//mycanvas.transform.Rotate (new Vector3 (0, 1, 0), 0); 
	}


}

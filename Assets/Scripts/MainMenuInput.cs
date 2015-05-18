using UnityEngine;
using System.Collections;

public class MainMenuInput : MonoBehaviour {

	private bool inputEnabled = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		StartCoroutine(inputEnableCounter());

		if (inputEnabled) {
			if (Input.GetKeyUp(KeyCode.Escape)) {
				Application.Quit();
			}

			if (Input.GetButtonUp("Submit")) {
				Application.LoadLevel("Main");
			}
		}
	}

	IEnumerator inputEnableCounter () {
		yield return new WaitForSeconds (1);
		inputEnabled = true;
	}
}

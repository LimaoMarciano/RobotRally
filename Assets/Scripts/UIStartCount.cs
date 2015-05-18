using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStartCount : MonoBehaviour {

	private Text uiText;
	// Use this for initialization
	void Start () {
		uiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		float count = RaceService.instance.startCount;

		if (count >= 0) {
			if (count >= 2) uiText.text = "3";
			else if (count >= 1) uiText.text = "2";
			else if (count >= 0) uiText.text = "1";
		}
		else
		{
			StartCoroutine(StartCount());
		}
	}

	IEnumerator StartCount () {
		uiText.text = "START!";
		yield return new WaitForSeconds(2);
		uiText.enabled = false;
	}
}

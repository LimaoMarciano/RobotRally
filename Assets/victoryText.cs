using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class victoryText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (RaceService.instance.isAnyRacerFinished()) {
			string raceLeaderName = RaceService.instance.GetRacerNamebyPositon(1);
			GetComponent<Text>().enabled = true;
			GetComponent<Text>().text = raceLeaderName + " won the race!";
		}
	}
}

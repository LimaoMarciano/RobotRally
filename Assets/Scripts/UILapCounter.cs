using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILapCounter : MonoBehaviour {

	private Text uiText;
	public int playerNumber;
	// Use this for initialization
	void Start () {
		uiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		int totalLaps = RaceService.instance.raceLapsQuantity;
		int currentLap = RaceService.instance.getRacerPositionByNumber(playerNumber);
		currentLap += 1;
		if (currentLap > totalLaps)
			currentLap = totalLaps;
		uiText.text = currentLap + "/" + totalLaps;
	}
}

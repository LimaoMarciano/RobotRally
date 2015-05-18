using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DashBarBehavior : MonoBehaviour {

	public GameObject target;
	private Image barFill;
	private RobotController robotController;
	// Use this for initialization
	void Start () {
		barFill = GetComponent<Image>();
		robotController = target.GetComponent<RobotController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//barFill.fillAmount = 0.5f;
		barFill.fillAmount = robotController.GetDashEnergyLevel() / 100;
	}
}

using UnityEngine;
using System.Collections;

public class ParticlesBehavior : MonoBehaviour {

	public GameObject robot;
	private RobotController robotController;
	// Use this for initialization
	void Start () {
		robotController = robot.GetComponent<RobotController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (robotController.isGrounded) {
			GetComponent<ParticleSystem>().enableEmission = true;
		}
		else
		{
			GetComponent<ParticleSystem>().enableEmission = false;
		}
	}
}

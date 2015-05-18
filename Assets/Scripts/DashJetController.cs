using UnityEngine;
using System.Collections;



public class DashJetController : MonoBehaviour {

	public GameObject robot;
	public ParticleSystem particles;

	private RobotController robotController;

	enum FacingDirection {
		UP = 270,
		DOWN = 90,
		LEFT = 180,
		RIGHT = 0
	}

	// Use this for initialization
	void Start () {
		robotController = robot.GetComponent<RobotController>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 inputDirection = robotController.GetInputDirection();

		inputDirection += new Vector2(transform.position.x, transform.position.y);

		LookAt2D(inputDirection,17f,FacingDirection.RIGHT);

		if (robotController.dashReleased) {
			particles.Play();
		}
	}

	void LookAt2D(Vector3 theTarget, float theSpeed, FacingDirection facing) {
		Vector3 vectorToTarget = theTarget - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		angle -= (float)facing;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * theSpeed);
	}
}

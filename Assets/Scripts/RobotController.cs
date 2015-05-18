using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	public GameObject robotEngine;
	public Transform groundSensor;
	public bool isGrounded;
	public LayerMask groundLayer;
	public float Speed = 50f;
	public int Player = 1;
	private JointMotor2D jointMotor;

	private bool isJumpPressed = false;
	private bool isJumpIncreaseAllowed = false;
	private bool isJumpJustPressed = false;
	private int jumpTimer = 0;
	private int jumpTimerLimit = 20;

	private bool isDashJustPressed = false;
	private bool isDashPressed = false;
	private float dashCharge = 0;
	public bool dashReleased = false;

	private float dashEnergy = 100f;
	private float dashConsumption = 30f;
	private float dashRecoveryRatio = 0.1f;

	private float inputHoriz;
	private float inputVert;

	private string inputHorizontal;
	private string inputVertical;
	private string inputJump;
	private string inputDash;
	private string inputBrake;

	public bool isInputActivated = true;

	void Awake () {
		switch (Player)
		{
		case 1:
			inputHorizontal = "Horizontal1";
			inputVertical = "Vertical1";
			inputJump = "Jump1";
			inputDash = "Dash1";
			inputBrake = "Brake1";

		break;
		
		case 2:
			inputHorizontal = "Horizontal2";
			inputVertical = "Vertical2";
			inputJump = "Jump2";
			inputDash = "Dash2";
			inputBrake = "Brake2";
		break;
		}
	}

	// Use this for initialization
	void Start () {
		jointMotor.maxMotorTorque = 1000f;
	}

	// Update is called once per frame
	void Update () {

		if (isInputActivated) {

			if (Input.GetButtonDown(inputJump)) {
				isJumpJustPressed = true;
			}

			if (Input.GetButton(inputJump)) {
				isJumpPressed = true;
			}
			else
			{
				isJumpPressed = false;
			}

			if (Input.GetButtonUp(inputJump)) {
				isJumpIncreaseAllowed = false;
			}

			if (Input.GetButton(inputDash)) {
				isDashPressed = true;
			}
			else
			{
				isDashPressed = false;
			}

			if (Input.GetButtonDown(inputDash)) {
				isDashJustPressed = true;
			}
		}
	}

	void FixedUpdate () {


		//Check if wheel is touching the ground
		isGrounded = Physics2D.OverlapCircle(groundSensor.position,0.480f,groundLayer);

		//Get input
		inputHoriz = Input.GetAxis(inputHorizontal);
		inputVert = Input.GetAxis(inputVertical);

		if (isInputActivated) {

			//Acceleration
			if (inputHoriz != 0 && isGrounded) {
				robotEngine.GetComponent<WheelJoint2D>().useMotor = true;
				jointMotor.motorSpeed = -inputHoriz * Speed;
				robotEngine.GetComponent<WheelJoint2D>().motor = jointMotor;
			}
			else
			{
				//Air control
				robotEngine.GetComponent<WheelJoint2D>().useMotor = false;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(200 * inputHoriz, 0));
			}

			//Brakes
			if (Input.GetButton(inputBrake)) {
				robotEngine.GetComponent<WheelJoint2D>().useMotor = true;
				jointMotor.motorSpeed = 0;
				robotEngine.GetComponent<WheelJoint2D>().motor = jointMotor;
			}


			//Jump Logic
			if (isJumpJustPressed && isGrounded) {
				print ("Jumped!");
				robotEngine.GetComponent<Rigidbody2D>().velocity = new Vector2(robotEngine.GetComponent<Rigidbody2D>().velocity.x, 6);
				isJumpIncreaseAllowed = true;
				jumpTimer = 0;
			}

			if (isJumpIncreaseAllowed && isJumpPressed) {
				robotEngine.GetComponent<Rigidbody2D>().velocity = new Vector2(robotEngine.GetComponent<Rigidbody2D>().velocity.x, 6);
				jumpTimer += 1;
				if (jumpTimer >= jumpTimerLimit) {
					jumpTimer = 0;
					isJumpIncreaseAllowed = false;
				}
			}

			//Dash Logic
			if (isDashPressed) {
				print ("Dash charging");

				if (dashEnergy > 0)
					dashCharge += 0.03f;

				if (dashCharge >= 1f) {
					dashCharge = 1f;
					print ("Dash max!!!");
				}
				else
				{
					if (dashEnergy >= 0)
						dashEnergy -= dashConsumption * 0.03f;
				}
			}
			else
			{
				if (dashCharge > 0) {
					print ("Dash released at " + dashCharge + "% power!");
					dashReleased = true;
					Vector2 dashDirection = new Vector2(inputHoriz, inputVert);
					dashDirection.Normalize();
					robotEngine.GetComponent<Rigidbody2D>().AddForce(dashDirection * 600 * dashCharge,ForceMode2D.Impulse);
					dashCharge = 0f;
				}

				dashEnergy += dashRecoveryRatio;
				if (dashEnergy >= 100)
					dashEnergy = 100;
			}
		}

		//Reset input booleans
		isJumpJustPressed = false;
		isDashJustPressed = false;
	}

	void LateUpdate () {

		dashReleased = false;

	}

	public Vector2 GetInputDirection () {
		Vector2 inputDirection = new Vector2 (inputHoriz, inputVert);
		inputDirection.Normalize();
		return inputDirection;
	}

	public float GetDashEnergyLevel () {
		return dashEnergy;
	}
}

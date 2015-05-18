using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float offsetX = 0;
	public float offsetY = 0;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (target.transform.position.x + offsetX, target.transform.position.y + offsetY, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float cameraPositionX = target.transform.position.x + offsetX;
		float cameraPositionY = target.transform.position.y + offsetY;

		Vector3 cameraPosition = new Vector3(cameraPositionX, cameraPositionY, transform.position.z);

		transform.position = Vector3.Lerp(transform.position, cameraPosition, 0.2f);

	}
}

using UnityEngine;
using System.Collections;

public class GroundSensor : MonoBehaviour {

	public LayerMask mask;
	public bool isGrounded;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		isGrounded = Physics2D.OverlapCircle(transform.position,0.405f,mask);
		print ("isGrounded: " + isGrounded);
	}
}

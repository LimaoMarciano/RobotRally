using UnityEngine;
using System.Collections;

public class PhysicalEffects : MonoBehaviour {

	public float airDragRatio = 0.07f;
	private float speed = 0;
	private float airDrag = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speed = GetComponent<Rigidbody2D>().velocity.x;
		airDrag = Mathf.Abs(speed) * airDragRatio;
		GetComponent<Rigidbody2D>().drag = airDrag;
	}
}

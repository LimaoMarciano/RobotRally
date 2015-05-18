using UnityEngine;
using System.Collections;

public class CheckpointBehavior : MonoBehaviour {

	public LayerMask targetLayer;
	public int checkpointNumber;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D go) {
		if (go.tag == "Player") {
			RaceService.instance.CheckpointReached(go.gameObject,checkpointNumber);
		}
	}
}

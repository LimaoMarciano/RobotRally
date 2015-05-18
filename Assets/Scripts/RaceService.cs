using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceService : MonoBehaviour {

	//Setting RaceService as a singleton
	private static RaceService _instance;

	public static RaceService instance
	{
		get
		{
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<RaceService>();
			return _instance;
		}

	}

	public int raceLapsQuantity = 5;
	public float finishTimerLimit = 10f;
	public float startCount = 3f;
	public List<GameObject> robotRacers;
	public List<GameObject> raceCheckpoints;
	public List<Racer> racers = new List<Racer>();

	private int checkpointsQuantity;
	private int racersQuantity;
	private int finishedRacers = 0;
	private float finishTimer = 0;

	//Class that holds race information of each robot
	public class Racer {
		public GameObject racerObject;
		public int lapNumber = 0;
		public int nextCheckpoint = 1;
		public int playerNumber;
		public int racePosition = 0;

		public Racer (GameObject gameObject,int number) {
			racerObject = gameObject;
			playerNumber = number;
		}
	}

	// Use this for initialization
	void Start () {

		//Counts how many checkpoints there're in this race
		checkpointsQuantity = raceCheckpoints.Count;
		print ("Checkpoints in this track: " + checkpointsQuantity);

		//Counts how many robots there're in this race
		racersQuantity = robotRacers.Count;
		print ("Robots in this race: " + racersQuantity);

		//Assign checkpoints number
		int c = 0;
		foreach (GameObject checkpoint in raceCheckpoints) {
			checkpoint.GetComponent<CheckpointBehavior>().checkpointNumber = c;
			c++;
		}

		//Add all players to the racers list
		foreach (GameObject go in robotRacers) {
			int n = go.GetComponent<RobotController>().Player;
			racers.Add(new Racer(go, n));
		}

		disablePlayersInput();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}

		//Race start countdown
		if (startCount >= 0) {
			startCount -= Time.deltaTime;
		}
		else
		{
			enablePlayersInput();
		}

		//If one player finished the race, a timer starts. If the timer reaches the limit, the race ends.
		if (isAnyRacerFinished()) {
			finishTimer += Time.deltaTime;
			if (finishTimer >= finishTimerLimit) {
				print ("Race time limit reached");
				Application.LoadLevel("MainMenu");
			}
		}
	}

	//This is called when some robot triggers a checkpoint
	public void CheckpointReached (GameObject target, int checkpointNumber) {

		foreach (Racer racer in racers) {
			if (racer.racerObject == target) {

				//If the robot reached his next checkpoint
				if (checkpointNumber == racer.nextCheckpoint) {

					//...and it's the first checkpoint, it means that it completed a lap.
					if (checkpointNumber == 0) {
						racer.lapNumber += 1;
						print ("Lap completed. Current lap: " + racer.lapNumber);

						//if it completed a lap and it's the last lap, it means that the robot finished the race.
						if (racer.lapNumber == raceLapsQuantity) {
							RaceCompleted(racer);
						}
					}

					//Update to the next checkpoint
					racer.nextCheckpoint += 1;

					//If the next checkpoint is above the checkpoint quantity, reset
					if (racer.nextCheckpoint == checkpointsQuantity) {
						racer.nextCheckpoint = 0;
					}
					print ("Next checkpoint: " + racer.nextCheckpoint);
				}
			}
		}
	}

	public void RaceCompleted (Racer racer) {
		print (racer.racerObject.name + " completed the race!");
		finishedRacers += 1;
		racer.racePosition = finishedRacers;

		//If all racers finished the race
		/*
		if (finishedRacers == racersQuantity) {
			Application.LoadLevel("MainMenu");
		}*/

	}

	public string GetRacerNamebyPositon (int position) {
		foreach (Racer racer in racers) {
			if (racer.racePosition == position) {
				string racerName = racer.racerObject.name;
				return racerName;
			}
		}
		return null;
	}

	public bool isAnyRacerFinished () {
		if (finishedRacers > 0) {
			return true;
		}
		else
			return false;
	}

	public void disablePlayersInput () {
		foreach (Racer racer in racers) {
			racer.racerObject.GetComponent<RobotController>().isInputActivated = false;
		}
	}

	public void enablePlayersInput () {
		foreach (Racer racer in racers) {
			racer.racerObject.GetComponent<RobotController>().isInputActivated = true;
		}
	}

	public int getRacerPositionByNumber (int playerNumber) {
		foreach (Racer racer in racers) {
			if (racer.playerNumber == playerNumber) {
				return racer.lapNumber;
			}
		}
		return 0;
	}
}

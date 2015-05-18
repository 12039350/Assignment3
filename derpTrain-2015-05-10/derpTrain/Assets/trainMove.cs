using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class trainMove : MonoBehaviour {
	Vector3 direction;
	float speed;
	float turn = 7.5f;
	public static List<Transform> carriageLine = new List<Transform>();
	private bool spawnBuffer = false;
	public GameObject carriagePrefab;
	private float timeBuffered = 0f;

	// Use this for initialization
	void Start () {
		direction = Vector3.left;
		speed = 2f;
	}

	void spawn(){
		Camera camera = Camera.main;
		Vector3 mousePos = camera.ScreenToWorldPoint( Input.mousePosition );
		mousePos.z = 0;
		GameObject other = Instantiate (carriagePrefab, mousePos, Quaternion.identity) as GameObject;
		Transform followTarget = carriageLine.Count == 0 ? transform : carriageLine[carriageLine.Count-1];
		other.GetComponent<CarriageMove>().move( followTarget, speed, turn );
		carriageLine.Add(other.transform);
		spawnBuffer = true;
		timeBuffered = 0f;
		
	}

	
	// Update is called once per frame
	void Update () {


		GameObject nextTrack = FindNextTrackSegment ();


		print (nextTrack.name);
		Vector3 currentPos = transform.position;
		Vector3 destination = nextTrack.transform.position;

		direction =	destination - currentPos;
		direction.z = 0;
		direction.Normalize ();
		Vector3 target = direction * speed + transform.position;
		transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime);
		float facingAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(0, 0, facingAngle), turn*Time.deltaTime);

		//Carriage spawn
		if (spawnBuffer) {
			timeBuffered += Time.deltaTime;
			if (timeBuffered > 0.5f){
				spawnBuffer = false;
			}
		}
		if (Input.GetButton ("Fire1") && !spawnBuffer) {
			spawn ();
		}
	}

	GameObject FindNextTrackSegment(){
		GameObject[] tracks;

		tracks = GameObject.FindGameObjectsWithTag ("track");
		GameObject next = tracks[0];
		float closestDist = Mathf.Infinity;
		Vector3 searchPos1 = transform.position + (float) 0.001 * direction ;

		foreach (GameObject track in tracks) {
			float trackDist = (track.transform.position - searchPos1).sqrMagnitude;
			float currentDist = (track.transform.position - transform.position).sqrMagnitude;
			if(trackDist < closestDist && trackDist < currentDist ){
				// && 
				next = track;
				closestDist = trackDist; 
			}
		}
		return next;
	}
}

using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	Vector3 direction;
	public float speed;
	public float damage;
	public float value;
	public float health;

	// Use this for initialization
	void Start () {
		direction = Vector3.left;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		GameObject nextTrack =
			FindNextTrackSegment ();
		
		
		print (nextTrack.name);
		Vector3 currentPos = transform.position;
		Vector3 destination = nextTrack.transform.position;
		
		direction =	destination - currentPos;
		direction.z = 0;
		direction.Normalize ();
		Vector3 target = direction * speed + transform.position;
		transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime);
		float facingAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(0, 0, facingAngle), 5f*Time.deltaTime);
	}
	
	GameObject FindNextTrackSegment(){
		GameObject[] tracks;
		
		tracks = GameObject.FindGameObjectsWithTag ("path");
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

	void OnBecameInvisible() {
		enemyCreator.totalAmount--;
		Main.health -= damage;
		Destroy( gameObject );
	}

}

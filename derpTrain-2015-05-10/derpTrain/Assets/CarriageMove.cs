using UnityEngine;
using System.Collections;

public class CarriageMove : MonoBehaviour {

	public float speed = 2f;
	public float turn = 0.5f;
	private Vector3 direction;
	Vector3 currentPosition;
	Vector3 moveDirection;
	Vector3 targetPosition;
	GameObject trainFollow;
	Transform followTarget;

	// Use this for initialization
	void Start () {
	}

	public void move(Transform followTarget, float moveSpeed, float turnSpeed){
		this.followTarget = followTarget;
		//speed = moveSpeed;
		turn = turnSpeed;

	}

	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;            
		Vector3 moveDirection = followTarget.position - currentPosition;
		
		//3
		float targetAngle = 
			Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp( transform.rotation, 
		                                      Quaternion.Euler(0, 0, targetAngle), 
		                                      turn * Time.deltaTime );
		
		//4
		float distanceToTarget = moveDirection.magnitude;
		if (distanceToTarget > 0)
		{
			//5
			if ( distanceToTarget > speed )
				distanceToTarget = speed;
			
			//6
			moveDirection.Normalize();
			Vector3 target = moveDirection * distanceToTarget + currentPosition;
			transform.position = 
				Vector3.Lerp(currentPosition, target, speed * Time.deltaTime);
		}
	}
}

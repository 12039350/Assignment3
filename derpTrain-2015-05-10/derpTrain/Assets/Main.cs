using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public static float health;
	public static float money;

	public float visibleHealth;

	// Use this for initialization
	void Start () {
		health = 100;
		money = 500;
	}
	
	// Update is called once per frame
	void Update () {
		visibleHealth = health;
		if (health <= 0) {
			//Switch to game over screen
		}
	}
}

using UnityEngine;
using System.Collections;

public class enemyCreator : MonoBehaviour {

	public static float waveNumber = 1f;
	public static float totalAmount = 0f;
	public GameObject Enemy1Prefab;
	public GameObject Enemy2Prefab;
	public GameObject EnemyCreatorPrefab;
	private float current1 = 1f;
	private float current2 = 1f;
	private float amount = 0f;

	public float testWaveNumber;
	public float testTotalAmount;

	// Use this for initialization
	void Start () {
		if (waveNumber > 1) {
			totalAmount++;
			Invoke ("SpawnEnemy2", 0f);
		}
		if (waveNumber > 0) {
			totalAmount++;
			Invoke ("SpawnEnemy1", 0f);
		}
	}
	
	void SpawnEnemy1(){
		amount = waveNumber * 7;
		Vector3 spawnPos = new Vector3 (1, -3, 0);
		Instantiate (Enemy1Prefab, spawnPos, Quaternion.identity);
		current1++;
		if (current1 <= amount) {
			totalAmount++;
			Invoke ("SpawnEnemy1", 1f);

		}
	}

	void SpawnEnemy2(){
		amount =  (waveNumber-1) * 4;
		Vector3 spawnPos = new Vector3 (1, -3, 0);
		Instantiate (Enemy2Prefab, spawnPos, Quaternion.identity);
		current2++;
		if (current2 <= amount) {
			totalAmount++;
			Invoke ("SpawnEnemy2", 2f);
		}
	}

	public void end(){
		Destroy( gameObject );

	}

	// Update is called once per frame
	void Update () {
		testWaveNumber = waveNumber;
		testTotalAmount = totalAmount;

		Vector3 spawnPos = new Vector3 (0, 0, 0);
		if (totalAmount == 0) {
			waveNumber++;
			Instantiate(EnemyCreatorPrefab, spawnPos, Quaternion.identity);
			end ();
		}
	}
}
		


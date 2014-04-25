using UnityEngine;

public class Booster : MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	void Update () {
		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
	
	void OnTriggerEnter () {
		Runner.AddBoost();
		gameObject.SetActive(false);
	}

	public void SpawnIfAvailable (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}

	private void GameOver () {
		gameObject.SetActive(false);
	}
}
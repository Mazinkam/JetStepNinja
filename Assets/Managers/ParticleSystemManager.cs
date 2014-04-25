using UnityEngine;

public class ParticleSystemManager : MonoBehaviour {

	public ParticleSystem[] particleSystems;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOver();
	}

	private void GameStart () {
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].Clear();
			particleSystems[i].enableEmission = true;
		}
	}

	private void GameOver () {
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].enableEmission = false;
		}
	}
}
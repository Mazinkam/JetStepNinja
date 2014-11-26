using UnityEngine;
using System.Collections;

public class CharacterAssets : MonoBehaviour {

	public GameObject[] characterMesh;
	public Material[] charMeshMaterial;

	public GameObject[] charEarMesh;
	public Material[] charEarMeshMaterial;

	void Awake()
	{
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

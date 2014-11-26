using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class Customize : MonoBehaviour {

	private CharacterAssets _charAssets;
	private PlayerCharacter _playerCharacter;

	private GameObject _earsMesh;

	private int _charMeshIndex = 0;
	private int _charMaterialIndex = 0;

	private int _earsMeshIndex = 0;
	private int _earsMeshMaterialIndex = 0;



	public enum CharacterMeshMaterial
	{
		Ears,
		Horns,
		Cheeks,
		Neck,
		Back,
		Tail,
	}
	
	// Use this for initialization
	void Start () 
	{

		_charAssets = GameObject.Find ("CharacterAssetManager").GetComponent <CharacterAssets>();
		InstantiateCharacterMesh();

		_playerCharacter = GameObject.FindWithTag ("Player").GetComponent<PlayerCharacter> ();;

		InstantiateRightEar ();                                                        
	}

	public void ButtonPressed()
	{



		InstantiateCharacterMesh();
		_playerCharacter = GameObject.FindWithTag ("Player").GetComponent<PlayerCharacter> ();;
	
		InstantiateRightEar ();
		_charMeshIndex++;
		_earsMeshIndex++;



	}

	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void InstantiateRightEar()
	{
		switch (_earsMeshIndex)
		{
		case 1:
			
			break;
		case 2:
			
			break;
		default:
			_earsMeshIndex = 0;
			
			break;
		}

		_earsMesh = (GameObject) Instantiate (_charAssets.charEarMesh [_earsMeshIndex], _playerCharacter.characterEars.transform.position, Quaternion.identity);
		_earsMesh.transform.parent = transform;
	}
	
	void InstantiateCharacterMesh()
	{
		switch (_charMeshIndex)
		{
		case 1:

			break;
		case 2:

			break;
		default:
			_charMeshIndex = 0;

			break;
		}
	

		GameObject mesh = (GameObject) Instantiate (_charAssets.characterMesh [_charMeshIndex], transform.position, Quaternion.identity) ;

		if (transform.childCount > 0) 
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
				Debug.Log(i);
			}
		}

		mesh.transform.parent = transform;
		mesh.transform.rotation = transform.rotation;
	}
	
}

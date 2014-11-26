using UnityEngine;
using System.Collections;


//remove rigidbody and change movement with lerps 
public class UmeController : MonoBehaviour
{

	enum GameMode
	{
		Normal,
		DoubleWall
	};

	enum PlayerPositionState
	{
		None,
		GoingUp,
		GoingDown,
		ReverseGoingUp,
		ReverseGoingDown
	};

	enum PlayerLocation
	{
		OnBottomFloor,
		OnTopFloor,
		InAir

	};

	public static Transform UmeTransform;

	public Vector3 JumpVelocity, LandVelocity, JumpVelocityReverse, LandVelocityReverse;
	public GameObject WaterRun, WaterJump, AirJump, AirJumpUpside, JetStep;

	private Animator _animator;

	private float _particleFix = 0;
	
	private GameMode _gMode = GameMode.Normal;
	private PlayerPositionState _pPositionState = PlayerPositionState.GoingDown;
	private PlayerLocation _pLocation = PlayerLocation.InAir;

	private GameObject _currentFloor, _bottomFloor, _topFloor;


	
	void Start()
	{
		_animator = GetComponent<Animator>();
		_animator.speed = 2f;

		if(_bottomFloor == null)
			_bottomFloor = GameObject.FindWithTag("BottomFloorTile");

		if(_topFloor == null)
			_topFloor = GameObject.FindWithTag("TopFloorTile");

		_currentFloor = _bottomFloor;

	}
	
	void Update()
	{

		HandlePlayerInput ();

		
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		

		_animator.SetFloat("Speed", Mathf.Clamp(Time.deltaTime, -1f, 1f));
		
		UpdateMoveParticles();

		Debug.Log ("_pLocation " + _pLocation + " _pPositionState " + _pPositionState);
		
	}

	void PhysicsCollisionFix()
	{
		/*
		if (_gMode == GameMode.Normal)
		{
			if (UmeTransform.position.y <= _currentFloor.transform.position.y ) //error margin of 1
			{
				_pLocation = PlayerLocation.OnBottomFloor;
			}
		}
	
		if (_gMode == GameMode.DoubleWall )
		{
			if(_pPositionState == PlayerPositionState.GoingUp || _pPositionState == PlayerPositionState.GoingDown )
			{
				if(UmeTransform.position.y <= _currentFloor.transform.position.y)
				{
					_pLocation = PlayerLocation.OnBottomFloor;
				}
				
			}
			else if(_pPositionState == PlayerPositionState.ReverseGoingDown || _pPositionState == PlayerPositionState.ReverseGoingUp)
			{
				if(UmeTransform.position.y <= _currentFloor.transform.position.y)
				{
					_pLocation = PlayerLocation.OnTopFloor;
				}
			}
		}
	*/

	}
	
	void HandlePlayerInput()
	{

		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			if(_gMode == GameMode.Normal)
			{
				if (_pLocation == PlayerLocation.OnBottomFloor )
				{
					_pPositionState = PlayerPositionState.GoingUp;
					//_pLocation = 
				}
				else if (_pLocation == PlayerLocation.InAir )
				{
					_pPositionState = PlayerPositionState.GoingDown;
					ParticleJetStep ();

				}
			}
			if(_gMode == GameMode.DoubleWall)
			{
				if (_pLocation == PlayerLocation.OnBottomFloor )
				{
					_pPositionState = PlayerPositionState.GoingUp;
				}
				else if (_pLocation == PlayerLocation.InAir && _pPositionState == PlayerPositionState.GoingUp)
				{
					_pPositionState = PlayerPositionState.ReverseGoingDown;
					ParticleJetStep ();
				}
				else if (_pLocation == PlayerLocation.OnTopFloor )
				{
					_pPositionState = PlayerPositionState.ReverseGoingUp;
				}
				else if (_pLocation == PlayerLocation.InAir   && _pPositionState == PlayerPositionState.ReverseGoingUp )
				{
					_pPositionState = PlayerPositionState.GoingDown;
					ParticleJetStep ();
				}
			}
		}
	}

	void UpdateJumpingGameMode1 ()
	{
		
		if (_pPositionState == PlayerPositionState.GoingUp && _pLocation == PlayerLocation.OnBottomFloor )
		{
				rigidbody.AddForce(JumpVelocity, ForceMode.Impulse);
				_animator.SetTrigger("Jump");		
		}
		else if (_pPositionState == PlayerPositionState.GoingDown && _pLocation == PlayerLocation.InAir)
		{
			rigidbody.AddForce(LandVelocity, ForceMode.Impulse);

		}
		
	}
		
	void UpdateJumpingGameMode2 ()
	{
		if (_pPositionState == PlayerPositionState.GoingUp && _pLocation == PlayerLocation.OnBottomFloor) 
		{
			rigidbody.AddForce (JumpVelocity, ForceMode.Impulse);
			_animator.SetTrigger ("Jump");
		}
		else if (_pPositionState == PlayerPositionState.GoingDown && _pLocation == PlayerLocation.InAir) 
		{
			rigidbody.AddForce (LandVelocity, ForceMode.Impulse);
			
		} 
		else if (_pPositionState == PlayerPositionState.ReverseGoingUp && _pLocation == PlayerLocation.OnTopFloor) 
		{
			rigidbody.AddForce (JumpVelocityReverse, ForceMode.Impulse);
			_animator.SetTrigger ("Jump");
		}
		else if (_pPositionState == PlayerPositionState.ReverseGoingDown && _pLocation == PlayerLocation.InAir) 
		{
			rigidbody.AddForce (LandVelocityReverse, ForceMode.Impulse);
		}
		
	}
	
	void UpdateMoveParticles ()
	{
		if (_pLocation == PlayerLocation.OnBottomFloor && _particleFix >= 0.2)
		{
			ParticleCreationRun();
			_particleFix = 0;
		}
		else
			_particleFix += Time.deltaTime;
	}
	
	void FixedUpdate()
	{

		UmeTransform = transform;
		PhysicsCollisionFix ();
		UpdateAnimatorSpeed ();

		if(_gMode == GameMode.Normal)
			UpdateJumpingGameMode1();

		if(_gMode == GameMode.DoubleWall)
			UpdateJumpingGameMode2();

			UpdateCustomGravity ();

		
	}

	void UpdateAnimatorSpeed()
	{
		if (_pLocation == PlayerLocation.OnBottomFloor)
		{
			_animator.speed = 2f;
		}
		else
		{
			if (_pPositionState == PlayerPositionState.GoingUp)
				_animator.speed = 0.10f;
			else
				_animator.speed = 2f;
		}

	}

	void UpdateCustomGravity()
	{
		if (_gMode == GameMode.Normal) 
		{
			if (_pLocation == PlayerLocation.InAir && _pPositionState == PlayerPositionState.GoingUp)
			{
					rigidbody.AddForce (new Vector3 (0, -3f, 0), ForceMode.VelocityChange);
			}
		}


		if (_gMode == GameMode.DoubleWall )
		{
			if(_pLocation == PlayerLocation.InAir || _pLocation == PlayerLocation.OnBottomFloor)
			{
				rigidbody.AddForce (new Vector3 (0, -3f, 0), ForceMode.VelocityChange);
				
			}
			if(_pLocation == PlayerLocation.OnTopFloor )
			{
				rigidbody.AddForce (new Vector3 (0, 6f, 0), ForceMode.VelocityChange);
			}
		}
	}
	
	public void ParticleCreationRun ()
	{
		//Set the lifetime of the particle system.
		GameObject tempParticleSystem;

		tempParticleSystem = WaterJump;

		//Set the correct position of the particle system.
		Vector3 position;
		position = new Vector3(UmeTransform.position.x-3, UmeTransform.position.y-0.5f, UmeTransform.position.z+2);

		
		GameObject tempSplashObj;
		//Create the splash and tell it to destroy itself.
		tempSplashObj = Instantiate(tempParticleSystem, position, Quaternion.identity) as GameObject;
		

		Destroy(tempSplashObj, tempSplashObj.particleSystem.startLifetime);
		
	}

	public void ParticleJetStep ()
	{
		var newObj = TrashMan.spawn( JetStep, transform.position, Quaternion.identity );
		TrashMan.despawnAfterDelay( newObj, 3 );
	}
	

	
	void OnCollisionEnter(Collision other)
	{
		//if(_pLocation == PlayerLocation.OnBottomFloor)
	//		_pPositionState = PlayerPositionState.None;

		_animator.SetBool("Landed", true);

		if (other.gameObject.tag == "BottomFloorTile")
		{
			_currentFloor = _bottomFloor;
			_pLocation = PlayerLocation.OnBottomFloor;
			_pPositionState = PlayerPositionState.None;
		}
		else if(other.gameObject.tag == "TopFloorTile")
		{
			_currentFloor = _topFloor;;
			_pLocation = PlayerLocation.OnTopFloor;
			_pPositionState = PlayerPositionState.None;
		}
		
		ParticleCreationRun();
		//	if (other.collider.tag == "Smelly")
		
	}
	
	void OnCollisionStay()
	{
	//	if(_pLocation == PlayerLocation.OnBottomFloor)
	//		_pPositionState = PlayerPositionState.None;

		_animator.SetBool("Landed", false);
		
	}
	
	void OnCollisionExit()
	{
		_pLocation = PlayerLocation.InAir;
		_animator.SetBool("Landed", false);
	}
	void OnTriggerEnter(Collider other)
	{


	}

}

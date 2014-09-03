using UnityEngine;
using System.Collections;

public class UmeController : MonoBehaviour
{
	public Camera curCam;
	
	public Vector3 jumpVelocity, landVelocity;
	public float gameOverY;
	public GameObject waterRun, waterJump, airJump, airJumpUpside;
	
	public float runnerSpeed;
	
	public static Transform UmeTransform;
	
	private bool touchingPlatform;
	private Vector3 startPosition;
	private Animator animator;
	private float speed;
	private bool goingDown;
	private float splashFix = 0;
	
	private bool canRotate = false;
	private bool gameMode2 = false;
	private Transform floor, roof;
	
	void Start()
	{
		startPosition = transform.localPosition;
	//	renderer.enabled = false;
		rigidbody.isKinematic = false;
		enabled = true;
		
		animator = GetComponent<Animator>();
		animator.speed = 10f;
		
	//	floor = GameObject.Find("BottomFloorSpawner").transform;
		
		if(GameObject.FindGameObjectWithTag("GameMode2") != null)
		{
			gameMode2 = true;
			roof = GameObject.Find("TopFloorSpawner").transform;
		}
	}
	
	void Update()
	{
		// Check if player outside
		if(gameMode2)
		{
			if(roof.position.y < transform.position.y)
			{
				transform.position = new Vector3(transform.position.x, roof.position.y + 0.2f, transform.position.z);
			}
			else if(floor.position.y > transform.position.y)
			{
				transform.position = new Vector3(transform.position.x, floor.position.y - 0.2f, transform.position.z);
			}
		}
		
		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			if (touchingPlatform)
			{
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				animator.SetTrigger("Jump");
				goingDown = false;
				touchingPlatform = false;
			}
			else if (!touchingPlatform)
			{
				// rotate player if game mode 2
				if(gameMode2 && canRotate)
				{
					if(Physics.gravity.y < 0f)
					{
						animator.SetBool("UpsideRun",true);
						Physics.gravity *= -1;
						jumpVelocity *= -1;
						landVelocity *= -1;
					}
					else
					{
						animator.SetBool("UpsideRun",false);
						Physics.gravity *= -1;
						jumpVelocity *= -1;
						landVelocity *= -1;
					}
					canRotate = false;
				}
				rigidbody.AddForce(landVelocity, ForceMode.VelocityChange);
				goingDown = true;
				
				if(gameMode2 && Physics.gravity.y < 0f)
					AirSplash(transform.position.x, 10, true);
				else
					AirSplash(transform.position.x, 10);
			}
		}
		/*
		if (!touchingPlatform)
		{
			Vector3 camCurVec = curCam.transform.position;
			float wantedZoom = camCurVec.z - 90;
          
			if (wantedZoom > -190)
				camCurVec = Vector3.Lerp(camCurVec, new Vector3(camCurVec.x, camCurVec.y, wantedZoom), Time.deltaTime);

			curCam.transform.position = camCurVec;
		}

		if (touchingPlatform)
		{
			Vector3 camCurVec = curCam.transform.position;
			float wantedZoom = camCurVec.z + 90;
         
			if (wantedZoom < 39)
				camCurVec = Vector3.Lerp(camCurVec, new Vector3(camCurVec.x, camCurVec.y, wantedZoom), Time.deltaTime);

			curCam.transform.position = camCurVec;
		}*/
		
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		
		speed = Mathf.Clamp(runnerSpeed + Time.deltaTime, -1f, 1f);
		animator.SetFloat("Speed", speed);
		
		if (transform.localPosition.y < gameOverY)
		{
			//	GameEventManager.TriggerGameOver();
		}
		
	}
	
	void FixedUpdate()
	{
		if (touchingPlatform)
		{
			animator.speed = 2f;
		}
		else
		{
			if (!goingDown)
				animator.speed = 0.10f;
			else
				animator.speed = 2f;
		}
		
		UmeTransform = transform;
		
	}
	
	public void Splash(float xpos, float velocity)
	{
		//Set the lifetime of the particle system.
		float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;
		
		//Set the splash to be between two values in Shuriken by setting it twice.
		waterRun.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
		waterRun.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
		waterRun.GetComponent<ParticleSystem>().startLifetime = lifetime;
		
		//Set the correct position of the particle system.
		Vector3 position;
		if(gameMode2 && Physics.gravity.y > 0)
			position = new Vector3(xpos, transform.position.y + 2, 0);
		else
			position = new Vector3(xpos, transform.position.y - 2, 0);
		
		//This line aims the splash towards the middle. Only use for small bodies of water:
		Quaternion rotation = Quaternion.LookRotation(new Vector3(xpos, 1 + 8, 5) - position);
		
		//Create the splash and tell it to destroy itself.
		GameObject tempSplashObj = Instantiate(waterRun, position, rotation) as GameObject;
		
		
		Destroy(tempSplashObj, lifetime / 2);
		
	}
	
	public void JumpSplash(float xpos, float velocity)
	{
		//Set the lifetime of the particle system.
		float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;
		
		//Set the splash to be between two values in Shuriken by setting it twice.
		waterJump.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
		waterJump.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
		waterJump.GetComponent<ParticleSystem>().startLifetime = lifetime;
		
		//Set the correct position of the particle system.
		Vector3 position = new Vector3(xpos, transform.position.y - 2, 0);
		
		//This line aims the splash towards the middle. Only use for small bodies of water:
		Quaternion rotation = Quaternion.LookRotation(new Vector3(xpos, 1 + 8, 5) - position);
		
		//Create the splash and tell it to destroy itself.
		GameObject tempSplashObj = Instantiate(waterRun, position, rotation) as GameObject;
		Destroy(tempSplashObj, lifetime / 2);
		
	}
	
	public void AirSplash(float xpos, float velocity, bool upside = false)
	{
		//Set the lifetime of the particle system.
		float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;
		
		//Set the splash to be between two values in Shuriken by setting it twice.
		airJump.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
		airJump.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
		airJump.GetComponent<ParticleSystem>().startLifetime = lifetime;
		
		//Set the correct position of the particle system.
		Vector3 position = new Vector3(xpos, transform.position.y - 2, 0);
		
		//This line aims the splash towards the middle. Only use for small bodies of water:
		// Quaternion rotation = Quaternion.LookRotation(new Vector3(xpos, 1 + 8, 5) - position);
		
		//Create the splash and tell it to destroy itself.
		GameObject tempSplashObj;
		if(gameMode2 && !upside)
			tempSplashObj = Instantiate(airJumpUpside, position, Quaternion.identity) as GameObject;
		else
			tempSplashObj = Instantiate(airJump, position, Quaternion.identity) as GameObject;
		Destroy(tempSplashObj, lifetime / 2);
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		canRotate = true;
		touchingPlatform = true;
		animator.SetBool("Landed", true);
		//Splash(transform.position.x, runnerSpeed);
		//	if (other.collider.tag == "Smelly")
		
	}
	
	void OnCollisionStay()
	{
		touchingPlatform = true;
		
		// gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
		if (splashFix > 0.2)
		{
			Splash(transform.position.x, runnerSpeed);
			splashFix = 0;
		}
		else
			splashFix += Time.deltaTime;
		animator.SetBool("Landed", false);

	}
	
	void OnCollisionExit()
	{
		touchingPlatform = false;
		
		JumpSplash(transform.position.x, 10);
		animator.SetBool("Landed", false);
	}
	
	private void GameStart()
	{
		transform.localPosition = startPosition;
		renderer.enabled = true;
		// rigidbody.velocity = Vector3.zero;
		
		rigidbody.isKinematic = false;
		// rigidbody.velocity = new Vector3(15, 0, 0);
		
		enabled = true;
		animator.SetFloat("Speed", 0);
		
	}
	
	private void GameOver()
	{
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
		
	}
}

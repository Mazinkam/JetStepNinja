using UnityEngine;

public class Runner : MonoBehaviour
{

	public static float distanceTraveled;
	public static float gamePoints;
	private static int boosts;
	public Camera curCam;
	public float acceleration;
	public Vector3 jumpVelocity, landVelocity;
	public float gameOverY;
	public GameObject waterRun;
	public GameObject waterJump;
	public GameObject airJump;
	private bool touchingPlatform;
	private Vector3 startPosition;
	private Animator animator;
	private float speed;
	private bool goingDown;
	private bool gameStart;
	private float splashFix = 0;
	private float runnerSpeed;
	private bool rotateMe;
	private bool canRotate = false;
    
	void Start()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = false;
		enabled = true;
 
		animator = GetComponent<Animator>();
		animator.speed = 2f;

		if(GameObject.FindGameObjectWithTag("GameMode2") != null)
			canRotate = true;
	}
    
	void Update()
	{
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
				if(canRotate)
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
				}
				rigidbody.AddForce(landVelocity, ForceMode.VelocityChange);
				goingDown = true;
				AirSplash(transform.position.x, 10);
			}
		}
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
		}
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}


		//    if(gameStart)
		//      GUIManager.SetDistance(gamePoints);

		speed = Mathf.Clamp(2 + Time.deltaTime, -1f, 1f);
		animator.SetFloat("Speed", speed);
        
		if (transform.localPosition.y < gameOverY)
		{
			GameEventManager.TriggerGameOver();
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

			//    animator.SetTrigger("Jump");
		}

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
		Vector3 position = new Vector3(xpos, transform.position.y - 2, 0);

		//This line aims the splash towards the middle. Only use for small bodies of water:
		Quaternion rotation = Quaternion.LookRotation(new Vector3(xpos, 1 + 8, 5) - position);

		//Create the splash and tell it to destroy itself.
		GameObject tempObj = Instantiate(waterRun, position, rotation) as GameObject;
		Destroy(tempObj, lifetime / 2);
        
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
		GameObject tempObj = Instantiate(waterRun, position, rotation) as GameObject;
		Destroy(tempObj, lifetime / 2);

	}

	public void AirSplash(float xpos, float velocity)
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
		GameObject tempObj = Instantiate(airJump, position, Quaternion.identity) as GameObject;
		Destroy(tempObj, lifetime / 2);

	}

	void OnCollisionEnter(Collision other)
	{
		touchingPlatform = true;
		// Splash(transform.position.x, 10);
		if (other.collider.tag == "Smelly")
			GameEventManager.TriggerGameOver();
	}

	void OnCollisionStay()
	{
		touchingPlatform = true;

		// gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
		if (splashFix > 0.2)
		{
			Splash(transform.position.x, 10);
			splashFix = 0;
		}
		else
			splashFix += Time.deltaTime;
        
	}

	void OnCollisionExit()
	{
		touchingPlatform = false;
		JumpSplash(transform.position.x, 10);

	}

	private void GameStart()
	{
		runnerSpeed = 0;
		//GUIManager.SetSpeed(runnerSpeed);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		renderer.enabled = true;
		// rigidbody.velocity = Vector3.zero;
       
		rigidbody.isKinematic = false;
		// rigidbody.velocity = new Vector3(15, 0, 0);

		enabled = true;
		animator.SetFloat("Speed", 0);
		gameStart = true;
        
	}
    
	private void GameOver()
	{
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
		gameStart = false;
	}
}
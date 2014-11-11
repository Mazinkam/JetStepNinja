using UnityEngine;
using System.Collections;


//remove rigidbody and change movement with lerps 
public class UmeController : MonoBehaviour
{
	public Vector3 jumpVelocity, landVelocity;
	public GameObject waterRun, waterJump, airJump, airJumpUpside;
	public float runnerSpeed;
	public static Transform UmeTransform;
	public Camera camera;
	
	private bool touchingPlatform, goingDown;
	private Animator animator;
	private float speed;
	private float particleFix = 0;

	private float splashFix;
	private bool isJumping;


	//Game mode 2
	private bool canRotate = false;
	private bool reversePhysics = false;
	private bool gameMode2 = false;
	private Transform floor, roof;

	void Start()
	{
		rigidbody.isKinematic = false;
		enabled = true;
		
		animator = GetComponent<Animator>();
		animator.speed = 2f;
		
		//	floor = GameObject.Find("BottomFloorSpawner").transform;
		
		if(GameObject.FindGameObjectWithTag("GameMode2") != null)
		{
			gameMode2 = true;
			roof = GameObject.Find("TopFloorSpawner").transform;
		}
	}
	
	void Update()
	{

		if(transform.position.y >= 120)
			rigidbody.velocity = new Vector3(0, -10, 0);
		
		//simulating own gravity :D
		if(transform.position.y <= -10){
			rigidbody.velocity = new Vector3(0, 0, 0); //preventing the player to go trough the floor if the colliders fail us
			transform.position = new Vector3(0, 0, 0);
			animator.SetBool("Landed", true);
			canRotate = true;
			touchingPlatform = true;
			isJumping = false;
			Debug.Log("Went through"); //temp fix for collision, needs to be redone or fine tuned
		}
		//	else
		//		rigidbody.AddForce(new Vector3(0, -0.9f, 0), ForceMode.VelocityChange);


		UpdateGameMode2();
		UpdateJumping();
		HandleZoom();
		
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		
		speed = Mathf.Clamp(runnerSpeed + Time.deltaTime, -1f, 1f);
		animator.SetFloat("Speed", speed);
		
		UpdateMoveParticles();
		
		
	}

	
	void UpdateGameMode2()
	{
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
	}

	void HandleZoom()
	{
		if(isJumping){
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,50,Time.deltaTime*3);
		}
		else{
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,30,Time.deltaTime/2);
		}
	}

	void UpdateJumping ()
	{

		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			if (touchingPlatform)
			{
				rigidbody.AddForce(jumpVelocity, ForceMode.Impulse);
				//rigidbody.velocity = jumpVelocity;
				animator.SetTrigger("Jump");
				goingDown = false;
				touchingPlatform = false;
				isJumping = true;
			}
			else if (!touchingPlatform)
			{
				// rotate player if game mode 2
				if(gameMode2 && canRotate)
				{
					if(Physics.gravity.y < 0f)
					{
						animator.SetBool("UpsideRun",true);
						reversePhysics = true;
						Physics.gravity *= -1;
						jumpVelocity *= -1;
						landVelocity *= -1;
					}
					else
					{
						animator.SetBool("UpsideRun",false);
						reversePhysics = false;
						Physics.gravity *= -1;
						jumpVelocity *= -1;
						landVelocity *= -1;
					}
					canRotate = false;
				}
				rigidbody.AddForce(landVelocity, ForceMode.Impulse);
				goingDown = true;

				
				if(gameMode2 && Physics.gravity.y < 0f)
					ParticleCreationJump();
				else
					ParticleCreationJump();
			}
		}
	}
	
	void UpdateMoveParticles ()
	{
		if (touchingPlatform && particleFix >= 0.2)
		{
			ParticleCreationRun();
			particleFix = 0;
		}
		else
			particleFix += Time.deltaTime;
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
	
	public void ParticleCreationRun ()
	{
		//Set the lifetime of the particle system.
		float lifetime = 0.93f  + Mathf.Abs(speed)  * 0.07f;
		
		GameObject tempParticleSystem;
		
		if(touchingPlatform)
			tempParticleSystem = waterRun;
		else
			tempParticleSystem = waterJump;
		

		//Set the correct position of the particle system.
		Vector3 position;
		if(gameMode2 && Physics.gravity.y > 0)
			position = new Vector3(UmeTransform.position.x+3, UmeTransform.position.y-0.5f, UmeTransform.position.z+2);
		else
			position = new Vector3(UmeTransform.position.x-3, UmeTransform.position.y-0.5f, UmeTransform.position.z+2);

		
		GameObject tempSplashObj;
		//Create the splash and tell it to destroy itself.
		if(gameMode2 && !reversePhysics)
			tempSplashObj = Instantiate(tempParticleSystem, position, Quaternion.identity) as GameObject;
		else
			tempSplashObj = Instantiate(tempParticleSystem, position, Quaternion.identity) as GameObject;
		

		Destroy(tempSplashObj, tempSplashObj.particleSystem.startLifetime);
		
	}
	
	public void ParticleCreationJump ()
	{
		//Set the lifetime of the particle system.
		float lifetime = 0.93f  + Mathf.Abs(speed) * 0.07f;
		
		GameObject tempParticleSystem;
		
		if(gameMode2 && !reversePhysics)
			tempParticleSystem = airJumpUpside;
		else
			tempParticleSystem = airJump;;
		
		//Set the correct position of the particle system.
		Vector3 position;
		if(gameMode2 && Physics.gravity.y > 0)
			position = new Vector3(UmeTransform.position.x+3, UmeTransform.position.y-0.5f, UmeTransform.position.z+2);
		else
			position = new Vector3(UmeTransform.position.x-3, UmeTransform.position.y-0.5f, UmeTransform.position.z+2);

		
		GameObject tempSplashObj;
		//Create the splash and tell it to destroy itself.
		if(gameMode2 && !reversePhysics)
			tempSplashObj = Instantiate(tempParticleSystem, position, Quaternion.identity) as GameObject;
		else
			tempSplashObj = Instantiate(tempParticleSystem, position, Quaternion.identity) as GameObject;

		Destroy(tempSplashObj, tempSplashObj.particleSystem.startLifetime);
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		canRotate = true;
		touchingPlatform = true;
		isJumping = false;
		animator.SetBool("Landed", true);

		ParticleCreationRun();
		//	if (other.collider.tag == "Smelly")
		
	}
	
	void OnCollisionStay()
	{

		if (splashFix > 0.2)
		{
			ParticleCreationRun();
			splashFix = 0;
		}
		else
			splashFix += Time.deltaTime;


		//touchingPlatform = true;

		animator.SetBool("Landed", false);
		
	}
	
	void OnCollisionExit()
	{
		touchingPlatform = false;

		animator.SetBool("Landed", false);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BasicObstacle")
		{
			rigidbody.velocity = new Vector3(0,0,0);
			animator.SetTrigger("Dead");
		}
	}
	
	private void GameStart()
	{
		rigidbody.isKinematic = false;
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

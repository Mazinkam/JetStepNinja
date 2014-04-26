using UnityEngine;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;
    public static float gamePoints;
    
	private static int boosts;

    Animator animator;
	
	public float acceleration;
	public Vector3 boostVelocity, jumpVelocity, landVelocity;
	public float gameOverY;

    public GameObject waterRun;
    public GameObject waterJump;
    public GameObject airJump;
	
	private bool touchingPlatform;
	private Vector3 startPosition;
    private float speed;
    private bool goingDown;

    private float runnerSpeed;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
        
 
        animator = GetComponent<Animator>();
        animator.speed = 2f;
       
	}
	
	void Update () {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
			if(touchingPlatform){
                if (runnerSpeed <= 40f)
                {
                    rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
                    animator.SetTrigger("Jump");
                    goingDown = false;
                }
                else
                {
                    rigidbody.AddForce(new Vector3(0,20,0), ForceMode.VelocityChange);
                    animator.SetTrigger("Jump");
                    goingDown = false;
                }
				touchingPlatform = false;
			}
			else if (!touchingPlatform )
            {
                if (runnerSpeed <= 40f)
                {
                    rigidbody.AddForce(landVelocity, ForceMode.VelocityChange);
                    goingDown = true;
                   
                }
                else
                {
                    rigidbody.AddForce(new Vector3(0, -25, 0), ForceMode.VelocityChange);
                    goingDown = true;
                    
                }
                AirSplash(transform.position.x, 10);
            }
		}
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
		distanceTraveled = transform.localPosition.x;
        gamePoints = distanceTraveled;
        GUIManager.SetDistance(gamePoints);
        //transform.rotation = Quaternion.Euler(0, 90, 0);

        
        GUIManager.SetSpeed(runnerSpeed);
        speed = Mathf.Clamp(runnerSpeed + Time.deltaTime, -1f, 1f);
        animator.SetFloat("Speed", speed);
		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
	}

	void FixedUpdate () {
        if (touchingPlatform)
        {
            if(runnerSpeed <= 40f)
                 rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
            else if (runnerSpeed <= 50f)
                rigidbody.AddForce(acceleration/2, 0f, 0f, ForceMode.Acceleration);
            else if (runnerSpeed <= 60f)
                rigidbody.AddForce(acceleration/3, 0f, 0f, ForceMode.Acceleration);
            else if (runnerSpeed <= 70f)
                rigidbody.AddForce(acceleration / 4, 0f, 0f, ForceMode.Acceleration);


            animator.speed = 2f;
        }
        else
        {
            gamePoints *= 1.1f;

            if(!goingDown)
               animator.speed = 0.10f;
            else
                animator.speed = 2f;

            animator.SetTrigger("Jump");
        }
        runnerSpeed = rigidbody.velocity.magnitude;
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
        Vector3 position = new Vector3(xpos, transform.position.y-2, 0);

        //This line aims the splash towards the middle. Only use for small bodies of water:
        Quaternion rotation = Quaternion.LookRotation(new Vector3(xpos, 1+ 8, 5) - position);

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
        Quaternion rotation = Quaternion.LookRotation(new Vector3(xpos, 1 + 8, 5) - position);

        //Create the splash and tell it to destroy itself.
        GameObject tempObj = Instantiate(airJump, position, Quaternion.identity) as GameObject;
        Destroy(tempObj, lifetime / 2);

    }



	void OnCollisionEnter (Collision other) {
		touchingPlatform = true;
       // Splash(transform.position.x, 10);
        if (other.collider.tag == "Smelly")
            GameEventManager.TriggerGameOver();
	}

    void OnCollisionStay()
    {
        touchingPlatform = true;
       
        Splash(transform.position.x, 10);
        
    }

	void OnCollisionExit () {
		touchingPlatform = false;
        JumpSplash(transform.position.x, 10);

	}

	private void GameStart () {
        runnerSpeed = 0;
        GUIManager.SetSpeed(runnerSpeed);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
        animator.SetFloat("Speed", 0);
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	public static void AddBoost(){
		boosts += 1;
        gamePoints += boosts * 100;
	}
}
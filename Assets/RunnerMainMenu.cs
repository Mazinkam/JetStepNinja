using UnityEngine;
using System.Collections;

public class RunnerMainMenu : MonoBehaviour
{

    public GameObject waterRun;
    private Animator animator;

    private float speed;
    private float splashFix = 0;

    private float runnerSpeed;

	public static bool jump = false;

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        renderer.enabled = false;
        rigidbody.isKinematic = false;
        enabled = true;
        animator = GetComponent<Animator>();
        animator.speed = 2f;
		jump = false;
    }

    void Update()
    {

        speed = Mathf.Clamp(1 + Time.deltaTime, -1f, 1f);
        animator.SetFloat("Speed", speed);
    }

	void OnControllerColliderHit(Collision collision)
	{
		if (collision.gameObject.tag == "Trigger")
		{
			Debug.Log("HIT!!!");
		}
	}

    void FixedUpdate()
    {
		if (jump) {
			animator.SetTrigger("Jump");
			animator.speed = 0.1f;
			Vector3 pos = transform.position;
			Vector3 force = new Vector3(0,18,-8);
			pos += force * Time.deltaTime;

			transform.position = pos;

		} else {
			animator.speed = 2f;

			if (splashFix > 0.1)
			{
				Splash (transform.position.x, 10);
				splashFix = 0;
			}
			else
				splashFix += Time.deltaTime;
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

    private void GameStart()
    {
 

        renderer.enabled = true;
        // rigidbody.velocity = Vector3.zero;

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
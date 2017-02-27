using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Niko Bazos(ndb9897@rit.edu)
 * This class moderates the movement of all movale objects in game. It is attached to everything that moves.
 */

public class GameMovement : MonoBehaviour
{
	//Attributes
	public Vector3 position;
	public Vector3 velocity = new Vector3(1.0f, 0.0f, 0.0f);
	public float speed = 0.0f;
	private float maxSpeed = 10.0f;
	private float slowDown = 0.97f;
	private float speedIncrement = 10.0f;

	private float angularSpeed = 90.0f;
	private float angle = 0.0f;

	private Vector3 worldSize = new Vector3(10.0f, 10.0f, 0.0f);

	public bool useSlowdown = true;

	public GameObject terrain; // takes in the terrain to be usedd

	// Use this for initialization
	void Start ()
	{
		// if no terrain is speciified
		if( null == terrain)
		{
			Debug.Log("Error: " + gameObject.name + " needs a GameObject that cointains a Terrain");
			Debug.Break();
		}

		// retrieves the worlSize
		worldSize = terrain.GetComponent<MeshRenderer>().bounds.size;

		// Asteroid Movement
		if(gameObject.tag != "Player" && gameObject.tag != "Bullet")
		{
			speed = Random.Range(0.5f, 4.0f);
			velocity = new Vector3(-1.0f, 0.0f, 0.0f);
			float halfX = worldSize.x / 2.0f;
			float halfY = worldSize.y / 2.0f;
			position = new Vector3(Random.Range(-halfX, halfX), Random.Range(-halfY, halfY), 0.0f);
			angle = Random.Range(0.0f, 360.0f);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Player movement
		if(gameObject.tag == "Player")
		{
			if(Input.GetKey(KeyCode.UpArrow))
			{
				speed += speedIncrement * Time.deltaTime;
			}
			else if(useSlowdown)
			{
				speed *= slowDown;
			}
			
			//Check max and min speed
			if(speed > maxSpeed)
			{
				speed = maxSpeed;
			}
			else if(speed < 0.01f)
			{
				speed = 0.0f;
			}
			
			//Increment angle or orientation
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				angle += angularSpeed * Time.deltaTime;
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				angle -= angularSpeed * Time.deltaTime;
			}
			
			//Keep the angle between 0 and 360
			if(angle > 360.0f)
			{
				angle -= 360.0f;
			}
			else if (angle < 0.0f)
			{
				angle += 360.0f;
			}
		}

		transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
		position += transform.rotation * velocity * speed * Time.deltaTime;

		transform.position = position;

		CheckBoundry();
	}
		

	public Vector3 GetDirection()
	{
		return Vector3.Normalize(Quaternion.Euler(0.0f, 0.0f, angle) * velocity);
	}
	
	// Screenwrap method
	void CheckBoundry()
	{	
		if (gameObject.tag != "Bullet") {
			//Check within X
			if (position.x > worldSize.x / 2.0f)
				position.x = -worldSize.x / 2.0f;
			else if (position.x < -worldSize.x / 2.0f)
				position.x = worldSize.x / 2.0f;
		
			//check within Y
			if (position.y > worldSize.y / 2.0f)
				position.y = -worldSize.y / 2.0f;
			else if (position.y < -worldSize.y / 2.0f)
				position.y = worldSize.y / 2.0f;
		}
	}


}

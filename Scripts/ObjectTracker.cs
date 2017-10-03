using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Niko Bazos(ndb9897@rit.edu)
 * This class moderates collisions between asteroids and the ship as well as the accompanuing sound.
 */

public class ObjectTracker : MonoBehaviour 
{
	// Attributes
	public List<GameObject> objects; // list of asteroids
	public GameObject player; // object to be used as player
	private int firstObj;
	private int secondObj;
	private int asteroidHit; 
	private bool isColliding;
	private Color green;
	private Color red;
	private SpriteRenderer [] spriteList;
	private int shipHealth;
	private bool hasCollided;

	public AudioSource sound;
	public AudioClip shipDead;
	public AudioClip lifeLost;

	// Use this for initialization
	void Start () 
	{
		red = new Color(1.0f,0.0f,0.0f);
		green = new Color (0.0f, 1.0f, 0.0f);
		spriteList = player.GetComponentsInChildren<SpriteRenderer>();
		shipHealth = 3;
		isColliding = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if no player lives are left
		if (shipHealth == 0) 
		{
			Destroy (player);
			sound.PlayOneShot (shipDead);
			Application.Quit ();
		} 

		// checks collision
		isColliding = CheckCollision ();
	
		if (isColliding) 
		{
			hasCollided = true;

			// Testing purposes
			/*
			objects[firstObj].GetComponent<SpriteRenderer> ().color = red;
			objects[secondObj].GetComponent<SpriteRenderer> ().color = red;

			foreach (SpriteRenderer child in spriteList) 
			{
				child.color = red;
			}
			*/

		} 

		/*
		else 
		{
			foreach (GameObject obj in objects) 
			{
				if (obj.tag == "Player") 
				{
					foreach (SpriteRenderer child in spriteList) 
					{
						child.color = green;
					}
				}

				obj.GetComponent<SpriteRenderer> ().color = green;
			}
		}
		*/

		// Decrement Ship Lives
		TakeHit ();
	}

	public int ShipHealth
	{
		get { return shipHealth;}
	}

	void TakeHit()
	{
		if (hasCollided && !isColliding) 
		{
			Debug.Log ("Collison");
			hasCollided = false;
			shipHealth--;
			sound.PlayOneShot (lifeLost);
		}
	}

	// Return whether the player is colliding with any of the asteroids
	bool CheckCollision()
	{

		for (int i = 0; i < objects.Count; i++)
		{
			for (int j = 0; j < objects.Count; j++) 
			{
				if (j == i) {
					continue;
				} 
					
				// checks if two objects are colliding
				if ((objects[i].transform.position - objects[j].transform.position).magnitude <= (objects[i].GetComponent<ObjectInformation>().radius + objects[j].GetComponent<ObjectInformation>().radius)) 
				{
					// Checks if one of those objects is the player
					if (objects[i].tag == "Player") 
					{
						firstObj = i;
						secondObj = j;
						return true;

					}
					else if (objects[j].tag == "Player") 
					{
						firstObj = j;
						secondObj = i;
						return true;
					}
				}
			}
		}

		return false;
	}
}

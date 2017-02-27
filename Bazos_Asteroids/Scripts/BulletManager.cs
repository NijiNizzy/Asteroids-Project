using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * Niko Bazos(ndb9897@rit.edu)
 * This class moderates bullets, bullet pool, score, and bullet to asteroid collisions. It is attached to the GameManager.
 */

public class BulletManager : MonoBehaviour
{

	// Attributes
	public GameObject bullet; //Object to be used as bullet
	private GameObject collBullet; // temporary variable to store the bullet fuling
	private uint bulletsFired = 0; //count of bullets fired
	private List<GameObject> bulletPool; //list of bullets currently running in the scene
	private List<float> lifePool; //list of times the bullets where shoot
	public float lifeTime = 3.0f; //amount of seconds the bullet should be alive
	public GameObject player; // Object to be used as player
	public List<GameObject> asteroids; // List of asteroid objects in game
	private bool hit; // if an asteroid to bullet collision is true or not
	private int asteroidIndex; // index of a particular asteroid in a list
	public GameObject listTrack; // access the asteroid object list in the ObjectTracker class 
	private int firstLevelScore; // the score

	// The two new asteroid prefabs spawned after one has been shot
	public GameObject asteroidPrefab;
	public GameObject asteroidPrefab2;

	// The two new asteroid GameObjects spawned after one has been shot
	private GameObject newAsteroid;
	private GameObject newAsteroid2;

	// Audio for shooting an asteroid
	public AudioSource play;
	public AudioClip asteroidDead;

	// Use this for initialization
	void Start ()
	{
		bulletPool = new List<GameObject>();//initialize the list of bullets
		lifePool = new List<float>();//initialize the list of times
	}
	
	// Update is called once per frame
	void Update ()
	{
		//check for each element in the pool if its time to free the bullet
		for(int index = 0; index < bulletsFired; ++index)
		{
			//Check the current time vs the time the bullet was shoot plus the lifeTime total
			if(Time.time > lifePool[index] + lifeTime)
			{
				//Destroy the bullet and remove the bullet and time from their lists
				Destroy(bulletPool[index]);
				lifePool.RemoveAt(index);
				bulletPool.RemoveAt(index);

				//So we do not have an index overflow and can continue using the loop
				--index;
				--bulletsFired;
			}
		}

		// Checks if a bullets collides with an asteroid
		hit = CheckHit ();

		if (hit) 
		{
			// if it collides with a second tier asteroid
			if (asteroids [asteroidIndex].tag == "Asteroid2") 
			{
				// destroy the GameObject and remove from its respective lists
				Destroy (asteroids [asteroidIndex]);
				asteroids.RemoveAt (asteroidIndex);
				listTrack.GetComponent<ObjectTracker> ().objects.RemoveAt (asteroidIndex);

				// plays sound and keeps score
				play.PlayOneShot (asteroidDead);
				firstLevelScore += 60; 
			} 

			// otherwise
			else 
			{
				// store the position of the destroyed first tier asteroid
				Vector3 prevPosition = asteroids [asteroidIndex].GetComponent<GameMovement>().position;

				// destroy the GameObject and remove from its respective lists
				Destroy(asteroids [asteroidIndex]);
				asteroids.RemoveAt (asteroidIndex);
				listTrack.GetComponent<ObjectTracker>().objects.RemoveAt (asteroidIndex);

				// plays sound, keeps score, and sets hit to false
				firstLevelScore += 20;
				hit = false;
				play.PlayOneShot (asteroidDead);

				// instantiating and adding the two new second tier asteroids
				newAsteroid = (GameObject)Instantiate (asteroidPrefab, prevPosition, Quaternion.identity);
				newAsteroid2 = (GameObject)Instantiate (asteroidPrefab2, prevPosition, Quaternion.identity);

				listTrack.GetComponent<ObjectTracker>().objects.Add (newAsteroid);
				asteroids.Add (newAsteroid);
				listTrack.GetComponent<ObjectTracker>().objects.Add (newAsteroid2);
				asteroids.Add (newAsteroid2);
			}


		}
				
	}

	//Will create a bullet at the specified position applying the specified force
	public void Shoot(Vector3 playerPosition, Vector3 direction)
	{
		GameObject tempBullet = (GameObject)Instantiate(bullet, player.transform.position, player.transform.rotation);//create game object

		GameMovement movement = tempBullet.GetComponent<GameMovement>();//call its Movement script1

		movement.position = playerPosition; //Position of the bullet
		movement.velocity = direction;//acceleration of the bullet
		movement.speed = 0.5f;
		movement.useSlowdown = false;
		collBullet = tempBullet; // stores the bullets currently flying

		lifePool.Add (Time.time); //add the current time to list
		bulletPool.Add(tempBullet); //add the current bullet to list
		++bulletsFired;// increment the number of bullets
	}

	// Will check if a bullet is colliding with an asteroid
	public bool CheckHit()
	{
		bool collides = false;

		// loops through list of asteroids
		for (int i = 0; i < asteroids.Count; i++) 
		{
			// checks if they are colliding, and sotres the index and sets collides to true.
			if ((asteroids [i].GetComponent<GameMovement> ().position - collBullet.transform.position).magnitude <= (asteroids [i].GetComponent<ObjectInformation> ().radius + collBullet.GetComponent<ObjectInformation> ().radius)) 
			{
				//Debug.Log ("HIT!");
				asteroidIndex = i;
				collides = true;
			}
		}

		return collides;

	}

	// Property for score
	public int FirstLevelScore
	{
		get {return firstLevelScore; }
	}

}

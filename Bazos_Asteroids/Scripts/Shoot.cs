using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Niko Bazos(ndb9897@rit.edu)
 * This class moderates the actual call to shoot the bullet, the sound firing has, and the firing cooldown. It is attached to the GameManager.
 */

public class Shoot : MonoBehaviour
{

	// Attributes
	private BulletManager bulletMngr; //Bullet Manager instance
	private GameMovement movement; //Player Movement

	public GameObject player; //Player that will shoot
	public bool shouldShoot = false; //Show a shoot be generated this frame

	// Variables for the firing cooldown
	private float coolDown = 0.75f;
	private float start = 0f;

	// Sound for firing a shot
	public AudioSource audio;
	public AudioClip fireShot;

	// Use this for initialization
	void Start ()
	{
		//initialize the bulletMngr varialbe base on the field from editor
		bulletMngr = gameObject.GetComponent<BulletManager>();
		//Error control
		if (bulletMngr == null)
		{
			Debug.Log("Bullet Manager not assigned in GameManager");
			Debug.Break();//Stop execution
		}
		//Error control
		if (player == null)
		{
			Debug.Log("Player not assigned in GameManager");
			Debug.Break();//Stop execution
		}
		movement = player.GetComponent<GameMovement>();
		//Error control
		if (movement == null)
		{
			Debug.Log("Movement not assigned in Player");
			Debug.Break();//Stop execution
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//checking if shootin is possible and if the firing cooldown is over
		if(shouldShoot && Time.time > start + coolDown)
		{
			start = Time.time;
			shouldShoot = false;//so next frame it does not shoot unless called
			bulletMngr.Shoot(movement.position, movement.GetDirection() * 50);//, player.transform.rotation);//Bullets away! 
			audio.PlayOneShot (fireShot);

		}
	}


	// Generatres a shoot event based on the pressing of the space key
	void OnGUI()
	{
		//Check if the space was pressed so we generate a new shoot event
		if(Event.current.keyCode == KeyCode.Space)
		{
			shouldShoot = true;
		}

	}
}

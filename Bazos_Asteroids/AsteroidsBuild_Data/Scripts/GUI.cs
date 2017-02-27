using UnityEngine;
using System.Collections;

/*
 * Niko Bazos(ndb9897@rit.edu)
 * This class moderates the GUI. It is attached to the Main Camera.
 */

public class GUI : MonoBehaviour 
{
	// Attributes
	public ObjectTracker track; 
	public BulletManager score;

	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI ()
	{
		//GUI
		GUILayout.Box("Lives: " + track.ShipHealth);
		GUILayout.Box("Score: " + score.FirstLevelScore);
	}
}

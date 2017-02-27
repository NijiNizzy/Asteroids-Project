using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Niko Bazos(ndb9897@rit.edu)
 * This class the radii of the objects, and the testing of collision. It is attached to all collidable game objects.
 */

public class ObjectInformation : MonoBehaviour
{
	// Attributes
	public float radius = 1.0f;
	public Vector3 position;
	private GameMovement movement;
	private SpriteRenderer sprite;
	private SpriteRenderer [] spriteList;
	private Color green;

	// Use this for initialization
	void Start ()
	{
		green = new Color (0.0f, 1.0f, 0.0f);

		spriteList = gameObject.GetComponentsInChildren<SpriteRenderer>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
		if(null == sprite)
		{
			Debug.Log("Error: object" + gameObject.name + " needs to be a 2D sprite");
			Debug.Break();
		}

		/*
		sprite.color = green;

		foreach (SpriteRenderer child in spriteList) 
		{
			child.color = green;
		}
		*/
			
		movement = gameObject.GetComponent<GameMovement> ();
		if (null == movement) 
		{
			Debug.Log ("Error: object" + gameObject.name + " also needs to have a Movement component");
			Debug.Break ();
		}
		
		// radius setting
		Vector3 size = sprite.bounds.size/3;
		radius = size.x;
		if (radius < size.y) 
		{
			radius = size.y;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		position = movement.position;
	}

	/*
	void OnDrawGizmos()
	{
		Gizmos.color = new Color (1.0f, 1.0f, 0.0f, 0.33f);
		Gizmos.DrawSphere (transform.position, radius);
	}
	*/
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeeController : MonoBehaviour
{
	GameObject parent;
	public BeeController targetBee;
	public float speed = 10f;
	public float wander = 1f;
	public float minDistance = 1f;
	bool chase;
	Vector3 target;
	Material material;
	
	// Use this for initialization
	void Start ()
	{
		newTarget ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!chase)
			moveRandom ();
		else
			moveChase ();
		
		transform.rotation = Quaternion.LookRotation (GetComponent<Rigidbody> ().velocity);
	}
	
	void moveRandom ()
	{
		Vector3 delta = getTarget () - transform.position;
		if (delta.sqrMagnitude <= minDistance * minDistance) {
			delta = newTarget () - transform.position;
		}
		
		var dist = delta.magnitude;
		GetComponent<Rigidbody> ().AddForce (delta.normalized * Mathf.Sqrt (dist) * speed);
	}
	
	void moveChase ()
	{
		target = targetBee.transform.position + targetBee.GetComponent<Rigidbody> ().velocity;
	}
	
	Vector3 newTarget ()
	{
		target = Random.insideUnitSphere * wander;
		return target;
	}
	
	Vector3 getTarget ()
	{
		return target + parent.transform.position + parent.GetComponent<Rigidbody> ().velocity;
	}
	
	public void changeSwarm (SwarmController newSwarm)
	{
		parent = newSwarm.gameObject;
		wander = newSwarm.beeWander;
		if (newSwarm.team == 0)
			getMaterial ().color = new Color (0, .6f, 0);
		else
			getMaterial ().color = new Color (.6f, 0, 0);
	}

	public void kill ()
	{
		var children = new List<GameObject> ();
		foreach (Transform child in transform)
			children.Add (child.gameObject);
		children.ForEach (child => Destroy (child));
		GameObject.Destroy (this);
	}
	
	Material getMaterial ()
	{
		if (!material)
			material = gameObject.GetComponentInChildren<Renderer> ().material;
		return material;
	}
}

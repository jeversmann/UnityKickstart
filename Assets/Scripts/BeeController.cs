using UnityEngine;
using System.Collections;

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
	void Start()
	{
		newTarget();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (!chase)
			moveRandom();
		else
			moveChase();
		
		transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
	}
	
	void moveRandom()
	{
		Vector3 delta = getTarget() - transform.position;
		if (delta.sqrMagnitude <= minDistance * minDistance)
		{
			delta = newTarget() - transform.position;
		}
		
		var dist = delta.magnitude;
		rigidbody.AddForce(delta.normalized * Mathf.Sqrt(dist) * speed);
	}
	
	void moveChase()
	{
		target = targetBee.transform.position + targetBee.rigidbody.velocity;
	}
	
	Vector3 newTarget()
	{
		target = Random.insideUnitSphere * wander;
		return target;
	}
	
	Vector3 getTarget()
	{
		return target + parent.transform.position + parent.rigidbody.velocity;
	}
	
	public void changeSwarm(SwarmController newSwarm)
	{
		parent = newSwarm.gameObject;
		wander = newSwarm.beeWander;
		if (newSwarm.player)
			getMaterial().color = Color.green;
		else
			getMaterial().color = Color.red;
	}
	
	Material getMaterial()
	{
		if (!material)
			material = gameObject.GetComponentInChildren<Renderer>().material;
		return material;
	}
}

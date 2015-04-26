using UnityEngine;
using System.Collections;

public class BeeController : MonoBehaviour
{
	GameObject parent;
	public Vector3 targetPosition;
	public GameObject targetObject;
	public float speed = 10f;
	public float wander = 1f;
	public float minDistance = 1f;
	public bool chase;
	public float maxForce = 10;
	Vector3 target;
	Material material;
	
	// Use this for initialization
	void Start()
	{
		var root = FindObjectOfType<BeeRoot>();
		transform.parent = root.transform;
		newTarget();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		Vector3 force;
		if (!chase)
			force = moveRandom();
		else
			force = moveChase();
		if (GetComponent<Rigidbody>().velocity != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
		
		GetComponent<Rigidbody>().AddForce(force);
	}
	
	Vector3 moveRandom()
	{
		Vector3 delta = getTarget() - transform.position;
		if (delta.sqrMagnitude <= minDistance * minDistance)
		{
			delta = newTarget() - transform.position;
		}
		
		var dist = delta.magnitude;
		return delta.normalized * Mathf.Min(Mathf.Sqrt(dist) * speed, maxForce);
	}
	
	Vector3 moveChase()
	{
		if (targetObject)
			target = targetObject.transform.position;
		else
			target = targetPosition;
		var delta = target - transform.position;
		if (delta.sqrMagnitude <= minDistance * minDistance)
		{
			// do something when we hit the target bee
			chase = false;
			targetObject = null;
		}
		
		var dist = delta.magnitude;
		return delta.normalized * Mathf.Min(Mathf.Sqrt(dist) * speed, maxForce);
	}
	
	Vector3 newTarget()
	{
		target = Random.insideUnitSphere * wander;
		return target;
	}
	
	Vector3 getTarget()
	{
		return target + parent.transform.position + parent.GetComponent<Rigidbody>().velocity;
	}
	
	public void changeSwarm(SwarmController newSwarm)
	{
		parent = newSwarm.gameObject;
		wander = newSwarm.beeWander;
		if (newSwarm.team == 0)
			getMaterial().color = Color.green;
		else
			getMaterial().color = Color.red;
		newTarget();
	}
	
	Material getMaterial()
	{
		if (!material)
			material = gameObject.GetComponentInChildren<Renderer>().material;
		return material;
	}
	
	public void sendTo(Vector3 position)
	{
		targetPosition = position;
		targetObject = null;
		chase = true;
	}
	
	public void sendTo(GameObject other)
	{
		targetObject = other;
		chase = true;
	}
}

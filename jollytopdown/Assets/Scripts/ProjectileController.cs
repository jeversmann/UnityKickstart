using UnityEngine;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour
{
	public float speed = 1f;
	public Vector3 direction;
	public float maxDistance = 100;
	public SwarmController parent;
	public GameObject target;
	
	float distance;
	SwarmController swarm;
	
	// Use this for initialization
	void Start ()
	{
		swarm = GetComponent<SwarmController> ();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 move;
		if (target != null) {
			move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
		} else {
			move = direction * speed * Time.deltaTime;
		}
		transform.position += move;
		distance += speed * Time.deltaTime;
		
		if (distance >= maxDistance && !target) {
			swarm.sendBeesTo (parent);
		}
	}
	
	public void intersect (SwarmController other)
	{
		swarm.sendBeesTo (parent);
	}
}

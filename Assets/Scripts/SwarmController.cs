﻿using UnityEngine;
using System.Collections.Generic;

public class SwarmController : MonoBehaviour
{
	public BeeController beePrefab;
	public SwarmController swarmPrefab;
	public int size = 1;
	public float speed = 1f;
	
	public float beeWander = 5f;
	public float minWander = 1f;
	
	public int team = 0;
	
	List<BeeController> bees = new List<BeeController>();

	PlayerController player;
	
	// Use this for initialization
	void Start()
	{
		player = GetComponent<PlayerController>();
		
		for (int i = 0; i < size; i++)
		{
			var other = GameObject.Instantiate(beePrefab.gameObject, transform.position + Random.insideUnitSphere, Quaternion.identity) as GameObject;
			var bee = other.GetComponent<BeeController>();
			bee.changeSwarm(this);
			bees.Add(bee);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{	
		updateBees();
	}
	
	void updateBees()
	{
		foreach (var bee in bees)
		{
			float lerp = 0;
			if (GetComponent<Rigidbody>().velocity != Vector3.zero)
				lerp = speed / GetComponent<Rigidbody>().velocity.magnitude;
			bee.wander = Mathf.Lerp(beeWander, minWander, lerp);
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		var projectile = GetComponent<ProjectileController>();
		var otherSwarm = other.gameObject.GetComponent<SwarmController>();
		if (!otherSwarm)
			return;
		if (otherSwarm.team == team)
		{
			if (otherSwarm.player)
			{
				sendBeesTo(otherSwarm);
				if (projectile)
					projectile.intersect(otherSwarm);
				kill();
			}
		} else
		{
			if (otherSwarm.size <= size)
			{
				otherSwarm.sendBeesTo(this);
				if (projectile)
					projectile.intersect(otherSwarm);
				otherSwarm.kill();
			}
		}
	}
	
	public void sendBeesTo(SwarmController other)
	{
		sendBeesTo(other, bees.Count);
	}
	
	public void sendBeesTo(SwarmController other, int n)
	{
		n = Mathf.Min(n, bees.Count);
		for (int i = 0; i < n; i++)
		{
			other.bees.Add(bees [i]);
			bees [i].changeSwarm(other);
		}
		other.size += n;
		bees.RemoveRange(0, n);
		size -= n;
	}
	
	public void kill()
	{
		if (!player)
			GameObject.Destroy(gameObject);
	}
	
	
}

using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public SwarmController swarmPrefab;
	
	public float speed = 1f;
	public float strafeSpeed = 1f;
	public float turnSpeed = 1f;
	public float turnSpeedVert = 1f;

	SwarmController chargingSwarm = null;
	
	public float chargeDelay = .01f;
	public float projectileSpeed = 1.5f;
	public float projectileDistance = 100;
	float chargeTimer = 0;
	int chargeRate = 1;
	
	SwarmController swarm;
	
	// Use this for initialization
	void Start()
	{
		swarm = GetComponent<SwarmController>();	
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		chargeTimer += Time.deltaTime;

		movePlayer();
		if (Input.GetAxis("Fire1") > .1f)
		{
			chargeBee();
		} else if (chargingSwarm)
		{
			fireSwarm();
		}
	}
	
	void movePlayer()
	{
		var moveInput = new Vector3(Input.GetAxis("HorizontalLeft"), Input.GetAxis("VerticalLeft"), 0);
		var move = transform.forward * moveInput.y * speed;
		move += transform.right * moveInput.x * strafeSpeed;
		move *= Time.deltaTime;
		transform.position += move;
		
		var cameraInput = new Vector3(Input.GetAxis("HorizontalRight"), Input.GetAxis("VerticalRight"), 0);
		var rotY = Input.GetAxis("VerticalRight") * turnSpeedVert * Time.deltaTime;
		var rotX = Input.GetAxis("HorizontalRight") * turnSpeed * Time.deltaTime;
		
		transform.Rotate(Vector3.up, rotX, Space.World);
		transform.Rotate(Vector3.left, rotY, Space.Self);
	}
	
	void chargeBee()
	{
		if (chargeTimer >= chargeDelay)
		{
			chargeTimer = 0;
			if (!chargingSwarm)
			{
				var o = GameObject.Instantiate(swarmPrefab.gameObject, transform.position + transform.forward * 10, Quaternion.identity) as GameObject;
				chargingSwarm = o.GetComponent<SwarmController>();
				chargingSwarm.transform.parent = transform;
				chargingSwarm.beeWander = swarm.beeWander * .25f;
				chargingSwarm.team = 0;
			}
			swarm.sendBeesTo(chargingSwarm, 1);
		}
		
	}
	
	void fireSwarm()
	{
		chargingSwarm.transform.parent = null;
		var projectile = chargingSwarm.gameObject.AddComponent<ProjectileController>();
		projectile.parent = swarm;
		projectile.direction = transform.forward;
		projectile.maxDistance = projectileDistance;
		projectile.speed = speed * projectileSpeed;
		chargingSwarm = null;
	}
}

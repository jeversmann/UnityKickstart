using UnityEngine;
using System.Collections.Generic;

public class SwarmController : MonoBehaviour
{
	public bool player;
	public BeeController beePrefab;
	public SwarmController swarmPrefab;
	public int size = 1;
	public float speed = 1f;
	public float strafeSpeed = 1f;
	public float turnSpeed = 1f;
	public float turnSpeedVert = 1f;
	public float beeWander = 5f;
	public float minWander = 1f;
	public int team = 0;
	
	List<BeeController> bees = new List<BeeController>();
	int nextBee;
	SwarmController chargingSwarm = null;

	public float chargeDelay = .01f;
	float chargeTimer = 0;
	// Use this for initialization
	void Start()
	{
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
		chargeTimer += Time.deltaTime;
		if (player)
		{
			movePlayer();
			if (Input.GetAxis("Fire1") > .1f)
			{
				chargeBee();	
			} else if (chargingSwarm)
			{
				fireSwarm();
			}
		}
		
		updateBees();
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
		var otherSwarm = other.gameObject.GetComponent<SwarmController>();
		if (!otherSwarm)
			return;
		if (otherSwarm.team == team)
		{
			if (otherSwarm.player)
			{
				sendBeesTo(otherSwarm);
				kill();
			}
		} else
		{
			if (otherSwarm.size <= size)
			{
				otherSwarm.sendBeesTo(this);
				otherSwarm.kill();
			} else
			{
				sendBeesTo(otherSwarm);
				kill();
			}
		}
	}
	
	void sendBeesTo(SwarmController other)
	{
		sendBeesTo(other, bees.Count);
	}
	
	void sendBeesTo(SwarmController other, int n)
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
	
	void kill()
	{
		if (!player)
			GameObject.Destroy(gameObject);
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
				chargingSwarm.beeWander = beeWander * .25f;
				chargingSwarm.team = team;
			}
			sendBeesTo(chargingSwarm, 1);
		}
		
	}
	
	void fireSwarm()
	{
		chargingSwarm.transform.parent = null;
		chargingSwarm = null;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool controller;

	public float HorizontalMovementAxis {
		get {
			if (controller) {
				return Input.GetAxis ("HorizontalLeft");
			} else {
				return Input.GetAxis ("Horizontal[0]");
			}
		}
	}

	public float VerticalMovementAxis {
		get {
			if (controller) {
				return Input.GetAxis ("VerticalLeft");
			} else {
				return Input.GetAxis ("Vertical[0]");
			}
		}
	}
	
	public float VerticalLookAxis {
		get {
			if (controller) {
				return Input.GetAxis ("VerticalRight");
			} else {
				return Input.GetAxis ("Mouse Y");
			}
		}
	}

	public float HorizontalLookAxis {
		get {
			if (controller) {
				return Input.GetAxis ("HorizontalRight");
			} else {
				return Input.GetAxis ("Mouse X");
			}
		}
	}

	public SwarmController swarmPrefab;
	
	public float speed = 1f;
	public float strafeSpeed = 1f;
	public float turnSpeed = 1f;
	public float turnSpeedVert = 1f;
	
	SwarmController chargingSwarm = null;

	public float launchCount {
		get {
			if (chargingSwarm) {
				return chargingSwarm.size;
			} else {
				return 0;
			}
		}
	}

	public float chargeDelay = .01f;
	public float projectileSpeed = 1.5f;
	public float projectileDistance = 100;
	float chargeTimer = 0;
	int chargeRate = 1;
	
	SwarmController swarm;

	public float size {
		get {
			return swarm.size;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		swarm = GetComponent<SwarmController> ();
		Camera camera = Camera.main;
		float[] distances = new float[32];
		distances [9] = 250;

		camera.layerCullDistances = distances;
		camera.layerCullSpherical = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		chargeTimer += Time.deltaTime;
		
		movePlayer ();
		if (Input.GetAxis ("Fire1") > .1f) {
			chargeBee ();
		} else if (chargingSwarm) {
			fireSwarm ();
		}
	}
	
	void movePlayer ()
	{
		var moveInput = new Vector3 (HorizontalMovementAxis, VerticalMovementAxis, 0);
		var move = transform.forward * moveInput.y * speed;
		move += transform.right * moveInput.x * strafeSpeed;
		move *= Time.deltaTime;
		transform.position += move;
		
		var cameraInput = new Vector3 (HorizontalLookAxis, VerticalLookAxis, 0);
		var rotY = VerticalLookAxis * turnSpeedVert * Time.deltaTime;
		var rotX = HorizontalLookAxis * turnSpeed * Time.deltaTime;
		
		transform.Rotate (Vector3.up, rotX, Space.World);
		transform.Rotate (Vector3.left, rotY, Space.Self);
	}
	
	void chargeBee ()
	{
		if (chargeTimer >= chargeDelay) {
			chargeTimer = 0;
			if (!chargingSwarm) {
				swarmPrefab.playerSwarm = this;
				var o = GameObject.Instantiate (swarmPrefab.gameObject, transform.position + transform.forward * 10, Quaternion.identity) as GameObject;
				chargingSwarm = o.GetComponent<SwarmController> ();
				chargingSwarm.transform.parent = transform;
				chargingSwarm.beeWander = swarm.beeWander * .25f;
				chargingSwarm.team = 0;
			}
			if (swarm.size > 5) {
				swarm.sendBeesTo (chargingSwarm, 1);
			}
		}
		
	}
	
	void fireSwarm ()
	{
		chargingSwarm.transform.parent = null;
		var projectile = chargingSwarm.gameObject.AddComponent<ProjectileController> ();
		projectile.parent = swarm;
		projectile.direction = transform.forward;
		projectile.maxDistance = projectileDistance;
		projectile.speed = speed * projectileSpeed;
		chargingSwarm = null;
	}
}

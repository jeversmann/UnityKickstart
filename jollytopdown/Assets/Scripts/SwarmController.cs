using UnityEngine;
using System.Collections.Generic;

public class SwarmController : MonoBehaviour
{
	public bool player;
	public BeeController beePrefab;
	public int size = 1;
	public float speed = 1f;
	public float strafeSpeed = 1f;
	public float turnSpeed = 1f;
	public float turnSpeedVert = 1f;
	public float beeWander = 5f;
	public float minWander = 1f;
	
	List<BeeController> bees = new List<BeeController> ();

	private PlayerController control;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < size; i++) {
			var other = GameObject.Instantiate (beePrefab.gameObject, transform.position + Random.insideUnitSphere, Quaternion.identity) as GameObject;
			var bee = other.GetComponent<BeeController> ();
			bee.changeSwarm (this);
			bees.Add (bee);
		}

		control = GetComponent<PlayerController> ();
		var text = GetComponentInChildren<TextMesh> ();
		if (text) {
			text.text = size.ToString ();
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (player)
			movePlayer ();
		
		updateBees ();
	}
	
	void movePlayer ()
	{
		var moveInput = new Vector3 (control.HorizontalMovementAxis, control.VerticalMovementAxis, 0);
		var move = transform.forward * moveInput.y * speed;
		move += transform.right * moveInput.x * strafeSpeed;
		move *= Time.deltaTime;
		transform.position += move;
		
		var cameraInput = new Vector3 (control.HorizontalLookAxis, control.VerticalLookAxis, 0);
		var rotY = control.VerticalLookAxis * turnSpeedVert * Time.deltaTime;
		var rotX = control.HorizontalLookAxis * turnSpeed * Time.deltaTime;
		
		transform.Rotate (Vector3.up, rotX, Space.World);
		transform.Rotate (Vector3.left, rotY, Space.Self);
	}
	
	void updateBees ()
	{
		foreach (var bee in bees) {
			float lerp = 0;
			if (GetComponent<Rigidbody> ().velocity != Vector3.zero)
				lerp = speed / GetComponent<Rigidbody> ().velocity.magnitude;
			bee.wander = Mathf.Lerp (beeWander, minWander, lerp);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		var otherSwarm = other.gameObject.GetComponent<SwarmController> ();
		if (otherSwarm) {
			if (otherSwarm.size <= size) {
				otherSwarm.sendBeesTo (this);
				otherSwarm.kill ();
			} else {
				sendBeesTo (otherSwarm);
				kill ();
			}
		}
	}
	
	void sendBeesTo (SwarmController other)
	{
		foreach (var bee in bees) {
			other.bees.Add (bee);
			bee.changeSwarm (other);
		}
		other.size += size;
		bees = new List<BeeController> ();
		size = 0;
	}
	
	void kill ()
	{
		if (!player)
			GameObject.Destroy (gameObject);
	}
}

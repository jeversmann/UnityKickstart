using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SwarmController : MonoBehaviour
{
	public BeeController beePrefab;
	public PlayerController playerSwarm;
	public SwarmController swarmPrefab;
	public int size = 1;
	public float speed = 1f;
	
	public float beeWander = 5f;
	public float minWander = 1f;
	
	public int team = 0;
	
	List<BeeController> bees = new List<BeeController> ();

	PlayerController player;
	
	SphereCollider collider;
	
	// Use this for initialization
	void Start ()
	{
		player = GetComponent<PlayerController> ();
		collider = GetComponent<SphereCollider> ();
		for (int i = 0; i < size; i++) {
			var other = GameObject.Instantiate (beePrefab.gameObject, transform.position + Random.insideUnitSphere, Quaternion.identity) as GameObject;
			var bee = other.GetComponent<BeeController> ();
			bee.changeSwarm (this);
			bees.Add (bee);
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		var image = GetComponentInChildren<Image> ();
		if (team != 0 && image) {
			if (playerSwarm.launchCount <= 0) {
				if (playerSwarm.size < size) {
					image.color = new Color (1, .3f, .3f);
				} else {
					image.color = new Color (.3f, .7f, .3f);
				}
			} else {
				if (playerSwarm.launchCount < size) {
					image.color = new Color (1, .3f, .3f);
				} else {
					image.color = new Color (.3f, .7f, .3f);
				}
			}
		}
		
		collider.radius = beeWander;

		updateBees ();
	}
	
	void updateBees ()
	{
		float lerp = 0;
		if (GetComponent<Rigidbody> ().velocity != Vector3.zero)
			lerp = speed / GetComponent<Rigidbody> ().velocity.magnitude;
		foreach (var bee in bees) {
			bee.wander = Mathf.Lerp (beeWander, minWander, lerp);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		var projectile = GetComponent<ProjectileController> ();
		var otherSwarm = other.gameObject.GetComponent<SwarmController> ();
		if (!otherSwarm)
			return;
		if (otherSwarm.team == team) {
			if (otherSwarm.player) {
				sendBeesTo (otherSwarm);
				if (projectile)
					projectile.intersect (otherSwarm);
				kill ();
			}
		} else {
			if (otherSwarm.size < size || (team == 0 && otherSwarm.size == size)) {
				otherSwarm.sendBeesTo (this);
				if (projectile)
					projectile.intersect (otherSwarm);
				otherSwarm.kill ();
			}
		}
	}
	
	public void sendBeesTo (SwarmController other)
	{
		sendBeesTo (other, bees.Count);
	}
	
	public void sendBeesTo (SwarmController other, int n)
	{
		n = Mathf.Min (n, bees.Count);
		for (int i = 0; i < n; i++) {
			other.bees.Add (bees [i]);
			bees [i].changeSwarm (other);
		}
		other.size += n;
		bees.RemoveRange (0, n);
		size -= n;
	}

	public int removeBees (int count)
	{
		int n = Mathf.Min (count, bees.Count);
		for (int i = 0; i < n; i++) {
			bees [0].kill ();
			bees.RemoveAt (0);
		}
		size -= n;
		if (bees.Count == 0 && !player)
			GameObject.Destroy (gameObject);
		return n;
	}
	
	public void kill ()
	{
		if (!player)
			GameObject.Destroy (gameObject);
	}
}

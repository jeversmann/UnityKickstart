using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour
{
	public TargetCounter counter;
	private TextMesh display;
	private int health = 25;

	// Use this for initialization
	void Start ()
	{
		display = GetComponentInChildren<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		display.text = health.ToString ();
	}

	void OnTriggerEnter (Collider other)
	{
		var projectile = GetComponent<ProjectileController> ();
		var otherSwarm = other.gameObject.GetComponent<SwarmController> ();
		if (!otherSwarm)
			return;

		health -= otherSwarm.removeBees (health);

		if (health == 0) {
			counter.targets -= 1;
			Animator anim = GetComponentInChildren<Animator> ();
			anim.SetTrigger ("Die");
			anim.transform.parent = null;
			GameObject.Destroy (gameObject);
		}
	}
}

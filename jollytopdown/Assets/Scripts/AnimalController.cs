using UnityEngine;
using System.Collections.Generic;

public class AnimalController : MonoBehaviour
{
	public float speed = 5f;
	public float rotationSpeed = .5f;
	CharacterController character;
	Animator animator;
	
	// Use this for initialization
	void Start ()
	{
		character = GetComponentInChildren<CharacterController> ();
		animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		character.SimpleMove (transform.forward * speed + transform.right * .15f);
		
		if (character.velocity != Vector3.zero) {
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (character.velocity, Vector3.up), rotationSpeed);
		}
		
		animator.SetFloat ("Speed", character.velocity.magnitude);
	}
}

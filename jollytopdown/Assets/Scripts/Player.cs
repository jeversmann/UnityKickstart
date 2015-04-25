using UnityEngine;
using System.Collections;
using Jolly;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float MaxSpeed;
	public float JumpForce;
	public float TurnRate;

	public Vector3 Heading;

	public GameObject GroundContactDelta;

	public bool IsOnGround { get; private set; }

	public Color HUDColor;

	public float Size { get; private set; }

	public int Score { get; private set; }

	private float theta = 0;
	private float phi = 90;
	private bool pressed = false;

	public Player ()
	{
		this.Score = 0;
		this.Size = .1f;
	}

	void Start ()
	{
		this.transform.localScale = new Vector3 (Size, Size, Size);

		JollyDebug.Watch (this, "IsOnGround", delegate () {
			return this.IsOnGround;
		});
	}

	void Update ()
	{
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Mouse X");
		float v = Input.GetAxisRaw ("Mouse Y");
		
		theta += TurnRate * h;
		phi += TurnRate * v / 2;
		
		Heading = new Vector3 (Mathf.Sin (theta), Mathf.Sin (phi), Mathf.Cos (theta));

		if (Input.GetButtonDown ("Jump[1]") || pressed) {
			pressed = true;
		}

		if (Input.GetButtonUp ("Jump[1]") || !pressed) {
			this.GetComponent<Rigidbody> ().AddForce (Heading * this.MovementForce * Mathf.Sqrt (Size));
			pressed = false;
		}
		//this.GetComponent<Rigidbody> ().velocity = Heading.normalized * MaxSpeed;
	}

	public void OnCollected (Collectable collectable)
	{
		this.Score++;
		this.Size = Mathf.Pow (collectable.GetComponent<Collectable> ().vol () / 4 + Mathf.Pow (Size, 3), 1f / 3f);
		this.transform.localScale = new Vector3 (Size, Size, Size);
	}
}

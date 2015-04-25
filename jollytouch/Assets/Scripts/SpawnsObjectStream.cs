using UnityEngine;
using System.Collections;

public class SpawnsObjectStream : MonoBehaviour
{
	public GameObject StreamObject;
	public float TimeBetweenObjects;
	private float TimeUntilNextObject = 0.0f;
	public Vector2 InitialVelocity;

	public float ObjectsPerSecond
	{
		get
		{
			return 1.0f / this.TimeBetweenObjects;
		}
	}

	void Start ()
	{
	}
	
	void Update ()
	{
		if (LevelManager.Instance.LevelIsComplete)
		{
			return;
		}
		this.TimeUntilNextObject -= Time.deltaTime;
		if (this.TimeUntilNextObject < 0)
		{
			GameObject go = (GameObject)GameObject.Instantiate(this.StreamObject, this.transform.position, this.StreamObject.transform.localRotation);
			go.rigidbody2D.velocity = this.InitialVelocity;
			go.transform.parent = this.transform;
			this.TimeUntilNextObject = this.TimeBetweenObjects;
		}
	}
}

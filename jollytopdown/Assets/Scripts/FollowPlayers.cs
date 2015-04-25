using UnityEngine;
using System.Collections;
using Jolly;

public class FollowPlayers : MonoBehaviour
{
	public GameObject[] Players;
	public float FollowLerpFactor = 5.0f;

	private Vector3 CameraOffset;
	private Vector3 TargetCameraPosition;

	void Start ()
	{
		this.CameraOffset = this.camera.transform.position;
	}

	void OnPreRender ()
	{
		float lerpFactor = Time.deltaTime * this.FollowLerpFactor;
		this.camera.transform.position = Vector3.Lerp(this.camera.transform.position, this.TargetCameraPosition, lerpFactor);
	}

	void Update ()
	{
		this.TargetCameraPosition = (HeroesAverageLocation().SetY (0) + this.CameraOffset);
	}
	
	private Vector3 HeroesAverageLocation()
	{
		Vector3 average = Vector3.zero;
		foreach (GameObject go in this.Players)
		{
			average += go.transform.position;
		}
		average /= this.Players.Length;
		return average;
	}
	

}

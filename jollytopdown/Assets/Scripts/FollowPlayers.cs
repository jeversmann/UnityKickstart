using UnityEngine;
using System.Collections;
using Jolly;

public class FollowPlayers : MonoBehaviour
{
	public GameObject[] Players;
	public float FollowLerpFactor = 5.0f;

	private float CameraOffset = 10;
	private Vector3 CameraHeading;
	private Vector3 TargetCameraPosition;

	void OnPreRender ()
	{
		float lerpFactor = Time.deltaTime * this.FollowLerpFactor;
		this.GetComponent<Camera> ().transform.position = this.TargetCameraPosition;
		this.GetComponent<Camera> ().transform.rotation = Quaternion.LookRotation (CameraHeading);
	}

	void Update ()
	{
		this.CameraHeading = this.Players [1].GetComponent<Player> ().Heading;
		this.TargetCameraPosition = this.Players [1].transform.position +
			CameraHeading * -(CameraOffset * this.Players [1].GetComponent<Player> ().Size);
	}
	
	private Vector3 HeroesAverageLocation ()
	{
		Vector3 average = Vector3.zero;
		foreach (GameObject go in this.Players) {
			average += go.transform.position;
		}
		average /= this.Players.Length;
		return average;
	}
	

}

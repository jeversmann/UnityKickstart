using UnityEngine;
using System.Collections;

public class Rotates : MonoBehaviour
{
	public float RotationSpeed;

	void Update ()
	{
		this.transform.localRotation = Quaternion.Euler (0.0f, 0.0f, Time.time * this.RotationSpeed);
	}
}

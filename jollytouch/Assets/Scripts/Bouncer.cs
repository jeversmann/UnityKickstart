using UnityEngine;
using System.Collections;
using Jolly;

public class Bouncer : MonoBehaviour
{
	public float DespawnHeight;

	void Update ()
	{
		if (this.transform.position.y < this.DespawnHeight)
		{
			GameObject.Destroy (this.gameObject);
		}
	}
}

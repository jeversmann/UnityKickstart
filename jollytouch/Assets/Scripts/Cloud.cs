using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		Bouncer bouncer = other.gameObject.GetComponent<Bouncer>();
		if (null != bouncer)
		{
			GameObject.Destroy(other.gameObject);
		}
	}

}

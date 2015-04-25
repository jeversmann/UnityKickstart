using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
	public float RotationSpeed;
	public AudioClip CollectedSound;
	
	public float Size { get; private set; }

	void Start ()
	{
	}
	
	void Update ()
	{
		Vector3 eulerRotation = this.transform.localRotation.eulerAngles;
		this.transform.rotation = Quaternion.Euler (eulerRotation.x, Time.time * this.RotationSpeed, eulerRotation.z);
		if (4 * Mathf.Pow (FindObjectOfType<Player> ().Size, 3) > vol ()) {
			this.gameObject.GetComponent<MeshRenderer> ().material.color = new Color (0, 255, 0);
		}
		
	}

	void OnTriggerEnter (Collider other)
	{
		Player player = other.gameObject.GetComponent<Player> ();
		if (null == player) {
			return;
		} else if (4 * Mathf.Pow (player.Size, 3) > vol ()) {
			player.OnCollected (this);

			AudioSource.PlayClipAtPoint (this.CollectedSound, this.transform.position);

			DestroyObject (this.gameObject);
		}
	}

	public void SetScale (float size)
	{
		Size = size;

		this.transform.localScale = new Vector3 (4, .5f, 4) * size;
	}

	public float vol ()
	{
		return 3 * Mathf.Pow (Size * .5f, 2) * Size * 4; 
	}
}

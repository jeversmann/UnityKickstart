using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaunchCounter : MonoBehaviour
{

	public PlayerController playerSwarm;
	
	private Text myText;
	
	// Use this for initialization
	void Start ()
	{
		myText = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float count = playerSwarm.launchCount;
		if (count > 0) {
			myText.text = count.ToString ();
			transform.position = Vector2.Lerp (transform.position, new Vector2 (110, 20), .5f);
		} else {
			transform.position = Vector2.Lerp (transform.position, new Vector2 (110, -60), .5f);
		}
	}
}

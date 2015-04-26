using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwarmCounter : MonoBehaviour
{

	public SwarmController playerSwarm;

	private Text myText;

	// Use this for initialization
	void Start ()
	{
		myText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		myText.text = playerSwarm.size.ToString ();
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetCounter : MonoBehaviour
{

	public int targets;
	private Text myText;
	
	// Use this for initialization
	void Start ()
	{
		myText = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		myText.text = string.Format ("{0} Targets Left", targets);
	}
}

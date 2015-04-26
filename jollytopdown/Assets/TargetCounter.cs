using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetCounter : MonoBehaviour
{

	public float time;
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
		time -= Time.deltaTime;
		myText.text = string.Format ("{0} Targets Left", targets)
			+ "\n" + string.Format ("{0:0}:{1:00}", Mathf.Floor (time / 60), time % 60);
	}
}

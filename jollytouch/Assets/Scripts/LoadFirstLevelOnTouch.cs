using UnityEngine;
using System.Collections;

public class LoadFirstLevelOnTouch : MonoBehaviour
{
	public string FirstLevelName;

	void OnTouchDown(Vector2 point)
	{
		Application.LoadLevel(this.FirstLevelName);
	}
}

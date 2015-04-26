using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool controller;

	public float HorizontalMovementAxis {
		get {
			if (controller) {
				return Input.GetAxis ("HorizontalLeft");
			} else {
				return Input.GetAxis ("Horizontal[0]");
			}
		}
	}

	public float VerticalMovementAxis {
		get {
			if (controller) {
				return Input.GetAxis ("VerticalLeft");
			} else {
				return Input.GetAxis ("Vertical[0]");
			}
		}
	}
	
	public float VerticalLookAxis {
		get {
			if (controller) {
				return Input.GetAxis ("VerticalRight");
			} else {
				return Input.GetAxis ("Mouse Y");
			}
		}
	}

	public float HorizontalLookAxis {
		get {
			if (controller) {
				return Input.GetAxis ("HorizontalRight");
			} else {
				return Input.GetAxis ("Mouse X");
			}
		}
	}
}

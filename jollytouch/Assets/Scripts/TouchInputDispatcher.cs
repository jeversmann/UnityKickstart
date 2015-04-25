using UnityEngine;
using System.Collections;
using Jolly;

public class TouchInputDispatcher : MonoBehaviour
{
	void Start ()
	{

	}

	void Update ()
	{
		foreach (Touch touch in Input.touches)
		{
			this.CastTouchRay (touch.position, touch.phase);
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.CastTouchRay (Input.mousePosition, TouchPhase.Began);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.CastTouchRay (Input.mousePosition, TouchPhase.Ended);
		}
		if (Input.GetMouseButton(0))
		{
			this.CastTouchRay (Input.mousePosition, TouchPhase.Moved);
		}
	}

	void CastTouchRay (Vector2 position, TouchPhase phase)
	{
		Vector2 worldPoint = this.GetComponent<Camera>().ScreenToWorldPoint(position);
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero, 0);
		bool didHit = null != hit.collider;
		JollyDebug.Watch (this, "CastTouchRayHit", didHit.ToString());
		if (didHit)
		{
			GameObject recipient = hit.transform.gameObject;
			switch (phase)
			{
			case TouchPhase.Began:
				recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
				break;
			case TouchPhase.Ended:
				recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
				break;
			case TouchPhase.Stationary:
			case TouchPhase.Moved:
				recipient.SendMessage ("OnTouch", hit.point, SendMessageOptions.DontRequireReceiver);
				break;
			case TouchPhase.Canceled:
				recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				break;
			}
		}
	}
}

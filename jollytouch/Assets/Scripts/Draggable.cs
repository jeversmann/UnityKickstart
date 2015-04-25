using UnityEngine;
using System.Collections;
using Jolly;

public class Draggable : MonoBehaviour
{
	public Color touchedColor;
	private Color originalColor;

	private Vector2 lastTouch;

	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		this.spriteRenderer = this.GetComponent<SpriteRenderer>();
		this.originalColor = this.spriteRenderer.color;
	}
	
	void Update ()
	{
		this.rigidbody2D.velocity = Vector2.zero;
	}

	void OnTouchDown (Vector2 position)
	{
		if (LevelManager.Instance.LevelIsComplete)
		{
			return;
		}
		this.lastTouch = position;
		this.spriteRenderer.color = this.touchedColor;
	}

	void OnTouch (Vector2 position)
	{
		if (LevelManager.Instance.LevelIsComplete)
		{
			return;
		}
		this.transform.position += (position - this.lastTouch).xyz(0.0f);
		this.lastTouch = position;
	}

	void OnTouchUp (Vector2 position)
	{
		if (LevelManager.Instance.LevelIsComplete)
		{
			return;
		}
		this.RestoreOriginalColor();
	}

	void OnTouchExit (Vector2 position)
	{
		this.RestoreOriginalColor();
	}

	void RestoreOriginalColor ()
	{
		this.spriteRenderer.color = this.originalColor;
	}
}

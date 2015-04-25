using UnityEngine;
using System.Collections;
using Jolly;

public class Sun : MonoBehaviour
{
	public Color UnpoweredColor;
	public Color PoweredColor;

	private float InitialDiscScale;
	public GameObject EmptyDisc;
	public GameObject DiscToFill;
	public Sprite FullDiscSprite;

	public float TimeToFullPower;
	private float PowerIncreasePerUnit;
	private float Power = 0.0f;
	private float LerpedPower = 0.0f;

	public bool IsFullyPowered
	{
		get; private set;
	}

	private float PulseSizeStopTime = Mathf.NegativeInfinity;
	private float LerpedSize = 1.0f;
	public float PulseSizePeriod = 2.0f;
	public float PulseSizeScale = 0.2f;


	private ArrayList timesOfLastUnits = new ArrayList();

	void Start ()
	{
		int totalDestinations = GameObject.FindGameObjectsWithTag("Sun").Length;
		JollyDebug.Assert (totalDestinations > 0);
		float totalObjectsPerSecond = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("SpawnPoint"))
		{
			SpawnsObjectStream spawnsObjectStream = go.GetComponent<SpawnsObjectStream>();
			totalObjectsPerSecond += spawnsObjectStream.ObjectsPerSecond - 1;
		}
		totalObjectsPerSecond /= totalDestinations;
		JollyDebug.Assert (totalObjectsPerSecond > 0);
		this.PowerIncreasePerUnit = 1.0f / totalObjectsPerSecond;

		this.InitialDiscScale = this.EmptyDisc.transform.localScale.x;
	}

	void Update ()
	{
		this.ComputePower ();
		this.PulseSize ();
		this.FillDisc ();
	}

	void ComputePower ()
	{
		bool levelIsComplete = this.IsFullyPowered || this.LerpedPower > 0.90f;
		if (levelIsComplete)
		{
			this.Power = 1.0f;
			this.IsFullyPowered = true;
		}
		else
		{
			float sum = 0.0f;
			for (int i = this.timesOfLastUnits.Count - 1; i >= 0; --i)
			{
				float unitTime = (float)this.timesOfLastUnits[i];
				if (Time.time - unitTime > 1.0f)
				{
					this.timesOfLastUnits.RemoveRange(0, i+1);
					break;
				}
				sum += this.PowerIncreasePerUnit;
			}
			this.Power = Mathf.Max (0.1f * (1.5f + Mathf.Sin (4.0f * Time.time)), sum);
		}
		this.LerpedPower = Mathf.Clamp01 (Mathf.Lerp (this.LerpedPower, this.Power, Time.deltaTime));
		JollyDebug.Watch (this, "Power", this.Power);
		JollyDebug.Watch (this, "LerpedPower", this.LerpedPower);
	}

	void PulseSize ()
	{
		bool pulseSize = Time.time < this.PulseSizeStopTime;
		float size = this.InitialDiscScale;
		if (pulseSize)
		{
			size *= Mathf.Sin (Time.time / this.PulseSizePeriod * 2.0f * Mathf.PI) * this.PulseSizeScale + 1.0f;
		}
		this.LerpedSize = Mathf.Lerp (this.LerpedSize, size, Time.deltaTime * 5.0f);
		Vector3 scale = new Vector3(this.LerpedSize, this.LerpedSize, 1.0f);
		this.DiscToFill.transform.localScale = scale;
		this.EmptyDisc.transform.localScale = scale;
	}

	void FillDisc ()
	{
		Rect textureRect = this.FullDiscSprite.textureRect;
		float top = textureRect.yMax * (this.LerpedPower);
		Rect croppedSpriteRect = new Rect (0.0f, 0.0f, textureRect.width, top);
		Sprite croppedSprite = Sprite.Create(this.FullDiscSprite.texture, croppedSpriteRect, new Vector2(0.5f,0));
		SpriteRenderer cropSpriteRenderer = this.DiscToFill.GetComponent<SpriteRenderer>();
		cropSpriteRenderer.sprite = croppedSprite;

		const int pixelsToUnits = 100;
		cropSpriteRenderer.color = Color.Lerp (this.UnpoweredColor, this.PoweredColor, this.LerpedPower);
		float yOffset = -0.5f * textureRect.width / pixelsToUnits * cropSpriteRenderer.transform.localScale.y;
		cropSpriteRenderer.transform.localPosition = new Vector3(0.0f, yOffset, 0.0f);
	}

	public Color Color
	{
		get
		{
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
			return spriteRenderer.color;
		}
		set
		{
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
			spriteRenderer.color = value;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Bouncer bouncer = other.gameObject.GetComponent<Bouncer>();
		if (null != bouncer)
		{
			this.PulseSize();
			this.PulseSizeStopTime = Time.time + this.PulseSizePeriod;
			this.timesOfLastUnits.Add (Time.time);
			GameObject.Destroy(other.gameObject);
		}
	}
}

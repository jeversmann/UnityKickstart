using UnityEngine;
using System.Collections;

public class PlayLoopedMovementSound : MonoBehaviour
{
	public AudioSource LoopingMovementSound;
	public float PitchMinimum;
	public float PitchMaximum;
	public float SpeedForFullVolume;

	private float PitchFactor;

	private Player Player;

	void Start ()
	{
		this.Player = this.GetComponent<Player>();
		this.PitchFactor = (PitchMaximum - PitchMinimum) / this.Player.MaxSpeed;
	}

	void Update ()
	{
		this.LoopingMovementSound.mute = !this.Player.IsOnGround;
		float speed = this.Player.rigidbody.velocity.magnitude;
		this.LoopingMovementSound.volume = speed > this.SpeedForFullVolume ? 1.0f : speed/this.SpeedForFullVolume;
		this.LoopingMovementSound.pitch = this.PitchMinimum + this.PitchFactor * speed;
	}
}

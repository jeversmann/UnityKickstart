using UnityEngine;
using System.Collections;
using Jolly;

public class LevelManager : MonoBehaviour
{
	public string NextLevelSceneName;

	void Start ()
	{
		JollyDebug.Assert (this.gameObject.name.Equals ("LevelManager"));
	}

	void Update ()
	{
		if (!this.LevelIsComplete)
		{
			this.LevelIsComplete = this.AllSunsIndicateLevelIsComplete;
			if (this.LevelIsComplete)
			{
				this.StartCoroutine("EndLevelAndLoadNextLevel");
			}
		}
	}

	public bool LevelIsComplete
	{
		get; private set;
	}

	private bool AllSunsIndicateLevelIsComplete
	{
		get
		{
			GameObject[] sunsToTestIfLevelIsComplete = GameObject.FindGameObjectsWithTag("Sun");
			foreach (GameObject go in sunsToTestIfLevelIsComplete)
			{
				Sun sun = go.GetComponent<Sun>();
				if (!sun.IsFullyPowered)
				{
					return false;
				}
			}
			return true;
		}
	}

	public static LevelManager Instance
	{
		get
		{
			return GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		}
	}

	IEnumerator EndLevelAndLoadNextLevel ()
	{
		float timeToStopWaiting = Time.time + 5.0f;
		while (GameObject.FindGameObjectsWithTag("Bouncer").Length > 0 || timeToStopWaiting >= Time.time)
		{
			yield return null;
		}

		Application.LoadLevel(this.NextLevelSceneName);
	}
}

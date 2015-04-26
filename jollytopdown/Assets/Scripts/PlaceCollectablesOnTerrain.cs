using UnityEngine;
using System.Collections;
using Jolly;

public class PlaceCollectablesOnTerrain : MonoBehaviour
{
	public GameObject SwarmToPlace;
	public GameObject ParentGameObject;
	public PlayerController Player;
	public float SpawnRadius;
	public int SpawnCount;

	void Start ()
	{
		Terrain terrain = this.GetComponent<Terrain> ();

		Cursor.visible = false;

		int i = 0;
		for (; i < this.SpawnCount/4; ++i) {
			float count = Random.Range (5, 20) + 5;
			SpawnBee ((int)count, terrain);

		}

		for (; i < this.SpawnCount; ++i) {
			float count = Random.Range (3, 7);
			SpawnBee ((int)count, terrain);
		}
	}

	void SpawnBee (int count, Terrain terrain)
	{
		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 0.0f, location.y);
		float y = terrain.SampleHeight (worldPosition) + count * 3 + 5;
		worldPosition = worldPosition.SetY (y);
		
		this.SwarmToPlace.GetComponent<SwarmController> ().playerSwarm = Player;
		this.SwarmToPlace.GetComponent<SwarmController> ().size = (int)count;
		GameObject swarmObject = (GameObject)GameObject.Instantiate
			(this.SwarmToPlace, worldPosition, Quaternion.identity);
		swarmObject.isStatic = true;
		swarmObject.transform.parent = this.ParentGameObject.transform;
	}
}

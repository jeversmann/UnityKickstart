using UnityEngine;
using System.Collections;
using Jolly;

public class PlaceCollectablesOnTerrain : MonoBehaviour
{
	public GameObject SwarmToPlace;
	public GameObject AnimalToPlace;
	public GameObject ParentGameObject;
	public TargetCounter targetCounter;
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

		for (int j = 0; j < 12; ++j) {
			SpawnAnimal (terrain);
			
		}

	}

	void SpawnBee (int count, Terrain terrain)
	{
		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 0.0f, location.y);
		float y = terrain.SampleHeight (worldPosition) + count * 2 + 10;
		worldPosition = worldPosition.SetY (y);
		
		this.SwarmToPlace.GetComponent<SwarmController> ().playerSwarm = Player;
		this.SwarmToPlace.GetComponent<SwarmController> ().size = (int)count;
		GameObject swarmObject = (GameObject)GameObject.Instantiate
			(this.SwarmToPlace, worldPosition, Quaternion.identity);
		swarmObject.isStatic = true;
		swarmObject.transform.parent = this.ParentGameObject.transform;
	}

	void SpawnAnimal (Terrain terrain)
	{
		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 0.0f, location.y);
		float y = terrain.SampleHeight (worldPosition) + 8;
		worldPosition = worldPosition.SetY (y);
		
		this.AnimalToPlace.GetComponent<TargetController> ().counter = targetCounter;
		GameObject animalObject = (GameObject)GameObject.Instantiate
			(this.AnimalToPlace, worldPosition, Quaternion.identity);
		animalObject.transform.parent = this.ParentGameObject.transform;
	}
}

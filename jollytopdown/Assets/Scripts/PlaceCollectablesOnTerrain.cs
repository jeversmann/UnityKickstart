using UnityEngine;
using System.Collections;
using Jolly;

public class PlaceCollectablesOnTerrain : MonoBehaviour
{
	public GameObject CollectableToPlace;
	public GameObject ParentGameObject;
	public float SpawnRadius;
	public float HeightAboveGround;
	public int SpawnCount;

	void Start ()
	{
		Terrain terrain = this.GetComponent<Terrain>();

		for (int i = 0; i < this.SpawnCount; ++i)
		{
			Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
			Vector3 worldPosition = new Vector3(location.x, 0.0f, location.y);
			float y = terrain.SampleHeight(worldPosition) + this.HeightAboveGround;
			worldPosition = worldPosition.SetY(y);

			GameObject collectableObject = (GameObject)GameObject.Instantiate(this.CollectableToPlace, worldPosition, this.CollectableToPlace.transform.rotation);
			collectableObject.isStatic = true;
			collectableObject.transform.parent = this.ParentGameObject.transform;
		}
	}
}

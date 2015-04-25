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
		Terrain terrain = this.GetComponent<Terrain> ();

		Cursor.visible = false;

		for (int i = 0; i < this.SpawnCount/2; ++i) {
			Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
			Vector3 worldPosition = new Vector3 (location.x, 0.0f, location.y);
			float y = terrain.SampleHeight (worldPosition) + Random.Range (20, 40);
			worldPosition = worldPosition.SetY (y);

			GameObject collectableObject = (GameObject)GameObject.Instantiate
				(this.CollectableToPlace, worldPosition, this.CollectableToPlace.transform.rotation);
			collectableObject.isStatic = true;
			collectableObject.transform.parent = this.ParentGameObject.transform;
			collectableObject.GetComponent<Collectable> ().SetScale (Random.Range (.2f, 1));
		}
		for (int i = 0; i < this.SpawnCount; ++i) {
			Vector2 location = Random.insideUnitCircle * this.SpawnRadius / 2;
			Vector3 worldPosition = new Vector3 (location.x, 0.0f, location.y);
			float y = terrain.SampleHeight (worldPosition) + Random.Range (2, 10);
			worldPosition = worldPosition.SetY (y);
			
			GameObject collectableObject = (GameObject)GameObject.Instantiate
				(this.CollectableToPlace, worldPosition, this.CollectableToPlace.transform.rotation);
			collectableObject.isStatic = true;
			collectableObject.transform.parent = this.ParentGameObject.transform;
			collectableObject.GetComponent<Collectable> ().SetScale (Random.Range (.01f, .2f));
		}
	}
}

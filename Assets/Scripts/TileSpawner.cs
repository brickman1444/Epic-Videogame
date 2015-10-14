using UnityEngine;
using System.Collections;

public class TileSpawner : MonoBehaviour {

    [SerializeField]
    TextAsset jsonInput = null;
    [SerializeField]
    GameObject tilePrefab = null;
    [SerializeField]
    Transform maxSpawn = null;
    [SerializeField]
    Transform minSpawn = null;

    void Start()
    {
        string encodedString = jsonInput.text;
        JSONObject root = new JSONObject(encodedString);
        JSONObject list = root["Tiles"];

        foreach (JSONObject tileJSON in list.list)
        {
            GameObject tileObject = (GameObject)GameObject.Instantiate(tilePrefab, NextSpawnPoint(), Quaternion.identity);
            Tile tileComponent = tileObject.GetComponent<Tile>();
            string topKey = tileJSON["TopKey"].str;
            string bottomKey = tileJSON["BottomKey"].str;
            string text = tileJSON["Text"].str;
            tileComponent.Initialize(topKey, bottomKey, text);
        }
    }

    Vector3 NextSpawnPoint()
    {
        float x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
        float y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

        return new Vector3(x, y, 0);
        
    }
}

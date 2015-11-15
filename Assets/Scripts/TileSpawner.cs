using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TileSpawner : SingletonBehaviour<TileSpawner> {

    [SerializeField]
    TextAsset jsonInput = null;
    [SerializeField]
    GameObject tilePrefab = null;
    [SerializeField]
    Transform maxSpawn = null;
    [SerializeField]
    Transform minSpawn = null;
    [SerializeField]
    GameObject classHeader = null;

    [ReadOnly]
    List<GameObject> tileObjects = new List<GameObject>();

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
            tileObjects.Add(tileObject);
        }

        JSONObject title = root["Class Title"];

        classHeader.GetComponent<Text>().text = title.str;

        JSONTextDisplay.instance.SetText(encodedString);
    }

    Vector3 NextSpawnPoint()
    {
        float x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
        float y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

        return new Vector3(x, y, 0);
    }

    public void DisableAllTiles()
    {
        foreach (GameObject go in tileObjects)
        {
            go.SetActive(false);
        }
    }
}

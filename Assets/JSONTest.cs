using UnityEngine;
using System.Collections;

public class JSONTest : MonoBehaviour {

    [SerializeField]
    TextAsset jsonInput = null;
    [SerializeField]
    GameObject tilePrefab = null;

	// Use this for initialization
	void Start () {
	    string encodedString = jsonInput.text;
        JSONObject root = new JSONObject(encodedString);

        JSONObject list = root["Tiles"];

        foreach( JSONObject tileJSON in list.list )
        {
            GameObject tileObject = GameObject.Instantiate(tilePrefab);
            Tile tileComponent = tileObject.GetComponent<Tile>();
            string topKey = tileJSON["TopKey"].str;

            string bottomKey = tileJSON["BottomKey"].str;

            string text = tileJSON["Text"].str;
         
            tileComponent.Initialize(topKey, bottomKey, text);
        }
    }

    //access data (and print it)
    void accessData(JSONObject obj){
	    switch(obj.type){
		    case JSONObject.Type.OBJECT:
			    for(int i = 0; i < obj.list.Count; i++){
				    string key = (string)obj.keys[i];
				    JSONObject j = (JSONObject)obj.list[i];
				    Debug.Log(key);
				    accessData(j);
			    }
			    break;
		    case JSONObject.Type.ARRAY:
			    foreach(JSONObject j in obj.list){
				    accessData(j);
			    }
			    break;
		    case JSONObject.Type.STRING:
			    Debug.Log(obj.str);
			    break;
		    case JSONObject.Type.NUMBER:
			    Debug.Log(obj.n);
			    break;
		    case JSONObject.Type.BOOL:
			    Debug.Log(obj.b);
			    break;
		    case JSONObject.Type.NULL:
			    Debug.Log("NULL");
			    break;
		
	    }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class LoadLevelOnCLick : MonoBehaviour {

    [SerializeField]
    string levelName = "";

    public void LoadLevel()
    {
        Application.LoadLevel(levelName);
    }
}

using UnityEngine;
using System.Collections;

public class ReloadOnButton : MonoBehaviour {

    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}

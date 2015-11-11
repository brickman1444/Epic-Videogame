using UnityEngine;
using System.Collections;

public class ButtonDisabler : SingletonBehaviour<ButtonDisabler> {

    [SerializeField]
    GameObject[] buttonsToDisable = new GameObject[0];

    public void DisableButtons()
    {
        foreach (GameObject go in buttonsToDisable)
        {
            go.SetActive(false);
        }
    }
}

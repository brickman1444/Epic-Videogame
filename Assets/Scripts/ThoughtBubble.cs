using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour {

    [SerializeField]
    GameObject yesEffect = null;
    [SerializeField]
    GameObject noEffect = null;

    bool isNo = false;

    public void MakeNo()
    {
        isNo = true;
        yesEffect.SetActive(false);
        noEffect.SetActive(true);
    }

    void OnMouseDown()
    {
        if (isNo)
        {
            ThoughtBubbleSpawner.instance.LoadNextLevel();
        }
    }
}

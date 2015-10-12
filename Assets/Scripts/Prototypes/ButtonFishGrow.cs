using UnityEngine;
using System.Collections;

public class ButtonFishGrow : MonoBehaviour
{

    [SerializeField]
    KeyCode spinKey;

    FishGrow fish = null;

    // Use this for initialization
    void Start()
    {
        fish = GetComponent<FishGrow>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!fish.growing && Input.GetKeyDown(spinKey))
        {
            fish.Grow();
        }
    }
}

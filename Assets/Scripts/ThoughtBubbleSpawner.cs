using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThoughtBubbleSpawner : SingletonBehaviour<ThoughtBubbleSpawner> {

    [SerializeField]
    GameObject positionParent = null;
    [SerializeField]
    GameObject thoughtBubblePrefab = null;

    public void Spawn()
    {
        List<Transform> transforms = new List<Transform>(positionParent.GetComponentsInChildren<Transform>());
        transforms.Remove(positionParent.GetComponent<Transform>());

        foreach (Transform transform in transforms)
        {
            GameObject thoughtBubbleObject = GameObject.Instantiate<GameObject>(thoughtBubblePrefab);
            thoughtBubbleObject.GetComponent<Transform>().position = transform.position;
        }
    }
}

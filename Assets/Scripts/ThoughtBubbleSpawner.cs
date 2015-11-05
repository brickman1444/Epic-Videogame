using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThoughtBubbleSpawner : SingletonBehaviour<ThoughtBubbleSpawner> {

    [SerializeField]
    GameObject positionParent = null;
    [SerializeField]
    GameObject thoughtBubblePrefab = null;
    [SerializeField]
    string nextLevelName = "";

    public void Spawn()
    {
        List<Transform> transforms = new List<Transform>(positionParent.GetComponentsInChildren<Transform>());
        transforms.Remove(positionParent.GetComponent<Transform>());

        List<ThoughtBubble> thoughtBubbles = new List<ThoughtBubble>();

        foreach (Transform transform in transforms)
        {
            GameObject thoughtBubbleObject = GameObject.Instantiate<GameObject>(thoughtBubblePrefab);
            thoughtBubbleObject.GetComponent<Transform>().position = transform.position;
            thoughtBubbles.Add(thoughtBubbleObject.GetComponentInChildren<ThoughtBubble>());
        }

        int index = Random.Range(0, thoughtBubbles.Count);

        thoughtBubbles[index].MakeNo();
    }

    public void LoadNextLevel()
    {
        if (nextLevelName.Length != 0)
        {
            Application.LoadLevel(nextLevelName);
        }
    }
}

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

    [ReadOnly,SerializeField]
    List<ThoughtBubble> bubbles = new List<ThoughtBubble>();

    [ReadOnly,SerializeField]
    int currentNoIndex = -1;

    [ReadOnly, SerializeField]
    int numNosSquashed = 0;

    [SerializeField]
    int numNosToSquash = 0;

    public virtual void Spawn()
    {
        PlaceYesBubbles();
        MakeNewNo();
    }

    protected void PlaceYesBubbles()
    {
        List<Transform> transforms = new List<Transform>(positionParent.GetComponentsInChildren<Transform>());
        transforms.Remove(positionParent.GetComponent<Transform>());

        foreach (Transform transform in transforms)
        {
            GameObject thoughtBubbleObject = GameObject.Instantiate<GameObject>(thoughtBubblePrefab);
            thoughtBubbleObject.GetComponent<Transform>().position = transform.position;
            bubbles.Add(thoughtBubbleObject.GetComponentInChildren<ThoughtBubble>());
        }
    }

    protected void MakeNewNo()
    {
        if (currentNoIndex != -1)
        {
            bubbles[currentNoIndex].MakeYes();
        }

        currentNoIndex = NewNoIndex();

        bubbles[currentNoIndex].MakeNo();
    }

    int NewNoIndex()
    {
        int index = Random.Range(0, bubbles.Count);

        if (index == currentNoIndex)
        {
            return NewNoIndex();
        }
        else
        {
            return index;
        }
    }

    public void OnBubbleClicked()
    {
        numNosSquashed++;

        if (numNosSquashed >= numNosToSquash)
        {
            LoadNextLevel();
        }
        else
        {
            MakeNewNo();
        }
    }

    public void LoadNextLevel()
    {
        if (nextLevelName.Length != 0)
        {
            Application.LoadLevel(nextLevelName);
        }
    }
}

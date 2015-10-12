using UnityEngine;
using System.Collections;

public class RandomFishGrow : MonoBehaviour {

    [SerializeField] float minWait = 0.0f;
    [SerializeField] float maxWait = 0.0f;

    FishGrow FishGrow = null;

	// Use this for initialization
	void Start () {
        FishGrow = GetComponent<FishGrow>();
        StartCoroutine(SpinManagerRoutine());
	}

    IEnumerator SpinManagerRoutine()
    {
        if (Random.Range(0, 2) == 0)
        {
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);
        }

        while (true)
        {
            FishGrow.Grow();
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

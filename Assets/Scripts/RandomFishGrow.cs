using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BearSpin))]
public class RandomBearSpin : MonoBehaviour {

    [SerializeField] float minWait = 0.0f;
    [SerializeField] float maxWait = 0.0f;

    BearSpin bearSpin = null;

	// Use this for initialization
	void Start () {
        bearSpin = GetComponent<BearSpin>();
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
            bearSpin.Spin();
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

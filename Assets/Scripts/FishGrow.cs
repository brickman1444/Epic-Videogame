using UnityEngine;
using System.Collections;

public class FishGrow : MonoBehaviour {

    [SerializeField] float growSpeed = 0.0f;

    Vector3 originalScale = Vector3.zero;

    public void Grow()
    {
        originalScale = transform.localScale;
        StartCoroutine(GrowRoutine());
    }

    IEnumerator GrowRoutine()
    {
        float t = 0;
        while (t < Mathf.PI)
        {
            t += Time.deltaTime * growSpeed;
            float uniformScale = 1 + Mathf.Sin(t);
            transform.localScale = uniformScale * originalScale;
            yield return null;
        }
        transform.rotation = Quaternion.identity;
    }
}

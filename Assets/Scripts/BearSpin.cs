using UnityEngine;
using System.Collections;

public class BearSpin : MonoBehaviour {

    [SerializeField] float spinSpeed = 0.0f;

    public bool spinning { get; private set; }

    void Start()
    {
        spinning = false;
    }

    public void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        spinning = true;
        float angle = 0;
        while (angle < 360)
        {
            angle += Time.deltaTime * spinSpeed;
            transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
            yield return null;
        }
        transform.rotation = Quaternion.identity;
        spinning = false;
    }
}

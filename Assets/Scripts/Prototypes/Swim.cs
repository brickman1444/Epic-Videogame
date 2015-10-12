using UnityEngine;
using System.Collections;

public class Swim : MonoBehaviour {

    [SerializeField] float forwardSpeed = 0.0f;
    [SerializeField] float sideSpeed = 0.0f;

    public void SwimLeft()
    {
        transform.position += Vector3.right * Time.deltaTime * forwardSpeed + Vector3.up * Time.deltaTime * sideSpeed;
    }

    public void SwimRight()
    {
        transform.position += Vector3.right * Time.deltaTime * forwardSpeed + Vector3.up * Time.deltaTime * -sideSpeed;
    }
}

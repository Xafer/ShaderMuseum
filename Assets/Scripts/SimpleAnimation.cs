using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    public Vector3 EulerRotation = new Vector3(0, 0, 0);
    public bool UseRandom = false;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= UseRandom?Quaternion.Euler(Mathf.Sin(Time.time/4)*1.2948f, Mathf.Sin(Time.time/7), Mathf.Sin(Time.time / 8.152f) * 1.2948f):
                                        Quaternion.Euler(EulerRotation * Time.deltaTime);
    }
}

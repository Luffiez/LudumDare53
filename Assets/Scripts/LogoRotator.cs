using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoRotator : MonoBehaviour
{
    float startrotation;
    [SerializeField]
    float maxRotation;
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationOffset;
    // Start is called before the first frame update
    void Start()
    {
        startrotation = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y , startrotation + Mathf.Sin(Time.time * speed) * maxRotation));
    }
}

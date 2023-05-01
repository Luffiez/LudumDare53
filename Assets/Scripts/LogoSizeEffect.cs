using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSizeEffect : MonoBehaviour
{
    float startSizeX;
    float startSizeY;

    [SerializeField]
    float maxSizeX;
    [SerializeField]
    float maxSizeY;
    [SerializeField]
    float speedX;
    [SerializeField]
    float speedY;
    [SerializeField]
    float OffsetX;
    [SerializeField]
    float OffsetY;
    // Start is called before the first frame update
    void Start()
    {
        startSizeX = transform.localScale.x;
        startSizeY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale =  new Vector3(startSizeX + Mathf.Sin(OffsetX +Time.time*speedX)*maxSizeX, startSizeY + Mathf.Sin(OffsetY + Time.time * speedY) * maxSizeY, transform.localScale.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(-8, 5, -8);

    [SerializeField]
    private float smoothSpeed = 1.25f;

    // Update is called once per frame
    void Update()
    {
        var bodyTransform = GameObject.Find("Body").transform;
        Vector3 desiredPosition = bodyTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(bodyTransform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(bodyTransform.position);
    }
}

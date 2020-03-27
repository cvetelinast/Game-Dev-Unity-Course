using System.Collections;
using UnityEngine;

public class FollowingComponent : MonoBehaviour {
    [SerializeField]
    private Vector3 offset = new Vector3(-8, 5, -8);

    [SerializeField]
    private float smoothSpeed = 1.25f;

    [SerializeField]
    private float delay = .7f;

    // Update is called once per frame
    void Update() {
        var bodyPosition = GameObject.Find("Body").transform.position;
        Vector3 desiredPosition = bodyPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(bodyPosition, desiredPosition, smoothSpeed);
        transform.LookAt(bodyPosition);

        StartCoroutine(RepositionAfterDelay(smoothedPosition));
    }

    private IEnumerator RepositionAfterDelay(Vector3 newPosition) {
        yield return new WaitForSeconds(delay);
        transform.position = newPosition;
    }
}

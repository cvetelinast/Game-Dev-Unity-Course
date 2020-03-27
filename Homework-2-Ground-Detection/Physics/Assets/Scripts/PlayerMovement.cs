using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float jumpForce = 500;

    private Vector3 previousVelocity = Vector3.zero;

    void Update() {
        MoveHorizontally();
        JumpIfPossible();
    }

    private void MoveHorizontally() {
        Vector3 moveDirection =
                new Vector3(-Input.GetAxis("Vertical"),
                            +0,
                            +Input.GetAxis("Horizontal"))
                            .normalized
                * Time.deltaTime
                * speed;
        Vector3 pointToLookAt = transform.position
                              + moveDirection * 100;

        transform.position += moveDirection;
        transform.LookAt(pointToLookAt);
    }

    private void JumpIfPossible() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var rigidbody = GetComponent<Rigidbody>();

            var acceleration = (rigidbody.velocity - previousVelocity) / Time.fixedDeltaTime;
            previousVelocity = rigidbody.velocity;

            if (IsJumpAllowed(acceleration)) {
                rigidbody.velocity = Vector3.up * jumpForce;
            }
        }
    }

    private bool IsJumpAllowed(Vector3 acceleration) {
        return AlmostEqualsZero(acceleration);
    }

    private bool AlmostEqualsZero(Vector3 first) {
        return AlmostEquals(first, Vector3.zero);
    }

    private bool AlmostEquals(Vector3 first, Vector3 second) {
        return Mathf.Round(first.x) == Mathf.Round(second.x)
        && Mathf.Round(first.y) == Mathf.Round(second.y)
        && Mathf.Round(first.z) == Mathf.Round(second.z);
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float jumpForce = 5;
    private bool isGrounded = true;
    private const string PLANE = "Plane";

    void Update() {
        MoveHorizontally();
        JumpIfPossible();
    }

    private void OnCollisionEnter(Collision theCollision) {
        if (theCollision.gameObject.name == PLANE) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision theCollision) {
        if (theCollision.gameObject.name == PLANE) {
            isGrounded = false;
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.up * jumpForce;
        }
    }
}
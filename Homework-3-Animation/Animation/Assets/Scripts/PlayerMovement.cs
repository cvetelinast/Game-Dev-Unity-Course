using UnityEngine;
using static UnityEngine.Mathf;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    [Range(0, 5)]
    private float moveSpeed = 2;

    [SerializeField]
    [Range(0.1f, 2)]
    public float gravity = 0.5f;

    private bool isCrouching = false;
    private bool isJumping = false;
    private readonly float movementThreshold = 0.01f;
    private Vector2 velocity = Vector2.zero;

    [SerializeField]
    private KeyCode jumpKey = KeyCode.W;

    [SerializeField]
    private KeyCode crouchKey = KeyCode.S;

    private Animator animator;
    private new Rigidbody2D rigidbody;

    void Start() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    void Update() {
        isCrouching = Input.GetKey(crouchKey);
        if (!isJumping) {
            animator.SetBool(Constants.IS_CROUCHING, isCrouching);
            if (isCrouching) {
                return;
            }
        }

        velocity.x = Input.GetAxis(Constants.HORIZONTAL_AXIS);
        animator.SetFloat(Constants.HORIZONTAL_MOVEMENT, Abs(velocity.x));

        if (Abs(velocity.x) > movementThreshold) {
            transform.localScale = new Vector3(Sign(velocity.x), 1, 1);
        }

        if (!isJumping && Input.GetKeyDown(jumpKey)) {
            velocity.y = 1;
            SetIsJumping(true);
        }

        rigidbody.MovePosition(rigidbody.position + velocity * moveSpeed * Time.deltaTime);

        if (isJumping) {
            velocity.y -= gravity * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Constants.FLOOR_TAG)) {
            SetIsJumping(false);
            velocity.y = 0;
        } else if (collision.collider.CompareTag(Constants.BLOCK_TAG)) {
            if (IsAboveBlock()) {
                SetIsJumping(false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag(Constants.BLOCK_TAG)) {
            SetIsJumping(true);
        }
    }

    private void SetIsJumping(bool newIsJumping) {
        isJumping = newIsJumping;
        animator.SetBool(Constants.IS_JUMPING, newIsJumping);
    }

    private bool IsAboveBlock() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        bool isAboveBlock = false;
        if (hit.collider != null && hit.collider.CompareTag(Constants.BLOCK_TAG))
            isAboveBlock = true;
        return isAboveBlock;
    }

    private bool IsBelowBlock() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        bool isBelowBlock = false;
        if (hit.collider != null && hit.collider.CompareTag(Constants.BLOCK_TAG))
            isBelowBlock = true;

        return isBelowBlock;
    }
}

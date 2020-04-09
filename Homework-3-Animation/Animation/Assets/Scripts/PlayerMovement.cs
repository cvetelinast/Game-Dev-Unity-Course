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
    private bool firstCollisionOnHitPassed = false;

    void Start() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    void Update() {
        isCrouching = Input.GetKey(crouchKey);
        if (!isJumping) {
            animator.SetBool(Constants.IS_CROUCHING, isCrouching);
        }
        if (isCrouching) return;

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
            HandleFloorCollision();
        } else if (collision.collider.CompareTag(Constants.BLOCK_TAG)) {
            HandleBlockCollision(collision);
        } else if (collision.collider.CompareTag(Constants.MUSHROOM_TAG)) {
            HandleMushroomCollision();
        }
    }

    private void HandleFloorCollision() {
        SetIsJumping(false);
        velocity.y = 0;
    }

    private void HandleBlockCollision(Collision2D collision) {
        if (IsAboveBlock(collision)) {
            SetIsJumping(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag(Constants.BLOCK_TAG)) {
            SetIsJumping(true);
        }
    }

    private void HandleMushroomCollision() {
        if (firstCollisionOnHitPassed) {
            Grow();
        } else {
            firstCollisionOnHitPassed = true;
        }
    }

    private void SetIsJumping(bool newIsJumping) {
        isJumping = newIsJumping;
        animator.SetBool(Constants.IS_JUMPING, newIsJumping);
    }

    private void Grow() {
        animator.SetBool(Constants.IS_BIG, true);
        if (GetComponent<BoxCollider2D>() != null) {
            GetComponent<BoxCollider2D>().size = new Vector2(0.14f, 0.27f);
        }
    }

    private bool IsAboveBlock(Collision2D collision) {
        bool isHigher = transform.position.y > collision.collider.transform.position.y;

        if (!isHigher) return false;

        Vector2 blockBoundaries = BoundariesByX(
                                    collision.collider as BoxCollider2D,
                                    collision.collider.transform.position.x);

        Vector2 playerBoundaries = BoundariesByX(
                                    GetComponent<BoxCollider2D>(),
                                    transform.position.x);

        bool isExactlyAbove = !((playerBoundaries.x < blockBoundaries.x && playerBoundaries.y < blockBoundaries.x) ||
                               (playerBoundaries.x > blockBoundaries.y && playerBoundaries.y > blockBoundaries.y));

        return isExactlyAbove;
    }

    private Vector2 BoundariesByX(BoxCollider2D boxCollider, float positionX) {
        float sizeX = boxCollider.size.x;
        float left = positionX - (sizeX / 2);
        float right = positionX + (sizeX / 2);
        return new Vector2(left, right);
    }

}

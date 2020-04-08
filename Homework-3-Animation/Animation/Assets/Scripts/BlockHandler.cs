using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlockHandler : MonoBehaviour {
    private Animator animator;
    public GameObject mushroomPrefab;
    private bool wasBlockHit = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!wasBlockHit) {
            if (collision.gameObject.CompareTag(Constants.PLAYER_TAG)) {
                if (IsHitBelow()) {
                    wasBlockHit = true;
                    animator.SetBool(Constants.IS_HIT_NOW, true);
                    var newMushroom = Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
    private bool IsHitBelow() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        bool isHitBelow = false;
        if (hit.collider != null && hit.collider.CompareTag(Constants.PLAYER_TAG))
            isHitBelow = true;

        return isHitBelow;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlockHandler : MonoBehaviour {
    private Animator animator;
    public GameObject mushroomPrefab;
    private bool firstCollisionOnHitPassed = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG)) {
            HandlePlayerCollision();
        }
    }

    private void HandlePlayerCollision() {
        if (!firstCollisionOnHitPassed && IsHitBelow()) {
            firstCollisionOnHitPassed = true;
            StartCoroutine(HopOnHit());
            animator.SetBool(Constants.IS_HIT_NOW, true);

            Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
        }
    }

    private bool IsHitBelow() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        bool isHitBelow = false;
        if (hit.collider != null && hit.collider.CompareTag(Constants.PLAYER_TAG))
            isHitBelow = true;

        return isHitBelow;
    }

    private IEnumerator HopOnHit() {
        transform.Translate(0f, 0.03f, 0f);
        yield return new WaitForSeconds(.1f);
        transform.Translate(0f, -0.03f, 0f);
    }
}

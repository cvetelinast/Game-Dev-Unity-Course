using System.Collections;
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
                    StartCoroutine(MoveUpAndDown());
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

    // private IEnumerator SpawnAfterDelay() {
    //     yield return new WaitForSeconds(.7f);
    //     animator.SetBool(Constants.IS_HIT_NOW, true);
    //     var newMushroom = Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
    // }
    private IEnumerator MoveUpAndDown() {
        transform.Translate(0f, 0.03f, 0f);
        yield return new WaitForSeconds(.1f);
        transform.Translate(0f, -0.03f, 0f);
        //animator.SetBool(Constants.IS_HIT_NOW, true);
        //var newMushroom = Instantiate(mushroomPrefab, transform.position, Quaternion.identity);
    }
}

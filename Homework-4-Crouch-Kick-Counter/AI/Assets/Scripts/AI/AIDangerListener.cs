using UnityEngine;
using static Controlls;
using static UnityEngine.Mathf;

public class AIDangerListener : MonoBehaviour {

    private Transform enemyTransform;

    private const float nearDistanceLimit = .3f;
    void Start() {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) {
            Debug.LogError("No GameObject with the \"Player\" tag found");
        } else {
            enemyTransform = player.transform;
        }
    }

    void Update() {
        if (ShouldAttack()) {
            Debug.Log("Should attack");
            ChooseDefenceStrategy();
        }
    }

    private bool ShouldAttack() {
        float enemyHorizontalPosition = enemyTransform.position.x;
        float myHorizontalPosition = transform.position.x;
        float enemyLookDirection = enemyTransform.localScale.x;

        bool isNearMe = Abs(enemyHorizontalPosition - myHorizontalPosition) < nearDistanceLimit;
        bool isLookingAtMe = (enemyHorizontalPosition - myHorizontalPosition) * enemyLookDirection < 0;
        bool isAttacking = IsAttacking();

        return isNearMe && isLookingAtMe && isAttacking;
    }

    private void ChooseDefenceStrategy() {
        float rand = Random.value;
        if (rand <= 0.8f) {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("ShouldCrouch");
        }
    }
}

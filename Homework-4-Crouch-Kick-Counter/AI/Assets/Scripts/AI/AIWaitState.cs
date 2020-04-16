using UnityEngine;

public class AIWaitState : StateMachineBehaviour {

    private MovementController movementController;
    private Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        movementController = animator.GetComponent<MovementController>();
        movementController.SetHorizontalMoveDirection(0);

        GameObject playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject == null) {
            Debug.LogError("No GameObject with the \"Player\" tag found");
        } else {
            player = playerGameObject.transform;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float directionToPlayer = player.position.x - animator.transform.position.x;
        movementController.TurnTowards(directionToPlayer);
    }
}

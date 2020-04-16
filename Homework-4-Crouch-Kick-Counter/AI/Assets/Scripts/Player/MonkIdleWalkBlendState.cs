using UnityEngine;
using static Controlls;
using static StateMachineUtil;

public class MonkIdleWalkBlendState : StateMachineBehaviour {

    private Animator animator;
    private MovementController movementController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.animator = animator;
        movementController = animator.GetComponent<MovementController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        DoMove(animator, movementController);

        if (IsAttacking()) {
            animator.SetTrigger("IsPunching");
        }
        if (IsJumping()) {
            animator.SetBool("IsJumping", true);
            movementController.Jump();
        }
    }
}

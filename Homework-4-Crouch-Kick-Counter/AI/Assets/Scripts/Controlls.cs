using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlls : MonoBehaviour {
    [SerializeField]
    private static KeyCode attackKey = KeyCode.E;
    [SerializeField]
    private static KeyCode jumpKey = KeyCode.W;
    public static string HorizontalMovementAxis = "Horizontal";

    [SerializeField]
    private KeyCode attackKeyBinding = attackKey;
    [SerializeField]
    private KeyCode jumpKeyBinding = jumpKey;

    public static bool IsAttacking() {
        return Input.GetKeyDown(attackKey);
    }

    public static bool IsJumping() {
        return Input.GetKeyDown(jumpKey);
    }

    // Called when the values are updated in the editor
    private void OnValidate() {
        attackKey = attackKeyBinding;
        jumpKey = jumpKeyBinding;
    }
}

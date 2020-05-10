using UnityEngine;

public class VignetteActivator : MonoBehaviour {
    [SerializeField] private DamageHandler damageHandler;

    private void OnEnable() {
        damageHandler.OnHealthChanged += changeHealth;
    }

    private void OnDisable() {
        damageHandler.OnHealthChanged -= changeHealth;
    }

    private void changeHealth(int health) {
        var effect = GetComponent<PostProcessingCameraEffect>();
        if (health <= 20 && !effect.enabled) {
            effect.enabled = true;
        } else if (health > 20 && effect.enabled) {
            effect.enabled = false;
        }
    }
}

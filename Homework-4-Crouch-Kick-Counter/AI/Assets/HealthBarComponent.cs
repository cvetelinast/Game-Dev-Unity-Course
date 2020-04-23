using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour {
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    void Update() {
        if ((IsFlippedInWorldSpace(transform) && !IsFlippedInLocalSpace(transform)) ||
             (!IsFlippedInWorldSpace(transform) && IsFlippedInLocalSpace(transform))) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private bool IsFlippedInWorldSpace(Transform transform) {
        return IsFlipped(transform.lossyScale);
    }
    private bool IsFlippedInLocalSpace(Transform transform) {
        return IsFlipped(transform.localScale);
    }
    private bool IsFlipped(Vector3 scale) {
        return scale == new Vector3(-1f, 1f, 1f);
    }
}

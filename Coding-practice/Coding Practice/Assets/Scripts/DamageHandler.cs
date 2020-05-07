using System;
using static UnityEngine.Mathf;
using UnityEngine;

public class DamageHandler : MonoBehaviour {
    public event Action<int> OnHealthChanged;
    public event Action<int> OnArmorChanged;
    private static readonly int maxHealth = 100;
    private static readonly int maxArmor = 100;
    private int health;
    private int armor;

    private float nextActionTime = 0.0f;
    public float period = 0.5f;

    void Start() {
        ChangeHealth(maxHealth);
        ChangeArmor(maxArmor);
    }

    private void Update() {
        if (Time.time > nextActionTime) {
            nextActionTime += period;
            TakeDamage();
        }
    }

    public void TakeDamage() {
        if (armor == 0) {
            TakeDamage(15, 0);
        } else {
            TakeDamage(10, 5);
        }
    }

    private void TakeDamage(int healthDamage, int armorDamage) {
        ChangeHealth(Max(health - healthDamage, 0));
        ChangeArmor(Max(armor - armorDamage, 0));
    }

    private void ChangeHealth(int newHealth) {
        health = newHealth;
        OnHealthChanged?.Invoke(health);
    }

    private void ChangeArmor(int newAmmo) {
        armor = newAmmo;
        OnArmorChanged?.Invoke(armor);
    }
}

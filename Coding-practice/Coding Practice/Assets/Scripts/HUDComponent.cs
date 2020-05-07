using System;
using UnityEngine;

public class HUDComponent : MonoBehaviour {
    [SerializeField] private DamageHandler playerDamage = null;
    [SerializeField] private PlayerAttacks playerAttacks = null;
    [SerializeField] private ThreeDigitsController healthDigitsController;
    [SerializeField] private ThreeDigitsController armorDigitsController;
    [SerializeField] private ThreeDigitsController ammoDigitsController;
    [SerializeField] private Animator headAnimator;
    [SerializeField] private Animator weaponAnimator;

    void Start() {

    }

    void Update() {

    }

    private void OnEnable() {
        playerDamage.OnHealthChanged += changeHealth;
        playerDamage.OnArmorChanged += changeArmor;
        playerAttacks.OnAmmoChanged += changeAmmo;
    }

    private void OnDisable() {
        playerDamage.OnHealthChanged -= changeHealth;
        playerDamage.OnArmorChanged -= changeArmor;
        playerAttacks.OnAmmoChanged -= changeAmmo;
    }

    private void changeHealth(int newHealth) {
        healthDigitsController?.changeValue(newHealth);
        headAnimator.SetTrigger("TakeDamage");
    }

    private void changeAmmo(int newAmmoCount) {
        ammoDigitsController?.changeValue(newAmmoCount);
        headAnimator.SetTrigger("Shoot");
    }
    private void changeArmor(int newArmor) {
        armorDigitsController?.changeValue(newArmor);
    }

}

using System;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {
    public event Action<int> OnAmmoChanged;
    private readonly float fireRate = .5f;
    private float canFire = .5f;
    private int ammoCount;
    private static readonly int maxAmmoCount = 100;

    private void Start() {
        ammoCount = maxAmmoCount;
        OnAmmoChanged?.Invoke(ammoCount);
    }
    private void Update() {
        if (Input.GetButton("Fire1") && Time.time > canFire) {
            ammoCount--;
            OnAmmoChanged?.Invoke(ammoCount);
            canFire = Time.time + fireRate;
        } else if (Input.GetButtonUp("Fire1")) {
            canFire = Time.time;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicyEffectsExecutor : MonoBehaviour {
    [SerializeField] private Health heroHealth = null;
    [SerializeField] ParticleSystem bloodParticleSystem;

    private void OnEnable() {
        heroHealth.OnDamageTaken += OnDamageTaken;
        heroHealth.OnDie += OnDie;
    }

    private void OnDisable() {
        heroHealth.OnDamageTaken -= OnDamageTaken;
        heroHealth.OnDie -= OnDie;
    }

    private void OnDamageTaken(int health) {
        ScreenShaker.ShakeScreenLight();
        AudioManager.PlayHurtSound();
        bloodParticleSystem.Play();
    }

    private void OnDie() {
        ScreenShaker.ShakeScreenHeavy();
        AudioManager.PlayDeathSound();
    }
}

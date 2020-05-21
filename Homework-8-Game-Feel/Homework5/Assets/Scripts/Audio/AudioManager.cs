using UnityEngine;
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource whooshSound = null;
    [SerializeField] private AudioSource hurtSound = null;
    [SerializeField] private AudioSource backgroundSound = null;
    [SerializeField] private AudioSource deathSound = null;
    private static AudioManager instance;

    private void Start() {
        instance = this;
    }

    public static void PlayWhooshSound() {
        instance.whooshSound.Play();
    }

    public static void PlayHurtSound() {
        instance.hurtSound.Play();
    }

    public static void PlayBackgroundSound() {
        instance.backgroundSound.Play();
    }

    public static void PlayDeathSound() {
        instance.deathSound.Play();
    }
}

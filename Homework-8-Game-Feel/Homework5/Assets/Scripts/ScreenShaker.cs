using System.Collections;
using UnityEngine;
using static UnityEngine.Mathf;

[RequireComponent(typeof(Camera))]
public class ScreenShaker : MonoBehaviour {

    [SerializeField] private AnimationCurve shakeCurve = null;

    [SerializeField]
    [Range(0, 1)]
    private float lightIntensity = 0.1f;

    [SerializeField]
    [Range(0, 2)]
    private float heavyIntensity = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    private float duration = 0.1f;
    private static ScreenShaker instance;
    private Vector3 originalPosition;

    private void Start() {
        instance = this;
        originalPosition = transform.position;
    }

    public static void ShakeScreenLight() {
        ShakeScreen(instance.lightIntensity);
    }

    public static void ShakeScreenHeavy() {
        ShakeScreen(instance.heavyIntensity);
    }

    private static void ShakeScreen(float intensity) {
        instance.StopAllCoroutines();
        instance.ResetPosition();
        instance.StartCoroutine(instance.ShakeScreenCoroutine(intensity));
    }
    private void ResetPosition() {
        instance.transform.position = originalPosition;
    }

    private IEnumerator ShakeScreenCoroutine(float intensity) {
        float shakeStart = Time.time;
        float shakeEnd = shakeStart + duration;

        float noiseSeed = Random.value * 1000;
        float cameraJiggle = intensity * 10;

        while (Time.time < shakeEnd) {
            float normalizedTime = (Time.time - shakeStart) / duration;
            float offsetX = PerlinNoise(noiseSeed + Time.time * cameraJiggle, 0);
            float offsetY = PerlinNoise(0, noiseSeed + Time.time * cameraJiggle);

            Vector3 offset = new Vector2(offsetX, offsetY)
                           * shakeCurve.Evaluate(normalizedTime)
                           * intensity;

            transform.position = transform.position + offset;
            yield return null;
        }
        ResetPosition();
    }
}

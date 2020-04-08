﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MushroomHandler : MonoBehaviour {
    private Animator animator;
    private bool collisionOnSpawn = false;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool(Constants.IS_MUSHROOM, true);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG)) {
            if (!collisionOnSpawn) {
                collisionOnSpawn = true;
            } else {
                StartCoroutine(FadeTo(0.0f, 2.0f));
            }
        } else if (collision.gameObject.CompareTag(Constants.FLOOR_TAG)) {

        }
    }

    private IEnumerator FadeTo(float aValue, float aTime) {
        var rigidbody = GetComponent<Rigidbody2D>();
        Destroy(rigidbody);
        var collider = GetComponent<BoxCollider2D>();
        Destroy(collider);

        float alpha = GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }
    }

}
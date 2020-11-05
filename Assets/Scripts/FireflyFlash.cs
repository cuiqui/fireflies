using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyFlash : MonoBehaviour
{
    enum State { CHARGING, FLASHING };

    [SerializeField] public float flashPeriod;
    [SerializeField] Color maxFlashColor;
    [SerializeField] float flashingAnimationLength;
    [SerializeField] State currentState;
    public float phase;

    private Material fMaterial;
    private Color originalColor;
    private float flashingTimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        Transform body = transform.Find("Body");
        fMaterial = body.GetComponent<MeshRenderer>().material;
        originalColor = fMaterial.GetColor("_EmissionColor");
        currentState = State.CHARGING;
    }

    bool hasFinishedCharging() {
        return (flashPeriod - phase) <= Mathf.Epsilon;
    }

    bool hasFinishedFlashing() {
        return (flashingAnimationLength - flashingTimeElapsed) <= Mathf.Epsilon;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.CHARGING) {
            phase += Time.deltaTime;
            if (hasFinishedCharging()) {
                currentState = State.FLASHING;
            }
        } else if (currentState == State.FLASHING) {
            flashingTimeElapsed += Time.deltaTime;
            float t = flashingTimeElapsed / flashingAnimationLength;
            float intensity = Mathf.Lerp(0, 8, t);
            fMaterial.SetColor("_EmissionColor", originalColor * (Mathf.Pow(intensity, 2)));
            if (hasFinishedFlashing()) {
                phase = 0;
                flashingTimeElapsed = 0;
                fMaterial.SetColor("_EmissionColor", originalColor);
                currentState = State.CHARGING;
            }
        }
    }
}

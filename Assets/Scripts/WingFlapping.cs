using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlapping : MonoBehaviour
{
    [SerializeField] float maxFlapAngle = 20;
    [SerializeField] float flapSpeed = 20f;

    private Quaternion currentTarget;
    private Quaternion maxFlap;
    private Quaternion minFlap;

    private void Start() {
        maxFlap = Quaternion.AngleAxis(maxFlapAngle, Vector3.right);
        minFlap = Quaternion.AngleAxis(-maxFlapAngle, Vector3.right);
        currentTarget = maxFlap;
    }

    private void Update() {
        if (Quaternion.Angle(transform.localRotation, currentTarget) <= Mathf.Epsilon) {
            if (currentTarget == maxFlap) {
                currentTarget = minFlap;
            } else {
                currentTarget = maxFlap;
            }
        }
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, currentTarget, flapSpeed * Time.deltaTime);
    }
}

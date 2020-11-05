using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] float maxDistanceToNewPosition = 10f;
    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float maxPitchAngle = 15f;
    [SerializeField] float pitchSpeed = 20f;
    [SerializeField] float maxRollAngle = 15f;

    Vector3 currentTarget;
    UnityEngine.Bounds bounds;

    private void Start() {
        bounds = transform.parent.GetComponent<Renderer>().bounds;
        currentTarget = getNewPosition();
    }

    Vector3 getNewPosition() {
        float scaleToDistance = Random.Range(0f, maxDistanceToNewPosition);
        Vector3 newPoint = Random.insideUnitSphere * scaleToDistance;
        return transform.position + newPoint;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentTarget) <= 0.1) {
            Vector3 newTarget = getNewPosition();
            while (!bounds.Contains(newTarget)) {
                newTarget = getNewPosition();
            }
            currentTarget = newTarget;
        }

        Vector3 newPosition = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        Quaternion newRotation = Quaternion.LookRotation(currentTarget.normalized) * transform.rotation;
        transform.position = newPosition;
        transform.rotation = Quaternion.FromToRotation(transform.up, currentTarget) * transform.rotation;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentTarget, 0.5f);
    }
}

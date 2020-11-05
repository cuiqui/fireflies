using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    [SerializeField] Transform fireflyPrefab;
    [SerializeField] int numFireflies;
    [SerializeField] int maxSpeed = 30;
    [SerializeField] int maxDistanceFromOrigin = 20;

    void Start()
    {
        for (int i = 0; i < numFireflies; i++) {
            Transform firefly = Instantiate(
                fireflyPrefab,
                Random.insideUnitSphere * maxDistanceFromOrigin + transform.position,
                Quaternion.identity
            ) as Transform;
            firefly.parent = transform;
            FireflyMovement fMovement = firefly.GetComponent<FireflyMovement>();
            FireflyFlash fFlash = firefly.GetComponent<FireflyFlash>();
            fMovement.speed = Random.Range(10, maxSpeed);
            fFlash.phase = Random.Range(0, fFlash.flashPeriod);
        }
    }
}

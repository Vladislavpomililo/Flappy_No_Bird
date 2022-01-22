using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField] private float time;

    [System.Obsolete]
    void Start()
    {
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        particle.startColor = TargetManager.targetColor;
        StartCoroutine(ObjDestroy());
    }

    private IEnumerator ObjDestroy()
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}

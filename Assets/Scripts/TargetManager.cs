using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    // Спавним шарики. Так же данный класс хранит цвет шара на который нажали
    [SerializeField] private GameObject ballon;

    [SerializeField] private float spawnPeriod;

    public static Color targetColor;

    void Start()
    {
        StartCoroutine(SpawnBallon());
    }

    IEnumerator SpawnBallon()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnPeriod);
            Instantiate(ballon);
        }
    }
}

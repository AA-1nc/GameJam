using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float maxAliveTime = 5;
    private float currentTime = 0;

    private void Update()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        currentTime += Time.deltaTime;

        if (currentTime >= maxAliveTime)
            Destroy(gameObject);
    }

    public void SetAliveTime(float time)
    {
        maxAliveTime = time;
    }
}

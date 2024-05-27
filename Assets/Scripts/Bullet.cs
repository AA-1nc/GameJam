using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private float maxAliveTime = 5;
    private float timeSinceSpawn = 0;

    public void SetAliveTime(float time)
    {
        maxAliveTime = time;
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > maxAliveTime)
            Destroy(gameObject);
    }
}

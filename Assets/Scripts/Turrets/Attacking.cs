using System;
using System.Collections;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float aimSpeed = 2.5f;
    private const int tallTurretGunRotationOffset = 90;
    
    [SerializeField] private float fireRate = 1f;
    private float lastShot = 0f;

    [SerializeField] private GameObject projectile;
    
    private Coroutine aimCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && target == null)
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
    
    private void Update()
    {
        if (target && aimCoroutine == null && IsReloaded())
        {
            attack();
        }
    }

    private bool IsReloaded()
    {
        return Time.time > lastShot + fireRate;
    }

    private void attack()
    {
        Debug.Log("start attacking");
        if (aimCoroutine != null)
        {
            StopCoroutine(aimCoroutine);
        }
        aimCoroutine = StartCoroutine(aim());
    }

    private IEnumerator aim()
    {
        Debug.Log("taking aim");
        Quaternion targetDirection = Quaternion.LookRotation(target.position - transform.position);
        targetDirection = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            targetDirection.eulerAngles.y + tallTurretGunRotationOffset,
            transform.rotation.eulerAngles.z
        );

        float time = 0;

        while (time < fireRate)
        {
            Debug.Log("RotateRotateRotate");
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, time);

            time += Time.deltaTime * aimSpeed;

            yield return null;
        }
        fire();
        aimCoroutine = null;
    }

    private void fire()
    {
        //fire bullet
        Debug.Log("Fired");
        Instantiate(projectile, transform.position, transform.rotation);
        //TODO: Make the bullet projectile appear at the gun Barrel. Maybe look in the player script how he shoots
    }

    private void searchForNewTarget()
    {
        //Needs to be implemented
    }
}

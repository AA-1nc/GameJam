using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoBehaviour
{
    private GameObject target;
    
    [SerializeField] private float aimSpeed = 2.5f;
    [SerializeField] float turretGunRotationYAxisOffset;
    [SerializeField] float turretGunPositionYAxisOffset;
    
    [SerializeField] private float fireRate = 1f;
    private float lastShot = 0f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 30f;
    
    private Coroutine aimCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && target == null)
        {
            target = other.GameObject();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && target == null)
        {
            target = other.GameObject();
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
            lastShot = Time.time;
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
        Quaternion targetDirection = Quaternion.LookRotation(target.transform.position - transform.position);
        targetDirection = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            targetDirection.eulerAngles.y + turretGunRotationYAxisOffset,
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

        if (target)
        {
            fire();
        }
        
        aimCoroutine = null;
    }

    private void fire()
    {
        Debug.Log("Fired");
 
        Vector3 targetCenter = FirstOrderIntercept(new Vector3(transform.position.x, transform.position.y + turretGunPositionYAxisOffset, transform.position.z), 
            Vector3.zero, 
            bulletSpeed, 
            target.transform.position, 
            target.GetComponent<Rigidbody>().velocity);

        Instantiate(bulletPrefab, new Vector3(
            transform.position.x,
            transform.position.y + turretGunPositionYAxisOffset,
                transform.position.z), 
                Quaternion.LookRotation(targetCenter));
    }
    
    
    
    
    
    //first-order intercept using absolute target position
    public static Vector3 FirstOrderIntercept
    (
        Vector3 shooterPosition,
        Vector3 shooterVelocity,
        float shotSpeed,
        Vector3 targetPosition,
        Vector3 targetVelocity
    )
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetRelativePosition + t * (targetRelativeVelocity);
    }
    
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;
 
        float a = velocitySquared - shotSpeed * shotSpeed;
 
        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }
 
        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;
 
        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }
 
}

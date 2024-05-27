using System.Collections;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float aimSpeed = 1f;
    private const int tallTurretGunRotationOffset = 90;

    private Coroutine aimCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && target == null)
        {
            target = other.transform;
            aimCoroutine = StartCoroutine(aim());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    private void attack()
    {
        //Start aim() Coroutine and reloading
    }

    private IEnumerator aim()
    {
        Quaternion targetDirection = Quaternion.LookRotation(target.position - transform.position);
        targetDirection = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            targetDirection.eulerAngles.y + tallTurretGunRotationOffset,
            transform.rotation.eulerAngles.z
        );

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, time);

            time += Time.deltaTime * aimSpeed;

            yield return null;
        }
    }

    private void searchForNewTarget()
    {
        //Needs to be implemented
    }
}

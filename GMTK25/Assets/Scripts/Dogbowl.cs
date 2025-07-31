using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogbowl : MonoBehaviour
{
    [SerializeField] float dashRecoveryInterval;
   bool canRecover = true;
    public delegate void OnDashRecovery();
    public static event OnDashRecovery onDashRecovery;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.CompareTag("Player"))
           // onDashRecovery?.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canRecover)
            StartCoroutine(DogbowlTimer());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canRecover = false;
    }

    public IEnumerator DogbowlTimer()
    {   if (canRecover)
        {
            onDashRecovery?.Invoke();
            Debug.Log("dogbowl");
        }
        canRecover = false;
        yield return new WaitForSeconds(dashRecoveryInterval);
        canRecover = true;
    }
}

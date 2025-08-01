using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSheepZone : MonoBehaviour
{
    [SerializeField] float zoneForce;
    [SerializeField] CapsuleCollider2D blackSheepZone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (!collision.gameObject.CompareTag("Sheep"))
            return;
        if (collision.gameObject.GetComponent<Sheep>().sheepSO.type == Sheep.SheepType.BLACK)
            return;

        Vector3 forceDirection = (gameObject.transform.position - collision.gameObject.transform.position).normalized;

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * -zoneForce * Time.deltaTime);
        //Debug.Log("Adding force " + forceDirection + " to " + collision.gameObject.name);
    }
}

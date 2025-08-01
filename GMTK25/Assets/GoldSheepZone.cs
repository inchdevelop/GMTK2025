using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSheepZone : MonoBehaviour
{
    [SerializeField] float zoneForce;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Sheep>())
            return;
        if (collision.gameObject.GetComponent<Sheep>().sheepSO.type == Sheep.SheepType.GOLD)
            return;
        Vector3 forceDirection = (gameObject.transform.position - collision.gameObject.transform.position).normalized;

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * zoneForce * Time.deltaTime);
        Vector3 lookDirection = (gameObject.transform.position - collision.gameObject.transform.position).normalized;
        collision.gameObject.transform.right = lookDirection * Time.deltaTime;
        //Debug.Log("Adding force " + forceDirection + " to " + collision.gameObject.name);
    }
}

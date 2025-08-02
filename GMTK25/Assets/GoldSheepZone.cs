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
        if (!collision.gameObject.CompareTag("Sheep"))
            return;
        if (collision.gameObject.GetComponent<Sheep>().sheepSO.type == Sheep.SheepType.GOLD)
            return;

        Vector3 awayDirection = ((gameObject.transform.position - collision.gameObject.transform.position) * zoneForce).normalized;
        Vector3 awayPos = collision.gameObject.transform.position + awayDirection;

        collision.gameObject.GetComponent<Sheep>().currentState = Sheep.SheepMovement.FOLLOW;
        collision.gameObject.GetComponent<Sheep>().targetPos = awayPos;

        //Debug.Log(collision.gameObject.name + " following " + awayPos);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Sheep"))
            return;
        if (collision.gameObject.GetComponent<Sheep>().sheepSO.type == Sheep.SheepType.GOLD)
            return;
        if (collision.gameObject.GetComponent<Sheep>().sheepSO.type == Sheep.SheepType.BLACK)
            return;
        collision.gameObject.GetComponent<Sheep>().currentState = Sheep.SheepMovement.MOVE;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSheepZone : MonoBehaviour
{
    [SerializeField] Vector3 zoneForce;
    [SerializeField] CapsuleCollider2D blackSheepZone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        return;
        if (!collision.gameObject.CompareTag("Sheep"))
            return;
        if (collision.gameObject.GetComponent<Sheep>().sheepSO.type == Sheep.SheepType.BLACK)
            return;

        Sheep theSheep = collision.gameObject.GetComponent<Sheep>();
        bool isInZone = true;
        while(isInZone)
        {
            isInZone = false;
            theSheep.ChangeTargetPos();
            if (blackSheepZone.bounds.Contains(theSheep.targetPos))
                isInZone = true;
            Debug.Log("changing target");
        }
    }
}

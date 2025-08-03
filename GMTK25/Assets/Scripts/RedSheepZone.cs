using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSheepZone : MonoBehaviour
{
    [SerializeField] CapsuleCollider2D redSheepZone;
    [SerializeField] List<GameObject> explosionList = new List<GameObject>();
    private void OnEnable()
    {
        Sheep.onSheepCollide += Explode;
    }

    private void OnDisable()
    {
        Sheep.onSheepCollide -= Explode;
    }

    void Explode()
    {
        Debug.Log("red sheep exploding");
        redSheepZone.enabled = true;
    }

    public void Explosion()
    {
        Debug.Log("exploding");
        redSheepZone.enabled = false;
        for(int i = 0; i < explosionList.Count; i++)
        {
            SheepManager.instance.DestroySheep(explosionList[i]);
        }

        SheepManager.instance.DestroySheep(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Sheep"))
            return;
        Sheep theSheep = collision.gameObject.GetComponent<Sheep>();
        if (theSheep.sheepSO.type == Sheep.SheepType.RED)
            theSheep.GetComponent<RedSheepZone>().Explode();
        else
            explosionList.Add(collision.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class lassoDetect : MonoBehaviour
{
    public delegate void OnSheepCollected(int score);
    public static event OnSheepCollected onSheepCollected;
    int sheepCount = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(killCode());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Sheep")
        {
            sheepCount++;
            onSheepCollected?.Invoke(collider.gameObject.GetComponent<Sheep>().sheepSO.scoreValue);
            SheepManager.instance.DestroySheep(collider.gameObject);
        }
    }

    IEnumerator killCode()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(sheepCount);
        Destroy(gameObject);
    }
}

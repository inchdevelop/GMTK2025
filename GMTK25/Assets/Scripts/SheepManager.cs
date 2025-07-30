using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    [SerializeField] GameObject[] sheepPrefabs;
    [SerializeField] List<GameObject> currentSheep;

    [SerializeField] BoxCollider2D spawnZone;

    [SerializeField] int startSheep;
    void Start()
    {
        SpawnNumSheep(startSheep);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnNumSheep(int num)
    {
        for(int i = 0; i < num; i++)
        {
            SpawnSheep();
        }
    }

    void SpawnThisSheepAt(Sheep.SheepType type, Vector2 spawn)
    {
        GameObject sheepObject = sheepPrefabs[(int)type];
        GameObject newSheep = Instantiate(sheepObject, this.gameObject.transform);
        newSheep.transform.position = spawn;

        currentSheep.Add(newSheep);
    }

    void SpawnSheep()
    {
        bool canSpawn = false;
      //  while(!canSpawn)
     //   {

      //  }
        SpawnThisSheepAt(GetRandomSheepType(), GetRandomSpawnLocation());
        
    }

    Sheep.SheepType GetRandomSheepType()
    {
        Sheep.SheepType type = (Sheep.SheepType)Random.Range(0, (float)Sheep.SheepType.NUM_SHEEP);
        return type;
    }

    bool CanSpawnAt(Vector2 spawn)
    {
        bool canSpawn = false;


        return canSpawn;
    }

    Vector2 GetRandomSpawnLocation()
    {
        Vector2 spawn;
        Vector2 leftBounds;
        Vector2 rightBounds;
        Bounds bounds = spawnZone.bounds;
        leftBounds = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
        rightBounds = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y  + bounds.extents.y);
        spawn = new Vector2( Random.Range(leftBounds.x, rightBounds.x),Random.Range(leftBounds.y, rightBounds.y));
        return spawn;

    }
}

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
    [SerializeField] float spawnInterval;
    bool canSpawnSheep = true;

    public static SheepManager instance;

    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        SpawnNumSheep(startSheep);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawnSheep)
        {
            StartCoroutine(SheepSpawnTimer());
        }
    }

    public void DeleteSheep(GameObject sheep)
    {
        currentSheep.Remove(sheep);
    }

    public void DestroySheep(GameObject sheep)
    {
        currentSheep.Remove(sheep);
        
        Sheep theSheep = sheep.GetComponent<Sheep>();
        theSheep.SheepKnockUp();
    }

    public IEnumerator SheepSpawnTimer()
    {
        canSpawnSheep = false;
        SpawnSheep();
        //Debug.Log("Spawned sheep");
        yield return new WaitForSeconds(spawnInterval);
        canSpawnSheep = true;
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

        if (currentSheep.Count >= GameManager.instance.maxSheep)
            onGameOver?.Invoke();

    }

    void SpawnSheep()
    {
        bool canSpawn = false;
        Vector2 spawnPos = Vector2.zero;
        while(!canSpawn)
        {
            //check for if position is in bounds of any other sheep 

            spawnPos = GetRandomSpawnLocation();
            canSpawn = CanSpawnAt(spawnPos);
        }
        SpawnThisSheepAt(GetRandomSheepType(), spawnPos);
        
    }

    Sheep.SheepType GetRandomSheepType()
    {
        Sheep.SheepType type = (Sheep.SheepType)Random.Range(0, (float)Sheep.SheepType.NUM_SHEEP);
        return type;
    }

    bool CanSpawnAt(Vector2 spawn)
    {
        bool canSpawn = true;
        foreach (GameObject sheep in currentSheep)
        {
            if (sheep.GetComponent<Sheep>().spawnBuffer.bounds.Contains(spawn))
            {
                canSpawn = false;
                //Debug.Log(spawn + " is located in " + sheep.name);
            }

        }
            return canSpawn;
    }

    public Vector2 GetRandomSpawnLocation()
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

using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform spawnNPCPoint;
    [SerializeField] private KeyCode spawnKey = KeyCode.N;
    [SerializeField] private int maxNPCs = 10;
    [SerializeField] private float spawnCooldown = 2.0f;

    private int currentNPCs = 0;
    private float lastSpawnTime = 0.0f;

    void Start()
    {
        if (spawnNPCPoint == null)
        {
            spawnNPCPoint = GameObject.Find("SpawnNPC").transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(spawnKey) && CanSpawn())
        {
            SpawnNPC();
        }
    }

    bool CanSpawn()
    {
        return currentNPCs < maxNPCs && Time.time - lastSpawnTime >= spawnCooldown;
    }

    void SpawnNPC()
    {
        if (npcPrefab != null && spawnNPCPoint != null)
        {
            Instantiate(npcPrefab, spawnNPCPoint.position, spawnNPCPoint.rotation);
            currentNPCs++;
            lastSpawnTime = Time.time;
        }
        else
        {
            Debug.LogWarning("NPC prefab or SpawnNPC point is not set.");
        }
    }
}
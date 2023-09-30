using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class SpawnGridCube : MonoBehaviour
{
    [Header("Configurations")]
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;
    [SerializeField] Vector3 gridOrigin = Vector3.zero;

    [Header("Floor")]
    [SerializeField] GameObject[] blockFloorToPickFrom;
    GameObject blockFloor;

    [Header("Environments")]
    [SerializeField] GameObject[] environmentsToPickFrom;
    GameObject blockEnvironment;
    [Range(0f, 1f)]
    [SerializeField] float chanceToSpawnEnvironment;

    [Header("Map")]
    public List<PartMap> map;
    PartMap peaceMap;

    [Header("Player")]
    Player player;

    private void Awake()
    {
        map = new List<PartMap>();

        player = PhotonNetwork.LocalPlayer;

        if (player.IsMasterClient && player != null)
        {
            SpawnGridFloor();
        }
    }

    void Start()
    {
        SpawnGridFloor();
    }

    void SpawnGridFloor()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                //Spawn Floor
                Vector3 spawnPositionFloor = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawnFloor(spawnPositionFloor, Quaternion.identity);

                if ((x > 0 && x < (gridX - 1)))
                {
                    //Spawn Environment
                    Vector3 spawnPositionEnvironment = new Vector3(x * gridSpacingOffset, gridSpacingOffset / 2, z * gridSpacingOffset) + gridOrigin;
                    PickAndSpawnEnvironment(spawnPositionEnvironment, Quaternion.identity);

                    peaceMap = new PartMap
                    {
                        floor = blockFloor,
                        spawnPositionFloor = spawnPositionFloor,
                        environment = blockEnvironment,
                        spawnPosEnvironment = spawnPositionEnvironment
                    };
                }
                else
                {
                    peaceMap = new PartMap
                    {
                        floor = blockFloor,
                        spawnPositionFloor = spawnPositionFloor,
                        environment = blockEnvironment,
                        spawnPosEnvironment = Vector3.zero
                    };
                }

                map.Add(peaceMap);
            }
        }
    }

    void PickAndSpawnFloor(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, blockFloorToPickFrom.Length); 
        blockFloor = PhotonNetwork.Instantiate(blockFloorToPickFrom[randomIndex].name, positionToSpawn, rotationToSpawn, 0);
    }

    void PickAndSpawnEnvironment(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        float chanceToSpawn = Random.Range(0f, 1f);

        if (chanceToSpawn <= chanceToSpawnEnvironment)
        {
            int randomIndex = Random.Range(0, environmentsToPickFrom.Length);
            blockEnvironment = PhotonNetwork.Instantiate(environmentsToPickFrom[randomIndex].name, positionToSpawn, rotationToSpawn, 0);
        }
    }
}

[System.Serializable]
public class PartMap
{
    public GameObject floor;
    public Vector3 spawnPositionFloor;
    public GameObject environment;
    public Vector3 spawnPosEnvironment;
}

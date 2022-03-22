using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    const float MIN_PLAYER_DIST = 100f;
    //[SerializeField] private Transform levelPart_Start;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Transform levelPart_1;
    [SerializeField] private Transform levelPart_2;
    [SerializeField] private Transform myelinD_1;
    [SerializeField] private Transform myelinH_1;
    [SerializeField] private Transform myelinD_2;
    [SerializeField] private Transform myelinH_2;

    [SerializeField] private PlayerMovement player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        //lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastEndPosition = startPos;

        for (int i = 0; i < 5; i++)
        {
            SpawnLevelPart();
        }

    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < MIN_PLAYER_DIST) {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Vector3 spawnPos)
    {
        Transform levelPartTransform = null;
        Quaternion spawnRot = Quaternion.Euler(0f, 180f, Random.Range(0f, 360f));
        int partToSpawn = Random.Range(1, 2); // make 1-3 later
        int myelinToSpawn = Random.Range(1, 10);
        switch (partToSpawn) {
            case 1:
                spawnPos.z += 30;
                levelPartTransform = Instantiate(levelPart_1, spawnPos, spawnRot);
                if (myelinToSpawn > 8)
                {
                    Instantiate(myelinD_1, spawnPos, spawnRot);
                }
                else 
                {
                    Instantiate(myelinH_1, spawnPos, spawnRot);
                }
                    break;
            case 2:
                spawnPos.z += 25;
                levelPartTransform = Instantiate(levelPart_2, spawnPos, spawnRot);
                if (myelinToSpawn > 8)
                {
                    Instantiate(myelinD_2, spawnPos, spawnRot);
                }
                else
                {
                    Instantiate(myelinH_2, spawnPos, spawnRot);
                }
                break;
            case 3:
                break;
        }
        
        return levelPartTransform;
    }
}

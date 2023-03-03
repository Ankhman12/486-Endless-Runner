using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    const float MIN_PLAYER_DIST = 100f;
    //[SerializeField] private Transform levelPart_Start;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Transform startPart;
    [SerializeField] private Transform levelPart_1D;
    [SerializeField] private Transform levelPart_1H;
    [SerializeField] private Transform levelPart_2D;
    [SerializeField] private Transform levelPart_2H;

    [SerializeField] private PlayerMovement player;

    private Vector3 lastEndPosition;
    private Queue<GameObject> levelParts = new Queue<GameObject>();

    private void Awake()
    {
        //lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastEndPosition = startPos;
        Instantiate(startPart, new Vector3(startPos.x, startPos.y, startPos.z - 6.66f), Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            SpawnLevelPart();
        }


    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < MIN_PLAYER_DIST) {
            SpawnLevelPart();
            Destroy(levelParts.Dequeue());
        }
    }

    private void SpawnLevelPart() {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelParts.Enqueue(lastLevelPartTransform.gameObject);
    }

    private Transform SpawnLevelPart(Vector3 spawnPos)
    {
        Transform levelPartTransform = null;
        Quaternion spawnRot; //= Quaternion.Euler(0f, 180f, Random.Range(0f, 360f));
        int partToSpawn = Random.Range(1, 3); 
        int myelinToSpawn = Random.Range(1, 11);
        switch (partToSpawn) {
            case 1:
                spawnPos.z += 30;
                spawnRot = Quaternion.Euler(0f, 180f, Random.Range(0f, 360f));
                if (myelinToSpawn > 8)
                {
                    levelPartTransform = Instantiate(levelPart_1D, spawnPos, spawnRot);
                }
                else 
                {
                    levelPartTransform = Instantiate(levelPart_1H, spawnPos, spawnRot);
                }
                    break;
            case 2:
                spawnPos.z += 25;
                spawnRot = Quaternion.Euler(0f, 180f, Random.Range(0f, 360f));
                if (myelinToSpawn > 8)
                {
                    levelPartTransform = Instantiate(levelPart_2D, spawnPos, spawnRot);
                }
                else
                {
                    levelPartTransform = Instantiate(levelPart_2H, spawnPos, spawnRot);
                }
                break;
            case 3:
                break;
        }
        
        return levelPartTransform;
    }
}

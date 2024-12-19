using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngreSpawner : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 3f;
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public int spawnCount = 5;
    public float spawnDelay;

    private Vector3 initialPosition;
    private bool isMovingRight = true; 

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(SpawnObjects()); 
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpawner();
    }

    void MoveSpawner()
    {
        if (isMovingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= initialPosition.x + moveDistance)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x <= initialPosition.x - moveDistance)
            {
                isMovingRight = true;
            }
        }
    }

    private IEnumerator SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

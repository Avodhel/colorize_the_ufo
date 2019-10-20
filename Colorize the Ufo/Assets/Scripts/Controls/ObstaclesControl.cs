using System.Collections;
using UnityEngine;

public class ObstaclesControl : MonoBehaviour
{
    [Header("Spawn Obstacle")]
    [SerializeField]
    private float startWaitTime = 6f;
    [SerializeField]
    private float spawnObstacleTime = 6f;

    private void Start()
    {
        StartCoroutine(spawnObstacles());
    }

    private IEnumerator spawnObstacles()
    {
        yield return new WaitForSeconds(startWaitTime);
        while (true)
        {
            Vector3 obstaclePos = new Vector3(Random.Range(-10f, -2f), 7f);
            GameObject obstacle = ObstaclePooler.SharedInstance.GetPooledObject(Random.Range(0, ObstaclePooler.SharedInstance.itemsToPool.Count));
            obstacle.SetActive(true);
            obstacle.transform.position = obstaclePos;
            obstacle.GetComponent<IObstacle>().obstacleMovement();
            obstacle.GetComponent<IObstacle>().changeColor();

            yield return new WaitForSeconds(spawnObstacleTime);
        }
    }
}

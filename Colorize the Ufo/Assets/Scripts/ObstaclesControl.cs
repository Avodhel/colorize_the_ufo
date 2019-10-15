using System.Collections;
using UnityEngine;

public class ObstaclesControl : MonoBehaviour {

    [Header("Obstacles")]
    [SerializeField]
    private GameObject[] obstacleTypes;

    [Header("Spawn Obstacle")]
    [SerializeField]
    private float startWaitTime = 6f;
    [SerializeField]
    private float spawnObstacleTime = 6f;

    private GameObject chooseObstacle;
    private GameObject obstacle;

    private void Start()
    {
        StartCoroutine(spawnObstacles());
    }

    private IEnumerator spawnObstacles()
    {
        yield return new WaitForSeconds(startWaitTime);
        while (true)
        {
            chooseObstacle = obstacleTypes[Random.Range(0, obstacleTypes.Length)];
            Vector3 obstaclePos = new Vector3(Random.Range(-10f, -2f), 7f);
            obstacle = Instantiate(chooseObstacle, obstaclePos, Quaternion.identity );
            Rigidbody2D rb2D = obstacle.AddComponent<Rigidbody2D>(); //engelimize koddan rigidbody ekledik.
            rb2D.bodyType = RigidbodyType2D.Kinematic; //!!!dynamic olunca engeller savruluyor
            rb2D.gravityScale = 0; //oluşan engellerin düşmemesi için 
            rb2D.velocity = new Vector2(0, -2); // engellerimizin hareket etmesi için.

            yield return new WaitForSeconds(spawnObstacleTime);
        }
    }
}

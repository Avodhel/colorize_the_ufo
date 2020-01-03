using System.Collections;
using UnityEngine;

public class SpaceObjectsControl : MonoBehaviour {

    [Header("Spawn Objects")]
    [SerializeField]
    private float startSpawnTime = 2f;
    [SerializeField]
    private Vector2 randomPos = new Vector2(3f, 5.5f);

    [Header("Spawn Flying Stones")]
    [SerializeField]
    private float spawnFlyingStoneTime = 0.1f;
    [SerializeField]
    private float spawnFlyingStonesGroupTime = 5f;
    [SerializeField]
    private int minFlyingStonesRate = 12;
    [SerializeField]
    private int maxFlyingStonesRate = 30;

    [Header("Spawn Rare Objects")]
    [SerializeField]
    private float spawnRareObjectTime = 0.5f;
    [SerializeField]
    private float spawnRareObjectsGroupTime = 5f;
    [SerializeField]
    private int minRareObjectsRate = 0;
    [SerializeField]
    private int maxRareObjectsRate = 3;

    private void Start ()
    {
        StartCoroutine(SpawnFlyingStones());
        StartCoroutine(SpawnRareObjects());
	}
	
    private IEnumerator SpawnFlyingStones()
    {
        yield return new WaitForSeconds(startSpawnTime);

        while (true) // sonsuz bir döngü 
        {
            for (int i = 0; i < Random.Range(minFlyingStonesRate, maxFlyingStonesRate); i++)
            {
                Vector2 vec2Pos = new Vector2(Random.Range(-randomPos.x, randomPos.x), randomPos.y);
                GameObject FS = FlyingStonePooler.SharedInstance.GetPooledObject(Random.Range(0, FlyingStonePooler.SharedInstance.itemsToPool.Count));
                FS.SetActive(true);
                FS.transform.position = vec2Pos;
                FS.GetComponent<ISpaceObject>().ObjectMovement();

                yield return new WaitForSeconds(spawnFlyingStoneTime);
            }
            yield return new WaitForSeconds(spawnFlyingStonesGroupTime);
        }
    }

    private IEnumerator SpawnRareObjects()
    {
        yield return new WaitForSeconds(10); //başlangıç bekleme suresi

        while (true)
        {
            for (int i = 0; i < Random.Range(minRareObjectsRate, maxRareObjectsRate); i++)
            {
                Vector2 vec2Pos = new Vector2(Random.Range(-randomPos.x, randomPos.x), randomPos.y);
                GameObject RO = RareObjectPooler.SharedInstance.GetPooledObject(Random.Range(0, RareObjectPooler.SharedInstance.itemsToPool.Count));
                RO.SetActive(true);
                RO.transform.position = vec2Pos;
                RO.GetComponent<ISpaceObject>().ObjectMovement();
                if (RO.transform.tag != "meteorTag")
                {
                    RO.transform.GetComponent<CircleCollider2D>().isTrigger = false;
                }

                yield return new WaitForSeconds(spawnRareObjectTime);
            }
            yield return new WaitForSeconds(spawnRareObjectsGroupTime);
        }
    }
}

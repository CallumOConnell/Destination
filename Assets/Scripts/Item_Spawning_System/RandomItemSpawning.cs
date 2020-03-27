using UnityEngine;

public class RandomItemSpawning : MonoBehaviour
{
    public Transform parent;

    public GameObject[] items;

    public Transform[] spawnPositions;

    private void Start()
    {
        foreach (Transform spawnPoint in spawnPositions)
        {
            GameObject item = SpawnRandomItem();

            Instantiate(item, spawnPoint.position, Random.rotation, parent);
        }
    }

    private GameObject SpawnRandomItem()
    {
        int itemIndex = Random.Range(0, items.Length);

        return items[itemIndex];
    }
}

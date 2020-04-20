using UnityEngine;

public class RandomItemSpawning : MonoBehaviour
{
    public Transform itemHolder;

    public GameObject[] items;

    public Transform[] spawnPositions;

    public void PopulateItems()
    {
        foreach (Transform spawnPoint in spawnPositions)
        {
            GameObject item = SpawnRandomItem();

            Instantiate(item, spawnPoint.position, Random.rotation, itemHolder);
        }
    }

    private GameObject SpawnRandomItem() => items[Random.Range(0, items.Length)];
}

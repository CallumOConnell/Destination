using Destination;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Initialisation : MonoBehaviour
{
    public static Initialisation current;

    [Space, Header("Initialisation Settings")]
    public float progress;

    public bool isDone;

    public InitialisationStage currentStage;

    [Space, Header("Enemy Settings")]
    public Transform enemyHolder;

    public GameObject enemyPrefab;

    public List<GameObject> positions;

    public int amountToPool;

    [Space, Header("Loot Settings")]
    public Transform itemHolder;

    public GameObject[] itemsPrefabs;

    public Transform[] spawnPositions;

    private List<GameObject> pooledObjects;

    [Space, Header("Inventory Settings")]
    public InventoryObject inventory;

    public ItemObject startingWeapon;

    private void Awake()
    {
        current = this;

        PoolObjects();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(PopulateLoot());
        StartCoroutine(CreateCharacter());
    }

    private void PoolObjects()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, enemyHolder);

            obj.SetActive(false);

            pooledObjects.Add(obj);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        currentStage = InitialisationStage.Enemies;

        ResetEnemies();

        int count = 0;

        foreach (GameObject spawnPoint in positions)
        {
            GameObject enemy = GetPooledObject();

            if (enemy != null)
            {
                enemy.GetComponent<NavMeshAgent>().Warp(spawnPoint.transform.position);

                enemy.SetActive(true);
            }

            progress = count / (float)positions.Count;

            count++;
        }

        yield return new WaitForSeconds(0.5f);

        isDone = true;
    }

    private GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    private void ResetEnemies()
    {
        foreach (GameObject enemy in pooledObjects)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.SetActive(false);
            }
        }
    }

    private IEnumerator PopulateLoot()
    {
        currentStage = InitialisationStage.Loot;

        int count = 0;

        foreach (Transform spawnPoint in spawnPositions)
        {
            GameObject item = SpawnRandomItem();

            Instantiate(item, spawnPoint.position, spawnPoint.rotation, itemHolder);

            progress = count / (float)spawnPositions.Length;

            count++;
        }

        yield return new WaitForSeconds(0.5f);

        isDone = true;
    }

    private GameObject SpawnRandomItem() => itemsPrefabs[Random.Range(0, itemsPrefabs.Length)];

    private IEnumerator CreateCharacter()
    {
        currentStage = InitialisationStage.Character;

        for (int i = 0; i < 60; i++)
        {
            //Debug.Log("Fake Wait");

            //yield return new WaitForSeconds(1f);

            progress = i / (float)60;
        }

        inventory.Clear();

        inventory.AddItem(new Item(startingWeapon), 1);

        yield return new WaitForSeconds(0.5f);

        isDone = true;
    }
}

public enum InitialisationStage
{
    Enemies,
    Loot,
    Character
}
using System.Collections.Generic;
using UnityEngine;

namespace Destination
{
    public class RespawnEnemies : MonoBehaviour
    {
        public Transform enemyHolder;

        public GameObject enemyPrefab;

        public List<GameObject> pooledObjects;

        public List<GameObject> positions;

        public int amountToPool;

        private void Start()
        {
            pooledObjects = new List<GameObject>();

            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(enemyPrefab, enemyHolder);

                obj.SetActive(false);

                pooledObjects.Add(obj);
            }
        }

        public void SpawnEnemies()
        {
            ResetEnemies();

            foreach (GameObject spawnPoint in positions)
            {
                GameObject enemy = GetPooledObject();

                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.transform.position;

                    enemy.SetActive(true);
                }
            }
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
    }
}
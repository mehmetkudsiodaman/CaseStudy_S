using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;
        public List<GameObject> pooledObjects;
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;
        [SerializeField] private List<Transform> spawnPoints;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                int spawnPoint = Random.Range(0, 10);
                tmp = Instantiate(objectToPool);
                tmp.transform.SetParent(transform);
                tmp.transform.localPosition = spawnPoints[spawnPoint].localPosition;
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}
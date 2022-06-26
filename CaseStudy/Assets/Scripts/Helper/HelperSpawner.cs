using System;
using UnityEngine;
using Zone;

namespace Helper
{
    public class HelperSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject helperPrefab;
        private ZoneHandler zoneHandler;

        private void Awake()
        {
            zoneHandler = FindObjectOfType<ZoneHandler>();
        }

        private void Start()
        {
            Instantiate(helperPrefab, this.transform.position, Quaternion.identity, this.transform);
            zoneHandler.OnZoneUnlocked += HelperSpawner_OnZoneUnlocked;
        }

        private void HelperSpawner_OnZoneUnlocked(object sender, EventArgs e)
        {
            Instantiate(helperPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
    }
}
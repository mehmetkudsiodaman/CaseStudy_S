using System;
using UnityEngine;
using Zone;

namespace Helper
{
    public class HelperSpawner : MonoBehaviour
    {
        ////  TODO: Zonehandler cache hatalý, onu düzelteceðim!
        ////  Her zone açýlýnca bir sonrakini cache'lemek gerek!
        [SerializeField] private GameObject helperPrefab;

        [SerializeField] private GameObject[] cubeSpawnZone;
        [SerializeField] private ZoneHandler zoneHandler;

        public static event EventHandler<OnHelperSpawnedEventArgs> OnHelperSpawned;

        public class OnHelperSpawnedEventArgs : EventArgs
        {
            public GameObject helperPrefab;
        }

        private void Start()
        {
            zoneHandler.OnZoneUnlocked += HelperSpawner_OnZoneUnlocked;
        }

        //FixMe: Bazen helper spawn olmuyor, bölge de açýlmýyor!
        private void HelperSpawner_OnZoneUnlocked(object sender, ZoneHandler.OnZoneUnlockedEventArgs e)
        {
            //FixMe:zoneorder 2 þimdilik dursun!
            if (e.zoneOrder == 2 || e.zoneOrder == 3 || e.zoneOrder == 5 || e.zoneOrder == 7)
            {
                var helper = Instantiate(helperPrefab, e.zonePosition, Quaternion.identity, this.transform);
                //helper.transform.localPosition = Vector3.zero;
                OnHelperSpawned?.Invoke(this, new OnHelperSpawnedEventArgs { helperPrefab = helperPrefab });
            }

            //FixMe:zoneorder 5 ve 7 olacak, þimdilik 2 ve 3 yapýyorum!
            if (e.zoneOrder == 2)
            {
                cubeSpawnZone[0].SetActive(true);
            }

            if (e.zoneOrder == 3)
            {
                cubeSpawnZone[1].SetActive(true);
            }

            zoneHandler = e.zoneHandler;
            zoneHandler.OnZoneUnlocked += HelperSpawner_OnZoneUnlocked;
        }
    }
}
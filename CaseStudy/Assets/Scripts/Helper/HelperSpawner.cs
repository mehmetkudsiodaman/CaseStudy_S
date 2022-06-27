using UnityEngine;
using Zone;

namespace Helper
{
    public class HelperSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject helperPrefab;
        [SerializeField] private GameObject[] cubeSpawnZone;
        private ZoneHandler zoneHandler;

        private void Awake()
        {
            zoneHandler = FindObjectOfType<ZoneHandler>();
        }

        private void Start()
        {
            zoneHandler.OnZoneUnlocked += HelperSpawner_OnZoneUnlocked;
        }

        private void HelperSpawner_OnZoneUnlocked(object sender, ZoneHandler.OnZoneUnlockedEventArgs e)
        {
            zoneHandler = e.zoneHandler;
            zoneHandler.OnZoneUnlocked += HelperSpawner_OnZoneUnlocked;
            //FixMe:zoneorder 2 þimdilik dursun!
            if (e.zoneOrder == 2 || e.zoneOrder == 3 || e.zoneOrder == 5 || e.zoneOrder == 7)
            {
                Instantiate(helperPrefab, e.zonePosition, Quaternion.identity, this.transform);

                //FixMe:zoneorder 5 ve 7 olacak, þimdilik 2 ve 3 yapýyorum!
                if (e.zoneOrder == 2)
                {
                    cubeSpawnZone[0].SetActive(true);
                }
                if (e.zoneOrder == 3)
                {
                    cubeSpawnZone[1].SetActive(true);
                }
            }
        }
    }
}
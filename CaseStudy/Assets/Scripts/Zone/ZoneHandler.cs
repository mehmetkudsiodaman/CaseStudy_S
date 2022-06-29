using Player;
using System;
using TMPro;
using UnityEngine;

namespace Zone
{
    public class ZoneHandler : MonoBehaviour
    {
        [SerializeField] private ZoneSO zoneSO;
        [SerializeField] private ZoneSO nextZoneSO;
        [SerializeField] private TMP_Text zoneText = null;

        public event EventHandler<OnZoneUnlockedEventArgs> OnZoneUnlocked;

        public class OnZoneUnlockedEventArgs : EventArgs
        {
            public Vector3 zonePosition;
            public ZoneHandler zoneHandler;
            public int zoneOrder;
        }

        private void Awake()
        {
            zoneText.text = zoneSO.LockAmount.ToString();
            zoneSO.Unlocked = false;
            zoneSO.Zone = this.transform.localPosition;
            zoneSO.ZoneHandler = this;
        }

        private void Start()
        {
            PlayerDetectZones.OnZoneDetected += Zone_OnZoneDetected;
            
            if (zoneSO.ZoneOrder == 1)
            {
                zoneSO.IsActive = true;
            }
            else
            {
                zoneSO.IsActive = false;
            }
        }

        private void Update()
        {
            zoneText.gameObject.SetActive(zoneSO.IsActive);
        }

        private void Zone_OnZoneDetected(object sender, PlayerDetectZones.OnZoneDetectedEventArgs e)
        {
            if (zoneSO.Unlocked)
            {
                return;
            }

            int zoneLockCount = int.Parse(zoneText.text);

            if (zoneLockCount == 0)
            {
                zoneSO.Unlocked = true;
                zoneText.text = "XXX";
                UnlockNextZone();
            }

            //if (zoneText == e.zone.GetChild(1).GetComponentInChildren<TMP_Text>())
            //{
            //    int zoneLockCount = int.Parse(zoneText.text);

            //    if (zoneLockCount == 0)
            //    {
            //        zoneSO.Unlocked = true;

            //        UnlockNextZone();
            //    }
            //}
        }

        private void UnlockNextZone()
        {
            nextZoneSO.IsActive = true;
            OnZoneUnlocked?.Invoke(this, new OnZoneUnlockedEventArgs
            { zonePosition = zoneSO.Zone, zoneHandler = nextZoneSO.ZoneHandler, zoneOrder = nextZoneSO.ZoneOrder});
        }
    }
}
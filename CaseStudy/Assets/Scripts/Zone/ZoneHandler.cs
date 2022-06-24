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
        [SerializeField] private Transform zoneToUnlock;
        [SerializeField] private TMP_Text zoneText = null;

        private DetectZones detectZones;
        private TMP_Text nextZoneText;

        private void Awake()
        {
            detectZones = FindObjectOfType<DetectZones>();
        }

        private void Start()
        {
            detectZones.OnZoneDetected += Zone_OnZoneDetected;
            zoneText.text = zoneSO.LockAmount.ToString();
            zoneSO.Unlocked = false;
            
            if (zoneSO.ZoneOrder != 1)
            {
                zoneSO.IsActive = false;
            }

            nextZoneText = zoneToUnlock.GetChild(1).GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            zoneText.gameObject.SetActive(zoneSO.IsActive);
        }

        private void Zone_OnZoneDetected(object sender, DetectZones.OnZoneDetectedEventArgs e)
        {
            if (zoneSO.Unlocked)
            {
                return;
            }

            if (zoneText == e.zone.GetChild(1).GetComponentInChildren<TMP_Text>())
            {
                int zoneLockCount = int.Parse(zoneText.text);

                if (zoneLockCount == 0)
                {
                    zoneSO.Unlocked = true;
                    UnlockZone();
                    print("unlock zone");
                }
            }
        }

        private void UnlockZone()
        {
            nextZoneSO.IsActive = true;
        }
    }
}
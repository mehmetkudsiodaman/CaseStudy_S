using UnityEngine;

namespace Zone
{
    [CreateAssetMenu(fileName = "Zone", menuName = "Zones")]
    public class ZoneSO : ScriptableObject
    {
        [SerializeField] private int zoneOrder = 1;
        public int ZoneOrder { get => zoneOrder; set => zoneOrder = value; }

        [SerializeField] private bool isActive = false;
        public bool IsActive { get => isActive; set => isActive = value; }

        private bool unlocked = false;
        public bool Unlocked { get => unlocked; set => unlocked = value; }

        private int lockAmount;
        public int LockAmount { get => ZoneOrder * 10; private set { } }

        private Vector3 zone;
        public Vector3 Zone { get => zone; set => zone = value; }

        private ZoneHandler zoneHandler;
        public ZoneHandler ZoneHandler { get => zoneHandler; set => zoneHandler = value; }
    }
}
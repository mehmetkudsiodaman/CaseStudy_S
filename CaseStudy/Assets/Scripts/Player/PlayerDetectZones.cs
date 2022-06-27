using System;
using UnityEngine;

namespace Player
{
    public class PlayerDetectZones: MonoBehaviour
    {
        private Transform zone;

        public event EventHandler<OnZoneDetectedEventArgs> OnZoneDetected;

        public class OnZoneDetectedEventArgs : EventArgs
        {
            public Transform zone;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Zone"))
            {
                zone = other.transform;
                OnZoneDetected?.Invoke(this, new OnZoneDetectedEventArgs { zone = zone });
            }
        }
    }
}
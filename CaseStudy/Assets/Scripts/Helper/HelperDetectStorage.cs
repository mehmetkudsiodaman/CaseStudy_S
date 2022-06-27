using System;
using UnityEngine;

namespace Helper
{
    public class HelperDetectStorage : MonoBehaviour
    {
        private Transform storage;

        public event EventHandler<OnStorageDetectedEventArgs> OnStorageDetected;

        public class OnStorageDetectedEventArgs : EventArgs
        {
            public Transform storage;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Storage"))
            {
                storage = other.transform;
                OnStorageDetected?.Invoke(this, new OnStorageDetectedEventArgs { storage = storage });
            }
        }
    }
}
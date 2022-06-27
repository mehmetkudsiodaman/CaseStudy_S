using System;
using UnityEngine;

namespace Helper
{
    public class HelperDetectCubes : MonoBehaviour
    {
        private Transform cube;

        public event EventHandler<OnCubeDetectedHelperEventArgs> OnCubeDetectedHelper;

        public class OnCubeDetectedHelperEventArgs : EventArgs
        {
            public Transform cube;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cube"))
            {
                cube = other.transform;
                OnCubeDetectedHelper?.Invoke(this, new OnCubeDetectedHelperEventArgs { cube = cube });
            }
        }
    }
}
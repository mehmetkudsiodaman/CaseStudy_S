using System;
using UnityEngine;

namespace Player
{
    public class DetectCubes : MonoBehaviour
    {
        private Transform cube;

        public event EventHandler<OnCubeDetectedEventArgs> OnCubeDetected;

        public class OnCubeDetectedEventArgs : EventArgs
        {
            public Transform cube;
        }

        private void FixedUpdate()
        {
            // layerMask = 1 << 0 => "Default"
            // layerMask = 1 << 7 => "Cubes"
            int layerMask = 1 << 7;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
                out hit, maxDistance: .45f, layerMask))
            {
                //Debug.Log(hit.transform.name + " Hit");
                cube = hit.transform;
                OnCubeDetected?.Invoke(this, new OnCubeDetectedEventArgs { cube = cube });
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * .45f, Color.green);
        }
    }
}
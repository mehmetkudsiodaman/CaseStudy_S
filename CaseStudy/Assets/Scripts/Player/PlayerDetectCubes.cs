using Scripts;
using System;
using UnityEngine;
using Zone;

namespace Player
{
    public class PlayerDetectCubes: MonoBehaviour
    {
        private Transform cube;
        private Cube cubeScript;
        private bool isInStorage = false;
        private StorageArea storageArea;

        public static event EventHandler<OnCubeDetectedEventArgs> OnCubeDetected;

        public class OnCubeDetectedEventArgs : EventArgs
        {
            public Transform cube;
            public bool isInStorage;
        }

        private void Start()
        {
            storageArea = FindObjectOfType<StorageArea>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Storage"))
            {
                isInStorage = true;
                storageArea.CheckCubeCount();
            }
            else
            {
                isInStorage = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cube"))
            {
                
                cube = other.transform;
                cubeScript = cube.GetComponent<Cube>();
                if (cubeScript.isCubeStacked)
                {
                    return;
                }

                OnCubeDetected?.Invoke(this, new OnCubeDetectedEventArgs { cube = cube , isInStorage = isInStorage });
            }
        }

        //private void FixedUpdate()
        //{
        //    // layerMask = 1 << 0 => "Default"
        //    // layerMask = 1 << 7 => "Cubes"
        //    // layerMask = 1 << 8 => "Storage"
        //    int layerMask = 1 << 8;
           
        //    if (Physics.BoxCast(transform.position, Vector3.one, 
        //        transform.TransformDirection(Vector3.forward), Quaternion.identity, 1f , layerMask))
        //    {
        //        //Debug.Log(hit.transform.name + " Hit");
        //        isInStorage = true;
        //    }
        //    else
        //    {
        //        isInStorage = false;
        //    }
        //}

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireCube(transform.position, Vector3.one);
        //}
    }
}
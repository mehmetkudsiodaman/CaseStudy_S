using System;
using UnityEngine;

namespace Helper
{
    public class HelperDetectCubes : MonoBehaviour
    {
        private Transform cube;
        private HelperStack helperStack;
        private HelperDetectStorage helperDetectStorage;
        private CubeSpawnPoint cubeSpawnPoint;

        public event EventHandler OnCubeDetectedHelper;

        private void OnEnable()
        {
            helperStack = GetComponentInParent<HelperStack>();
            helperDetectStorage = GetComponent<HelperDetectStorage>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CubeSpawnPoint"))
            {
                cubeSpawnPoint = other.GetComponent<CubeSpawnPoint>();
            }

            if (other.CompareTag("Cube"))
            {
                if (helperDetectStorage.isInStorage)
                {
                    return;
                }
                else
                {
                    cube = other.transform;
                    helperStack.StackHelper(cube);
                    cubeSpawnPoint.isCubeStacked = true;
                    cubeSpawnPoint.hasCube = false;
                    OnCubeDetectedHelper?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
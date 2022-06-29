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
        private CubeSpawnArea cubeSpawnArea;
        private bool control;

        public event EventHandler<OnCubeDetectedHelperEventArgs> OnCubeDetectedHelper;

        public class OnCubeDetectedHelperEventArgs : EventArgs
        {
            public Transform cube;
        }

        private void OnEnable()
        {
            helperStack = GetComponentInParent<HelperStack>();
            helperDetectStorage = GetComponent<HelperDetectStorage>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cube"))
            {
                if (helperDetectStorage.isInStorage)
                {
                    return;
                }
                else
                {
                    cube = other.transform;
                    cube.parent.GetComponent<CubeSpawnPoint>().hasCube = false;
                    cube.parent.GetComponent<CubeSpawnPoint>().isCubeStacked = true;
                    control = true;
                    helperStack.StackHelper(cube);
                    OnCubeDetectedHelper?.Invoke(this, new OnCubeDetectedHelperEventArgs { cube = cube });
                }
            }
            else
            {
                control = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("CubeSpawnPoint"))
            {
                //Birden fazla küp eksiltiyor
                cubeSpawnPoint = other.GetComponent<CubeSpawnPoint>();
                cubeSpawnArea = cubeSpawnPoint.GetComponentInParent<Transform>()
                    .GetComponentInParent<CubeSpawnArea>();
                if (!cubeSpawnPoint.hasCube && cubeSpawnPoint.isCubeStacked)
                {
                    if (control)
                    {
                        cubeSpawnArea.totalCube--;
                        control = false;
                    }
                }
            }
        }
    }
}
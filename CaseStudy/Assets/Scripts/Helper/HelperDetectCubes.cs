using System;
using UnityEngine;

namespace Helper
{
    public class HelperDetectCubes : MonoBehaviour
    {
        private Transform cube;
        private HelperStack helperStack;
        HelperDetectStorage helperDetectStorage;

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
                    helperStack.StackHelper(cube);
                }
            }
        }
    }
}
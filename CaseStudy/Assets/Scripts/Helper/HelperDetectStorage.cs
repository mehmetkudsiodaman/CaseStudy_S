using System;
using UnityEngine;

namespace Helper
{
    public class HelperDetectStorage : MonoBehaviour
    {
        [HideInInspector] public bool isInStorage;
        private Transform storage;
        private HelperStack helperStack;

        private void OnEnable()
        {
            helperStack = GetComponentInParent<HelperStack>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Storage"))
            {
                storage = other.transform;
                helperStack.DeStackHelper(storage);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Storage"))
            {
                isInStorage = true;
            }
            else
            {
                isInStorage = false;
            }
        }
    }
}
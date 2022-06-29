using Helper;
using System;
using System.Collections;
using UnityEngine;

namespace Zone
{
    public class StorageArea : MonoBehaviour
    {
        public int storageCubesCount = 0;
        private bool storageFilled;

        public static event EventHandler OnStorageFilled;
        public static event EventHandler OnStorageEmpty;

        private void Update()
        {

            if (!storageFilled && storageCubesCount == 40)
            {
                OnStorageFilled?.Invoke(this, EventArgs.Empty);
                storageFilled = true;
            }

            if (storageFilled && storageCubesCount < 40)
            {
                OnStorageEmpty?.Invoke(this, EventArgs.Empty);
                storageFilled = false;
            }
        }

        public void CheckCubeCount()
        {
            storageCubesCount = transform.childCount - 1;
        }
    }
}
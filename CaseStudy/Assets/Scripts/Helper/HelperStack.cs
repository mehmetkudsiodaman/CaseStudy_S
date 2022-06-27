using Scripts;
using System;
using System.Collections;
using UnityEngine;
using Zone;

namespace Helper
{
    public class HelperStack : MonoBehaviour
    {
        [SerializeField] private Transform stackPoint;
        [SerializeField] private float lerpValueToHelper = .25f;
        [SerializeField] private float lerpValueToStorage = .85f;

        private Vector3 targetPosition = default;
        private int totalCubes = 1;
        private int currentCubes = 0;

        private HelperDetectCubes detectCubes;
        private HelperDetectStorage detectZones;
        private StorageArea storageArea;

        public event EventHandler OnCubeStacked;
        public event EventHandler OnStorageFilled;

        private void Awake()
        {
            detectCubes = FindObjectOfType<HelperDetectCubes>();
            detectZones = FindObjectOfType<HelperDetectStorage>();
            storageArea = FindObjectOfType<StorageArea>();
        }

        private void OnEnable()
        {
            detectCubes.OnCubeDetectedHelper += Stack_OnCubeDetected;
            detectZones.OnStorageDetected += Stack_OnStorageDetected;
        }

        private void Stack_OnCubeDetected(object sender, HelperDetectCubes.OnCubeDetectedHelperEventArgs e)
        {
            if (currentCubes >= totalCubes)
            {
                return;
            }

            if (currentCubes < totalCubes)
            {
                Stack(e);
            }
        }

        private void Stack_OnStorageDetected(object sender, HelperDetectStorage.OnStorageDetectedEventArgs e)
        {
            int cubeCount = stackPoint.childCount;
            if (cubeCount > 0)
            {
                DeStack(e, stackPoint.GetChild(cubeCount - 1));
            }
        }

        private void Stack(HelperDetectCubes.OnCubeDetectedHelperEventArgs e)
        {
            e.cube.SetParent(stackPoint);
            currentCubes++;
            e.cube.GetComponent<Cube>().isCubeStacked = true;
            StartCoroutine(LerpCubetoHelper(e, stackPoint.localPosition, lerpValueToHelper));
            OnCubeStacked?.Invoke(this, EventArgs.Empty);
        }

        private void DeStack(HelperDetectStorage.OnStorageDetectedEventArgs e, Transform cube)
        {
            if (storageArea.storageCubesCount >= 40)
            {
                OnStorageFilled?.Invoke(this, EventArgs.Empty);
                return;
            }

            Transform stackZone = e.storage;
            cube.SetParent(stackZone);
            ++storageArea.storageCubesCount;
            cube.GetComponent<BoxCollider>().isTrigger = true;
            cube.GetComponent<Cube>().isCubeStacked = false;
            Destroy(cube.GetComponent<CubeDetectHelper>());
            currentCubes--;

            if (storageArea.storageCubesCount < 11)
            {
                targetPosition = new Vector3(-2.0f, 0.5f * storageArea.storageCubesCount, -2.0f);
            }
            else if (storageArea.storageCubesCount > 10 && storageArea.storageCubesCount < 21)
            {
                targetPosition = new Vector3(-1.0f, 0.5f * (storageArea.storageCubesCount - 10), -2.0f);
            }
            else if (storageArea.storageCubesCount > 20 && storageArea.storageCubesCount < 31)
            {
                targetPosition = new Vector3(0.0f, 0.5f * (storageArea.storageCubesCount - 20), -2.0f);
            }
            else if (storageArea.storageCubesCount > 30 && storageArea.storageCubesCount < 41)
            {
                targetPosition = new Vector3(1.0f, 0.5f * (storageArea.storageCubesCount - 30), -2.0f);
            }

            StartCoroutine(LerpCubetoStorage(e, targetPosition, cube, lerpValueToStorage));
        }

        private IEnumerator LerpCubetoHelper(
            HelperDetectCubes.OnCubeDetectedHelperEventArgs e, Vector3 targetPosition, float duration)
        {
            float time = 0;
            Vector3 startPosition = e.cube.localPosition;

            while (time < duration)
            {
                e.cube.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            e.cube.localPosition = targetPosition;
            e.cube.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private IEnumerator LerpCubetoStorage(
            HelperDetectStorage.OnStorageDetectedEventArgs e,
            Vector3 targetPosition, Transform cube, float duration)
        {
            float time = 0;
            Vector3 startPosition = cube.localPosition;

            while (time < duration)
            {
                cube.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            cube.localPosition = targetPosition;
            cube.localRotation = Quaternion.Euler(0f, 0f, 0f);

            cube.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
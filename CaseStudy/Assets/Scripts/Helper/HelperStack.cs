using Scripts;
using System.Collections;
using UnityEngine;
using Zone;

namespace Helper
{
    public class HelperStack : MonoBehaviour
    {
        [SerializeField] private float lerpValueToHelper = .25f;
        [SerializeField] private float lerpValueToStorage = 1.85f;

        [HideInInspector] public bool hasCube = false;
        [HideInInspector] public Transform storage;
        private Transform stackedCube = null;
        private Transform stackPoint;
        private StorageArea storageArea;
        private Vector3 targetPosition;
        private HelperMovement helperMovement;

        private void OnEnable()
        {
            stackPoint = transform.GetChild(0).GetComponent<Transform>();
            helperMovement = GetComponent<HelperMovement>();
            storageArea = FindObjectOfType<StorageArea>();
            storage = storageArea.GetComponent<Transform>();
        }

        public void StackHelper(Transform cube)
        {
            if (hasCube)
            {
                helperMovement.GoToStorage();
                return;
            }
            else
            {
                cube.SetParent(stackPoint);
                hasCube = true;
                stackedCube = cube;
                cube.GetComponent<Cube>().isCubeStacked = true;
                helperMovement.GoToStorage();
                StartCoroutine(LerpCubetoHelper(cube, stackPoint.localPosition, lerpValueToHelper));
            }
        }

        private IEnumerator LerpCubetoHelper(
            Transform cube, Vector3 targetPosition, float duration)
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
        }

        public void DeStackHelper(Transform storage)
        {
            if (!hasCube)
            {
                //Find a cube
                helperMovement.CheckTargets();
                helperMovement.FindCube();
                return;
            }
            else
            {
                stackedCube.SetParent(storage);
                ++storageArea.storageCubesCount;
                stackedCube.GetComponent<BoxCollider>().isTrigger = true;
                stackedCube.GetComponent<Cube>().isCubeStacked = false;
                Destroy(stackedCube.GetComponent<CubeDetectHelper>());
                helperMovement.CheckTargets();

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

                hasCube = false;
                helperMovement.FindCube();
                StartCoroutine(LerpCubetoStorage(targetPosition, stackedCube, lerpValueToStorage));
            }

            IEnumerator LerpCubetoStorage(
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
}
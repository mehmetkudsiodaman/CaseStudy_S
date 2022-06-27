using Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using Zone;

namespace Player
{
    public class PlayerStack : MonoBehaviour
    {
        //TODO: Stack - destack scripts must be seperated!

        [SerializeField] private Transform stackPoint;
        [SerializeField] private TMP_Text cubeCountText;
        [SerializeField] private float lerpValueToPlayer = .25f;
        [SerializeField] private float lerpValueToZone = .85f;

        private int totalCubes = 10;
        private int currentCubes = 0;
        private Vector3 positionToMoveTo;

        private PlayerDetectCubes detectCubes;
        private PlayerDetectZones detectZones;
        private StorageArea storageArea;
        private Cube cubeScript;

        private void Awake()
        {
            detectCubes = FindObjectOfType<PlayerDetectCubes>();
            detectZones = FindObjectOfType<PlayerDetectZones>();
            storageArea = FindObjectOfType<StorageArea>();
            cubeCountText.text = "0";
        }

        private void Start()
        {
            detectCubes.OnCubeDetected += Stack_OnCubeDetected;
            detectZones.OnZoneDetected += Stack_OnZoneDetected;
        }

        private void Stack_OnCubeDetected(object sender, PlayerDetectCubes.OnCubeDetectedEventArgs e)
        {
            if (currentCubes >= totalCubes * 3)
            {
                return;
            }

            if (currentCubes < totalCubes)
            {
                Stack(e);
            }
            else if (currentCubes >= totalCubes && currentCubes < totalCubes * 2)
            {
                StackLeft(e);
            }
            else if (currentCubes >= totalCubes * 2 && currentCubes < totalCubes * 3)
            {
                StackRight(e);
            }
        }

        private void Stack_OnZoneDetected(object sender, PlayerDetectZones.OnZoneDetectedEventArgs e)
        {
            int cubeCount = stackPoint.childCount;
            if (cubeCount > 0)
            {
                if (e.zone.GetChild(1).GetComponentInChildren<TMP_Text>() != null)
                {
                    TMP_Text zoneText = e.zone.GetChild(1).GetComponentInChildren<TMP_Text>();
                    int zoneLockCount = int.Parse(zoneText.text);
                    if (zoneLockCount > 0)
                    {
                        DeStack(e, stackPoint.GetChild(cubeCount - 1), zoneText, zoneLockCount);
                    }
                }
            }
        }

        private void Stack(PlayerDetectCubes.OnCubeDetectedEventArgs e)
        {
            e.cube.SetParent(stackPoint);
            currentCubes++;

            if (e.isInStorage)
            {
                storageArea.storageCubesCount--;
            }
            cubeCountText.text = currentCubes.ToString();
            positionToMoveTo = new Vector3(0f, 0.5f * currentCubes, 0f);
            e.cube.GetComponent<Cube>().isCubeStacked = true;
            StartCoroutine(LerpCubetoPlayer(e, positionToMoveTo, lerpValueToPlayer));
        }

        private void StackLeft(PlayerDetectCubes.OnCubeDetectedEventArgs e)
        {
            e.cube.SetParent(stackPoint);
            currentCubes++;
            if (e.isInStorage)
            {
                storageArea.storageCubesCount--;
            }
            cubeCountText.text = currentCubes.ToString();
            positionToMoveTo = new Vector3(-1f, 0.5f * (currentCubes - totalCubes), 0f);
            e.cube.GetComponent<Cube>().isCubeStacked = true;
            StartCoroutine(LerpCubetoPlayer(e, positionToMoveTo, lerpValueToPlayer));
        }

        private void StackRight(PlayerDetectCubes.OnCubeDetectedEventArgs e)
        {
            e.cube.SetParent(stackPoint);
            currentCubes++;
            if (e.isInStorage)
            {
                storageArea.storageCubesCount--;
            }
            cubeCountText.text = currentCubes.ToString();
            positionToMoveTo = new Vector3(1f, 0.5f * (currentCubes - (totalCubes * 2)), 0f);
            e.cube.GetComponent<Cube>().isCubeStacked = true;
            StartCoroutine(LerpCubetoPlayer(e, positionToMoveTo, lerpValueToPlayer));
        }

        private void DeStack(PlayerDetectZones.OnZoneDetectedEventArgs e, Transform cube, TMP_Text zoneText, int lockCount)
        {
            Transform stackZone = e.zone.GetChild(0);
            cube.SetParent(stackZone);
            cube.GetComponent<BoxCollider>().isTrigger = true;
            currentCubes--;
            lockCount--;
            zoneText.text = lockCount.ToString();
            cubeCountText.text = currentCubes.ToString();
            positionToMoveTo = stackZone.localPosition;
            StartCoroutine(LerpCubetoZone(e, positionToMoveTo, cube, lerpValueToZone));
        }

        private IEnumerator LerpCubetoPlayer(
            PlayerDetectCubes.OnCubeDetectedEventArgs e, Vector3 targetPosition, float duration)
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

        private IEnumerator LerpCubetoZone(
            PlayerDetectZones.OnZoneDetectedEventArgs e, Vector3 targetPosition, Transform cube, float duration)
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
            cube.gameObject.SetActive(false);
        }
    }
}
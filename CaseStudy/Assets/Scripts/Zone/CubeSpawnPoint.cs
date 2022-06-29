using Helper;
using Player;
using System;
using UnityEngine;

public class CubeSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private CubeSpawnArea cubeSpawnArea;
    [HideInInspector] public bool hasCube = false;
    [HideInInspector] public bool isCubeStacked = false;
    private Vector3 spawnPosition;
    private HelperDetectCubes helperDetectCube;

    private void OnEnable()
    {
        //cubeSpawnArea = GetComponentInParent<Transform>().GetComponentInParent<CubeSpawnArea>();
        PlayerDetectCubes.OnCubeDetected += CubeSpawnPoint_OnCubeDetected;
        HelperSpawner.OnHelperSpawned += CubeSpawnPoint_OnHelperSpawned;
        hasCube = false;
        isCubeStacked = false;

        spawnPosition = new Vector3(0f, .25f, 0f);
    }

    private void CubeSpawnPoint_OnHelperSpawned(object sender, HelperSpawner.OnHelperSpawnedEventArgs e)
    {
        helperDetectCube = e.helperPrefab.transform.GetChild(1).GetComponent<HelperDetectCubes>();
        helperDetectCube.OnCubeDetectedHelper += CubeSpawnPoint_OnCubeDetectedHelper;
    }

    private void CubeSpawnPoint_OnCubeDetectedHelper(object sender, EventArgs e)
    {
        if (hasCube)
        {
            hasCube = false;
            isCubeStacked = true;
            cubeSpawnArea.totalCube--;
            Invoke(nameof(SpawnCube), 3f);
        }
    }

    private void CubeSpawnPoint_OnCubeDetected(object sender, PlayerDetectCubes.OnCubeDetectedEventArgs e)
    {
        //Birden fazla küp eksiltebilir.
        if (!e.isInStorage)
        {
            if (hasCube)
            {
                hasCube = false;
                isCubeStacked = true;
                cubeSpawnArea.totalCube--;
                Invoke(nameof(SpawnCube), 3f);
            }
        }
    }

    public void SpawnCube()
    {
        if (transform.childCount == 0)
        {
            var spawnedCube = Instantiate(cube, spawnPosition, Quaternion.identity, transform);
            spawnedCube.transform.localPosition = spawnPosition;
            isCubeStacked = false;
            hasCube = true;
            cubeSpawnArea.totalCube++;
        }
    }

    ////    Bu çok saðlýklý çalýþmaz!
    ////    Her helper spawn olduðunda ona abone olmak lazým
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Helper"))
    //    {
    //        helperDetectCube = other.GetComponent<HelperDetectCubes>();
    //        helperDetectCube.OnCubeDetectedHelper += CubeSpawnPoint_OnCubeDetectedHelper;
    //    }
    //}
}
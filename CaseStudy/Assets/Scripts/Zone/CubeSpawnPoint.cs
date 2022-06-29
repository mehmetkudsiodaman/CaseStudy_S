using Helper;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private CubeSpawnArea cubeSpawnArea;
    //[HideInInspector]
    public bool hasCube = false;
    [HideInInspector] public bool isCubeStacked = false;
    private Vector3 spawnPosition;
    private List<HelperDetectCubes> helperDetectCube = new List<HelperDetectCubes>();

    private void OnEnable()
    {
        //cubeSpawnArea = GetComponentInParent<Transform>().GetComponentInParent<CubeSpawnArea>();
        PlayerDetectCubes.OnCubeDetected += CubeSpawnPoint_OnCubeDetected;
        //HelperSpawner.OnHelperSpawned += CubeSpawnPoint_OnHelperSpawned;
        hasCube = false;
        isCubeStacked = false;

        spawnPosition = new Vector3(0f, .25f, 0f);
    }

    //private void CubeSpawnPoint_OnHelperSpawned(object sender, HelperSpawner.OnHelperSpawnedEventArgs e)
    //{
    //    helperDetectCube.Add(e.helperPrefab.transform.GetChild(1).GetComponent<HelperDetectCubes>());
    //    for (int i = 0; i < helperDetectCube.Count; i++)
    //    {
    //        helperDetectCube[i].OnCubeDetectedHelper -= CubeSpawnPoint_OnCubeDetectedHelper;
    //        helperDetectCube[i].OnCubeDetectedHelper += CubeSpawnPoint_OnCubeDetectedHelper;
    //    }
    //}

    ////Saðlýklý çalýþmýyor!
    //private void CubeSpawnPoint_OnCubeDetectedHelper(object sender, HelperDetectCubes.OnCubeDetectedHelperEventArgs e)
    //{
    //    if (e.cube.parent.GetComponent<CubeSpawnPoint>() == this)
    //    {
    //        hasCube = false;
    //        isCubeStacked = true;
    //        //cubeSpawnArea.totalCube--;
    //        Invoke(nameof(SpawnCube), 3f);
    //    }
    //}

    private void CubeSpawnPoint_OnCubeDetected(object sender, PlayerDetectCubes.OnCubeDetectedEventArgs e)
    {
        if (!e.isInStorage)
        {
            if (e.cube.parent.GetComponent<CubeSpawnPoint>() == this)
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
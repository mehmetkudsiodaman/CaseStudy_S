using System.Collections;
using UnityEngine;

public class CubeSpawnArea : MonoBehaviour
{
    [SerializeField] private CubeSpawnPoint[] cubeSpawnPoint;
    //[HideInInspector] 
    public int totalCube = 0;
    private IEnumerator spawnerCoroutine;

    private void OnEnable()
    {
        spawnerCoroutine = SpawnRepeater(3.0f);
        StartCoroutine(spawnerCoroutine);
    }

    private IEnumerator SpawnRepeater(float waitTime)
    {
        while (true)
        {
            if (totalCube < 10)
            {
                int rand = Random.Range(0, cubeSpawnPoint.Length);

                if (!cubeSpawnPoint[rand].hasCube)
                {
                    cubeSpawnPoint[rand].SpawnCube();
                }
                else
                {
                    //FixMe:hepsinde küp spawn oduðunda sonsuz döngü olacak!
                    //true yerine totalCube < 11
                    //FixMe:küplerin hepsi biranda spawn oluyor!
                    while (cubeSpawnPoint[rand].hasCube)
                    {
                        rand = Random.Range(0, cubeSpawnPoint.Length);
                    }
                    cubeSpawnPoint[rand].SpawnCube();
                }
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
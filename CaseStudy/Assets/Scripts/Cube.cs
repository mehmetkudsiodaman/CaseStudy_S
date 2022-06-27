using Player;
using UnityEngine;

namespace Scripts
{
    public class Cube : MonoBehaviour
    {
        [HideInInspector] public bool isCubeStacked;

        private PlayerDetectCubes playerDetectCubes;

        private void Awake()
        {
            playerDetectCubes = FindObjectOfType<PlayerDetectCubes>();
        }

        private void Start()
        {
            playerDetectCubes.OnCubeDetected += Cube_OnCubeDetected;
        }

        private void Cube_OnCubeDetected(object sender, PlayerDetectCubes.OnCubeDetectedEventArgs e)
        {
            Destroy(e.cube.GetComponent<CubeDetectHelper>());
        }
    }
}
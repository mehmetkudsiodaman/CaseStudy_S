using TMPro;
using UnityEngine;

namespace Player
{
    public class StackCubes : MonoBehaviour
    {
        [SerializeField] private Transform stackPoint;
        [SerializeField] private TMP_Text cubeCountText;

        private DetectCubes detectCubes;
        private int totalCubes = 10;
        private int currentCubes = 0;

        private void Awake()
        {
            detectCubes = FindObjectOfType<DetectCubes>();
            cubeCountText.text = "0";
        }

        private void Start()
        {
            detectCubes.OnCubeDetected += Stack_OnCubeDetected;
        }

        private void Stack_OnCubeDetected(object sender, DetectCubes.OnCubeDetectedEventArgs e)
        {
            if (currentCubes != totalCubes)
            {
                Stack(e);
            }
        }

        private void Stack(DetectCubes.OnCubeDetectedEventArgs e)
        {
            e.cube.SetParent(stackPoint);
            currentCubes++;
            cubeCountText.text = currentCubes.ToString();
            e.cube.localPosition = new Vector3(0f, 0.5f * currentCubes, 0f);
            e.cube.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
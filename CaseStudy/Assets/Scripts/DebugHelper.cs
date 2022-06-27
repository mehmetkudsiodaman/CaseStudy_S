using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class DebugHelper: MonoBehaviour
    {
        [SerializeField] GameObject helperPrefab;

        public void TimeScaleX1()
        {
            Time.timeScale = 1;
        }

        public void TimeScaleX4()
        {
            Time.timeScale = 4;
        }

        public void TimeScaleX8()
        {
            Time.timeScale = 8;
        }

        public void SpawnHelper()
        {
            Instantiate(helperPrefab, helperPrefab.transform.position, Quaternion.identity);
        }

        public void Restart()
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
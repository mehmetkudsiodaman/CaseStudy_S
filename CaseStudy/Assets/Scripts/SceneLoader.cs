using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] Image image;
        AsyncOperation asyncLoad;

        private void Awake()
        {
            StartCoroutine(LoadYourAsyncScene());
        }

        private void Update()
        {
            image.fillAmount = asyncLoad.progress;

            if (asyncLoad.isDone)
            {
                SceneManager.UnloadSceneAsync(0);
            }
        }

        IEnumerator LoadYourAsyncScene()
        {
            asyncLoad = SceneManager.LoadSceneAsync("TestScene");

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
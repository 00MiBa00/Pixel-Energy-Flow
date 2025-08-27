using System.Collections;
using Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Scenes
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        private void OnEnable()
        {   
            Initialize();
            Subscribe();
            OnSceneEnable();
        }

        private void Start()
        {
            OnSceneStart();
        }

        private void OnDisable()
        {   
            Unsubscribe();
            OnSceneDisable();
        }

        protected abstract void OnSceneEnable();
        protected abstract void OnSceneStart();
        protected abstract void OnSceneDisable();
        protected abstract void Initialize();
        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        protected void LoadScene(SceneType type)
        {
            StartCoroutine(DelayLoadScene(type.ToString()));
        }

        private IEnumerator DelayLoadScene(string sceneName)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
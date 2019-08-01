using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace RedPanda.Toolbox.Effects
{
    public class ChangeScene : MonoBehaviour
    {
        public RawImage fadeOutUIImage;
        public float fadeSpeed = 0.8f;
        public enum FadeDirection
        {
            In, // Alpha = 1
            Out // Alpha = 0
        }

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        public delegate void SceneLoadedAction();
        public static event SceneLoadedAction OnSceneLoadComplete;

        public delegate void SceneLoadStartedAction();
        public static event SceneLoadStartedAction OnSceneLoadStarted;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => StartCoroutine(Fade(FadeDirection.Out));

        private IEnumerator Fade(FadeDirection fadeDirection)
        {
            float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
            float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
            if (fadeDirection == FadeDirection.Out)
            {
                while (alpha >= fadeEndValue)
                {
                    SetColorImage(ref alpha, fadeDirection);
                    yield return null;
                }
                fadeOutUIImage.enabled = false;
                OnSceneLoadComplete?.Invoke();
            }
            else
            {
                OnSceneLoadStarted?.Invoke();
                fadeOutUIImage.enabled = true;
                while (alpha <= fadeEndValue)
                {
                    SetColorImage(ref alpha, fadeDirection);
                    yield return null;
                }
            }
        }

        private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
        {
            fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
            alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
        }

        private IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
        {
            yield return Fade(fadeDirection);
            SceneManager.LoadScene(sceneToLoad);
        }

        public void DoTransition(FadeDirection fadeDirection, string sceneToLoad) => StartCoroutine(FadeAndLoadScene(fadeDirection, sceneToLoad));
    }
}
using System.Collections;
using UnityEngine;

public class GeneralTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup _loadCanvas;
    [SerializeField] private float transitionDuration = 0.7f;

    public void MakeTransition()
    {
        _loadCanvas.alpha = 0f;
        StartCoroutine(_fadeCanvasGroup(_loadCanvas, 1f, transitionDuration));
    }

    public void SceneChangend()
    {
        _loadCanvas.alpha = 1f;
        StartCoroutine(_fadeCanvasGroup(_loadCanvas, 0f, transitionDuration));
    }

    private IEnumerator _fadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // Ignores time scale, similar to setIgnoreTimeScale(true)
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // Ensure final value is set correctly
    }
}

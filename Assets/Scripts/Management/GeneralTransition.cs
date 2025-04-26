using System;
using System.Collections;
using UnityEngine;

public class GeneralTransition : MonoBehaviour // sigleton yap
{
    [SerializeField] private CanvasGroup _loadCanvas;
    [SerializeField] private float transitionDuration = 0.7f;

    private bool _isOnTransition;

    public void EnterTransition(Action onTransitionOver){
        if(_isOnTransition) return;
        StartCoroutine(_processTransition(onTransitionOver));
    }

    private IEnumerator _processTransition(Action _onTransitionOver){  
        _loadCanvas.alpha = 0f;
        _isOnTransition = true;
        yield return StartCoroutine(_fadeCanvasGroup(_loadCanvas, 1f, transitionDuration));
        _onTransitionOver?.Invoke();
        yield return StartCoroutine(_fadeCanvasGroup(_loadCanvas, 0f, transitionDuration));

        
        _isOnTransition = false;
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup startPanel;
    public CanvasGroup mainPanel;
    public RectTransform mainRect;

    [Header("Settings")]
    public float duration = 0.8f;
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public void LaunchApp()
    {
        StartCoroutine(ExecuteTransition());
    }

    IEnumerator ExecuteTransition()
    {
        float elapsed = 0;
        mainPanel.gameObject.SetActive(true);
        mainPanel.alpha = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float curveValue = transitionCurve.Evaluate(t);

            startPanel.alpha = 1 - curveValue;
            startPanel.transform.localScale = Vector3.one + (Vector3.one * (curveValue * 0.2f));

            mainPanel.alpha = curveValue;
            float scaleValue = Mathf.Lerp(0.8f, 1.0f, curveValue);
            mainRect.localScale = new Vector3(scaleValue, scaleValue, 1);

            yield return null;
        }
        startPanel.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        StartCoroutine(ExecuteBackTransition());
    }

    IEnumerator ExecuteBackTransition()
    {
        float elapsed = 0;
        startPanel.gameObject.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float curveValue = transitionCurve.Evaluate(t);

            mainPanel.alpha = 1 - curveValue;
            float scaleValue = Mathf.Lerp(1.0f, 0.8f, curveValue);
            mainRect.localScale = new Vector3(scaleValue, scaleValue, 1);

            startPanel.alpha = curveValue;
            startPanel.transform.localScale = Vector3.one + (Vector3.one * (0.2f * (1 - curveValue)));

            yield return null;
        }
        mainPanel.gameObject.SetActive(false);
    }

    [Header("Results Settings")]
    public CanvasGroup resultOverlayGroup;
    public RectTransform resultCardRect;

    public void ShowResults()
    {
        StopAllCoroutines();
        StartCoroutine(PopInResults());
    }

    public void HideResults()
    {
        StartCoroutine(PopOutResults());
    }

    IEnumerator PopInResults()
    {
        resultOverlayGroup.gameObject.SetActive(true);
        resultOverlayGroup.alpha = 0;

        resultCardRect.localScale = Vector3.one;

        float elapsed = 0;
        float fadeDuration = 0.4f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            resultOverlayGroup.alpha = Mathf.SmoothStep(0, 1, t);
            yield return null;
        }
        resultOverlayGroup.alpha = 1;
    }

    IEnumerator PopOutResults()
    {
        float elapsed = 0;
        float fadeDuration = 0.3f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            resultOverlayGroup.alpha = 1 - Mathf.SmoothStep(0, 1, t);
            yield return null;
        }

        resultOverlayGroup.gameObject.SetActive(false);
    }
}
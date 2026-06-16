using System.Collections;
using UnityEngine;

public class ModernTransition : MonoBehaviour
{
    [Header("UI Panels Configuration")]
    public RectTransform resultsPanel;

    [Header("Animation Settings")]
    public float duration = 0.4f;

    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    private Coroutine activeCoroutine;

    void Start()
    {
        if (resultsPanel != null)
        {
            visiblePosition = Vector2.zero;

            hiddenPosition = new Vector2(Screen.width + resultsPanel.rect.width, 0);

            resultsPanel.anchoredPosition = hiddenPosition;
        }
    }

    public void ShowResults()
    {
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(SlidePanel(resultsPanel, visiblePosition));
    }

    public void HideResults()
    {
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(SlidePanel(resultsPanel, hiddenPosition));
    }

    private IEnumerator SlidePanel(RectTransform panel, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startPosition = panel.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            panel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        panel.anchoredPosition = targetPosition;
    }
}
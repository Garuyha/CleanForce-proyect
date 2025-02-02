using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f; // 1.2 significa un aumento del 20%
    public float animationSpeed = 0.2f;

    private Vector3 originalScale;
    private Vector3 targetScale;


    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * scaleFactor; // Calcula el tamaño aumentado automáticamente
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        StopAllCoroutines();
        StartCoroutine(ScaleTo(targetScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / animationSpeed);
            elapsedTime += Time.unscaledDeltaTime; // Usamos unscaledDeltaTime para que funcione con timeScale = 0
            yield return null;
        }

        transform.localScale = targetScale;
    }
}


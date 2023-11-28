using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Parameters")]
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.2f;

    private Vector3 originalPosition;

    private RectTransform rectTransform;

    public static CameraShake Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        originalPosition = transform.localPosition;
    }

    public IEnumerator DoShake(float intensity)
    {
        float elapsedTime = 0f;
        shakeIntensity = intensity;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            rectTransform.localPosition = originalPosition + randomOffset;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}

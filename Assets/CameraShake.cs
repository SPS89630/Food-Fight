using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Parameters")]
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.2f;

    private Vector3 originalPosition;

    private RectTransform rectTransform;

    bool isShaking = false;

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

    public IEnumerator DoShake()
    {
        if(!isShaking)
        {
            isShaking = true;
            rectTransform.DOPunchAnchorPos(new Vector2(shakeIntensity,shakeIntensity), shakeIntensity, 90, 80, false);
            yield return new WaitForSeconds(shakeDuration);
            isShaking = false;
        }
        yield return null;
    }
}

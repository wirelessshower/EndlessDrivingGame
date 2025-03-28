using UnityEngine;

public class RandomizeObject : MonoBehaviour {
    [SerializeField] Vector3 localRotationMin = Vector3.zero;
    [SerializeField] Vector3 localRotationMax = Vector3.zero;
    [SerializeField] float localScaleMultiplayerMin = 0.8f;
    [SerializeField] float localScaleMultiplayerMax = 1.5f;

    Vector3 localScaleOriginal;

    void Awake()
    {
        localScaleOriginal = transform.localScale;
    }
    void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(Random.Range(localRotationMin.x, localRotationMax.x), 
        Random.Range(localRotationMin.y, localRotationMax.y), 
        Random.Range(localRotationMin.z, localRotationMax.z));

        transform.localScale = localScaleOriginal * Random.Range(localScaleMultiplayerMin, localScaleMultiplayerMax);
    }
}

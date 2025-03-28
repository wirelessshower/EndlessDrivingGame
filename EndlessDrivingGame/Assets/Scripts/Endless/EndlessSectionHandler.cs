
using UnityEngine;

public class EndlessSectionHandler : MonoBehaviour {
    
    Transform playerCarTransform;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = transform.position.z - playerCarTransform.position.z;

        float lerpPercentage = 1.0f - ((distanceToPlayer - 100) / 150.0f);

        lerpPercentage = Mathf.Clamp01(lerpPercentage);

        transform.position = Vector3.Lerp(new Vector3(transform.position.x, -10, transform.position.z),
         new Vector3(transform.position.x, 0, transform.position.z), lerpPercentage);
         
    }

}
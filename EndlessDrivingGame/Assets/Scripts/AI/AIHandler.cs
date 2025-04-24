using System.Collections;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    [SerializeField] private CarHandler carHandler;
    [SerializeField] private LayerMask otherCarsLayerMask;
    [SerializeField] private MeshCollider meshCollider;

    [Header("SFX")]
    [SerializeField] private AudioSource honkHornAS;

    private const float ObstacleCheckInterval = 0.2f;
    private const float HornDistanceThreshold = 15f;
    private const float BoxCastHalfSize = 0.25f;
    private const float BoxCastDistance = 3f;

    private bool isCarAhead = false;
    private float carAheadDistance = 0f;
    private int drivingInLane = 0;
    private WaitForSeconds wait;
    private RaycastHit[] raycastHits = new RaycastHit[1];

    private void Awake()
    {
        if (CompareTag("Player"))
        {
            Destroy(this);
            return;
        }
        wait = new WaitForSeconds(ObstacleCheckInterval);
    }

    private void Start()
    {
        StartCoroutine(UpdateLessOftenCO());
    }

    private void Update()
    {
        HandleSteeringAndAcceleration();
        HandleHorn();
    }

    private void HandleSteeringAndAcceleration()
    {
        float accelerationInput = 1.0f;
        float steerInput = 0f;

        if (isCarAhead)
        {
            accelerationInput = -1f;
        }

        float desiredPositionX = Utils.CarLanes[drivingInLane];
        float difference = desiredPositionX - transform.position.x;

        if (Mathf.Abs(difference) > 0.05f)
        {
            steerInput = 1.0f * difference;
        }

        steerInput = Mathf.Clamp(steerInput, -1f, 1f);
        carHandler.SetInput(new Vector2(steerInput, accelerationInput));
    }

    private void HandleHorn()
    {
        if (isCarAhead && carAheadDistance < HornDistanceThreshold && !honkHornAS.isPlaying && !raycastHits[0].collider.gameObject.CompareTag("Coin"))
        {
            honkHornAS.pitch = Random.Range(0.5f, 1.1f);
            honkHornAS.Play();
        }
    }

    private IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            isCarAhead = CheckIfOtherCarsIsAhead();
            yield return wait;
        }
    }

    private bool CheckIfOtherCarsIsAhead()
    {
        meshCollider.enabled = false;

        int numberOfHits = Physics.BoxCastNonAlloc(transform.position, Vector3.one * BoxCastHalfSize, transform.forward, raycastHits, Quaternion.identity, BoxCastDistance, otherCarsLayerMask);

        meshCollider.enabled = true;

        if (numberOfHits > 0)
        {
            carAheadDistance = (transform.position - raycastHits[0].point).magnitude;
            return true;
        }
        
            
        carAheadDistance = float.MaxValue; // Reset distance when no car is ahead
        return false;
    }

    // Events
    private void OnEnable()
    {
        carHandler.SetMaxSpeed(Random.Range(2, 4));
        drivingInLane = Random.Range(0, Utils.CarLanes.Length);
    }
}

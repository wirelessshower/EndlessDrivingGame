using System.Collections;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    [SerializeField] CarHandler carHandler;
    [SerializeField] LayerMask otherCarsLayerMask;
    [SerializeField] MeshCollider meshCollider;

    RaycastHit[] raycastHits = new RaycastHit[1];
    bool isCarAhad = false;

    //lains
    int drivingInLane = 0;

    //Timing
    WaitForSeconds wait = new WaitForSeconds(0.2f);

    void Awake()
    {
        if(CompareTag("Player")){
            Destroy(this);
            return;
        }
    }

    void Update()
    {
        float accelarationInput = 1.0f;
        float steerInput = 0;

        if(isCarAhad)
            accelarationInput = -1;

        float desiredPositionX = Utils.CarLanes[drivingInLane];

        float difference = desiredPositionX - transform.position.x;

        if(Mathf.Abs(difference) > 0.05f)
            steerInput = 1.0f * difference;
        
        steerInput = Mathf.Clamp(steerInput, -1f, 1f);

        carHandler.SetInput(new Vector2(steerInput, accelarationInput));
    }

    IEnumerator UpdateLessoftenCO(){
        while(true){
            isCarAhad = CheckIfOtherCarsIsAhad();
            yield return wait;
        }
    }

    bool CheckIfOtherCarsIsAhad()
    {
        meshCollider.enabled = false;

        int numberOfHits = Physics.BoxCastNonAlloc(transform.position, Vector3.one * 0.25f, transform.forward, raycastHits, Quaternion.identity, 2, otherCarsLayerMask);

        meshCollider.enabled = true;

        if(numberOfHits > 0)
            return true;

        return false;
    }

    //events
    void OnEnable()
    {
        carHandler.SetMaxSpeed(Random.Range(2,4));

        drivingInLane = Random.Range(0, Utils.CarLanes.Length);
    }
}

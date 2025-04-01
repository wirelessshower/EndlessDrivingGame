using UnityEngine;

public class ExplodeHandler : MonoBehaviour {
    [SerializeField] GameObject originalObjec;
    [SerializeField] GameObject model;

    Rigidbody[] rigidbodies;

    void Awake()
    {
        rigidbodies  = model.GetComponentsInChildren<Rigidbody>(true);
    }
    

    public void Explode(Vector3 externalForce){
        originalObjec.SetActive(false);

        foreach(Rigidbody rb in rigidbodies){
            rb.transform.parent = null;

            rb.GetComponent<MeshCollider>().enabled = true;

            rb.gameObject.SetActive(true);
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.AddForce(Vector3.up * 200 + externalForce, ForceMode.Force);
            rb.AddTorque(Random.insideUnitSphere * 0.5f, ForceMode.Impulse);

            rb.gameObject.tag = "CarPart";
        }
    }
}
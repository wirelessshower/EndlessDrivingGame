using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] carsPrefubs;

    private int currentCarIndex;
    

    void Start()
    {   
        currentCarIndex = PlayerPrefs.GetInt("CurrentCar");
        Instantiate(carsPrefubs[currentCarIndex], Vector3.zero, Quaternion.identity);
        
    }
}

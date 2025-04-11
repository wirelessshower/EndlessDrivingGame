using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] carsPrefubs;

    private int currentCarIndex;
    

    void Start()
    {
        for(int i = 0; i < carsPrefubs.Length; i++){
            carsPrefubs[i].SetActive(false);
        }

        currentCarIndex = PlayerPrefs.GetInt("CurrentCar", 0);
        carsPrefubs[currentCarIndex].SetActive(true);
    }
}

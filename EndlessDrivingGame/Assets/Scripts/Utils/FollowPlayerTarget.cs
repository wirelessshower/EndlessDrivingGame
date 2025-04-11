using Unity.Cinemachine;
using UnityEngine;

public class FollowPlayerTarget : MonoBehaviour {

    private CinemachineCamera vcam;
    public string playerTag = "Player"; // Тег машины (можно изменить)

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();
        
        // Найти машину по тегу (если у неё есть тег "Player")
        GameObject playerCar = GameObject.FindGameObjectWithTag(playerTag);
        
        if (playerCar != null)
        {
            vcam.Follow = playerCar.transform;
            vcam.LookAt = playerCar.transform;
        }
        else
        {
            Debug.LogError("Машина не найдена! Проверь тег или метод спавна.");
        }
    }

}
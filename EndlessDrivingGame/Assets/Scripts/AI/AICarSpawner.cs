using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class AICarSpawner : MonoBehaviour {
    [SerializeField] GameObject[] carAIPrefabs;
    [SerializeField] LayerMask otherCarsLayerMask;

    GameObject[] carAIPool = new GameObject[20];

    Transform playerCarTransform;

    WaitForSeconds wait = new WaitForSeconds(0.5f);

    float timeLastTimeSpawned = 0;

    Collider[] overlappedCheckCollider = new Collider[1];

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        for(int i = 0; i < carAIPool.Length; i++){
            carAIPool[i] = Instantiate(carAIPrefabs[prefabIndex]);
            carAIPool[i].SetActive(false);

            prefabIndex++;

            if(prefabIndex > carAIPrefabs.Length - 1)
                prefabIndex = 0;
        }   

        StartCoroutine(UpdateLessOftenCO()); 
    }


    IEnumerator UpdateLessOftenCO(){
        while(true){
            CleanUpCarsBeyondView();
            SpawnNewCars();
            yield return wait;
        }
    }

    void SpawnNewCars(){
        if(Time.time - timeLastTimeSpawned < 2)
            return;

        GameObject carToSpawn = null;

        foreach(GameObject aiCar in carAIPool){
            if(aiCar.activeInHierarchy)
                continue;

            carToSpawn = aiCar;
            break;
        }

        if(carToSpawn == null)
            return;

        Vector3 spawnPoint = new Vector3(0,0,playerCarTransform.transform.position.z + 100);

        if(Physics.OverlapBoxNonAlloc(spawnPoint, Vector3.one *2, overlappedCheckCollider, Quaternion.identity, otherCarsLayerMask) > 0)
            return;
        

        carToSpawn.transform.position = spawnPoint;
        carToSpawn.SetActive(true);

        timeLastTimeSpawned = Time.time;
    }

    void CleanUpCarsBeyondView(){
        foreach(GameObject aiCar in carAIPool){
            if(!aiCar.activeInHierarchy)
                continue;

            if(aiCar.transform.position.z - playerCarTransform.position.z > 200)
                aiCar.SetActive(false);
            
            if(aiCar.transform.position.z - playerCarTransform.position.z < -50)
                aiCar.SetActive(false);
            
        }
    }
    
}
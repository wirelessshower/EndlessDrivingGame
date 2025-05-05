using UnityEngine;

public class PlatformCoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    [SerializeField] private int minCoinsToSpawn;
    [SerializeField] private int maxCoinsToSpawn;

    private void OnEnable()
    {
        SpawnCoins();
    }

    void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins(){
        foreach(Transform child in transform){
            if(child.gameObject.CompareTag("Coin"))
                child.gameObject.SetActive(false);
        }

        int conisToActivate = Random.Range(minCoinsToSpawn, maxCoinsToSpawn + 1);
        int activateCount = 0;
        int attempts = 0;
        int maxAttempts = 20;

        while(activateCount < conisToActivate && attempts < maxAttempts){
            attempts++;
            
            int RandomIndex = Random.Range(0, transform.childCount);
            Transform randomChild = transform.GetChild(RandomIndex);

            if(randomChild.gameObject.CompareTag("Coin") && !randomChild.gameObject.activeSelf){
                randomChild.gameObject.SetActive(true);
                activateCount++;
            }
        
        }
    }
}

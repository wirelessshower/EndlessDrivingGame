using System.Collections;
using UnityEngine;



public class EndlessLevelHandler : MonoBehaviour
{
   [SerializeField] GameObject[] sectionsPrefabs;
   
   GameObject[] sectionsPool = new GameObject[10];
   
   GameObject[] sections = new GameObject[5];

   private Transform playerCarTransform;
   
   WaitForSeconds waitFor100ms = new  WaitForSeconds(0.1f);
   
   const float SectionLenght = 26f;

   private void Start()
   {
      playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

      int prefabIndex = 0;

      for (int i = 0; i < sectionsPool.Length; i++)
      {
         sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
         sectionsPool[i].SetActive(false);
         prefabIndex++;

         if(prefabIndex > sectionsPrefabs.Length - 1)
            prefabIndex=0;
      }

      for (int i = 0; i < sections.Length; i++)
      {
         GameObject randomSection = GetRandomSectionFromPool();
         
         randomSection.transform.position = new Vector3(sectionsPool[i].transform.position.x, -10, i * SectionLenght);
         randomSection.SetActive(true);
         
         sections[i] = randomSection;
      }

      StartCoroutine(UpdateLessOfTenCO());
   }

   IEnumerator UpdateLessOfTenCO()
   {
      while (true)
      {
         UpdateSectionsPositions();
         yield return waitFor100ms;
      }
   }

   void UpdateSectionsPositions()
   {
      for (int i = 0; i < sections.Length; i++)
      {
         if (sections[i].transform.position.z - playerCarTransform.position.z < -SectionLenght)
         {
            Vector3 lastSectionPosition = sections[i].transform.position;
            sections[i].SetActive(false);
            
            sections[i] = GetRandomSectionFromPool();
            
            sections[i].transform.position = new Vector3(lastSectionPosition.x, 0, lastSectionPosition.z + SectionLenght * sections.Length);
            sections[i].SetActive(true);
         }
      }
   }
   
   GameObject GetRandomSectionFromPool()
   {
      int randomIndex = Random.Range(0, sections.Length);

      bool isNewSectionFound = false;

      while (!isNewSectionFound)
      {
         if(!sectionsPool[randomIndex].activeInHierarchy)
            isNewSectionFound = true;
         else
         {
            randomIndex ++;

            if (randomIndex > sectionsPool.Length - 1)
               randomIndex = 0;
         }
      }
      
      return sectionsPool[randomIndex];
   }
   
}

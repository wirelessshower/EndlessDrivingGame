using UnityEngine;
using YG;

public class Controller : MonoBehaviour
{
    private void Start() {             
        if(YG2.envir.isMobile)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}

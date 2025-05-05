using TMPro;
using UnityEngine;
using YG;

using PlayerPrefs = RedefineYG.PlayerPrefs;
public class MoneyText : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    private int money;

    private void Update() {
        money = PlayerPrefs.GetInt("Money");
        m_Text.text = money.ToString();
    }
     
}

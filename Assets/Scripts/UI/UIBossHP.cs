using UnityEngine;
using UnityEngine.UI;

public class UIBossHP : MonoBehaviour
{
    [SerializeField]
    private Image imageHP; 

    public void UpdateHP(float percent)
    {
        imageHP.fillAmount = percent;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LodingCon : MonoBehaviour
{
    public TMP_Text LodingText;
    public Image LodingBar;

    private void Awake()
    {
        Debug.Log("�ε�������");
    }

    public void SetLodingProgress(float value)
    {
        LodingBar.fillAmount = value;
    }
}

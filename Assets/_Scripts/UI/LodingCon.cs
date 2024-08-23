using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LodingCon : MonoBehaviour
{
    public TMP_Text LodingText;
    public Image LodingBar;

    public void SetLodingProgress(float value)
    {
        LodingBar.fillAmount = value;
    }
}

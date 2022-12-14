using UnityEngine;
using TMPro;

public class fpsCounter : MonoBehaviour
{
    public TMP_Text fpsText;
    void Update()
    {
        fpsText.text = "" + (int)(1f / Time.unscaledDeltaTime);
    }
}

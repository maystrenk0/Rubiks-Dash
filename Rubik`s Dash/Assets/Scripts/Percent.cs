using UnityEngine.UI;
using UnityEngine;

public class Percent : MonoBehaviour {
    public Text percentText;
    public Text maxPercentText;
    public Transform pos;
    private float curScore = 0;
    private float maxScore = 0;
    void Update() {
        curScore = Mathf.RoundToInt((pos.position.z + 7.5f) * 100f / 227.5f);

        maxScore = PlayerPrefs.GetFloat("MaxScore", 0f);

        maxScore = Mathf.Max(maxScore, curScore);
        PlayerPrefs.SetFloat("MaxScore", maxScore);
        string text = curScore.ToString() + " %";
        string maxText = maxScore.ToString() + " %";
        percentText.text = text;
        maxPercentText.text = maxText;
    }
}

using UnityEngine.UI;
using UnityEngine;

public class Percent : MonoBehaviour {
    public Text percentText;
    public Transform pos;
    private float curScore = 0;
    private float maxScore = 0;
    void Update() {
        curScore = Mathf.RoundToInt((pos.position.z + 7.5f) * 100f / 227.5f);
        maxScore = Mathf.Max(maxScore, curScore);
        string text = curScore.ToString() + " %";
        percentText.text = text;
    }
}

using UnityEngine.UI;
using UnityEngine;

public class Percent : MonoBehaviour {
    public Text percentText;
    public Transform pos;
    private float maxScore = 0;
    void Update() {
         maxScore = Mathf.Max(maxScore, Mathf.RoundToInt((pos.position.z + 7.5f) * 100f / 227.5f));
        string text = maxScore.ToString() + " %";
        percentText.text = text;
    }
}

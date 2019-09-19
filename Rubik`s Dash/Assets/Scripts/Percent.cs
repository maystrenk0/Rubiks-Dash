using UnityEngine.UI;
using UnityEngine;

public class Percent : MonoBehaviour {
    public Text percentText;
    public Transform pos;
    void Update() {
        string text = Mathf.RoundToInt((pos.position.z+7.5f)*100f/227.5f).ToString() + " %";
        percentText.text = text;
    }
}

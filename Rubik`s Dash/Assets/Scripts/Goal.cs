using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    public GameManager gameManager;
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            gameManager.CompleteLevel();
        }
    }
}

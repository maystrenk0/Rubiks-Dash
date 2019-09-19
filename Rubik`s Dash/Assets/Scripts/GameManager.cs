using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text runText;
    public GameObject wall;
    public GameObject spawnPoint;
    static int amountOfSpawnpoints = 15;
    GameObject[] spawnPoints = new GameObject[amountOfSpawnpoints];
    GameObject instantiatedObj;
    public Rigidbody player;
    public GameObject completeLevelUI;
    public Percent percent;
    public static float spawnDelay = 0.5f;
    float destroyDelay = 10f;
    bool gameHasEnded = false;
    float restartDelay = 1f;
    int run = 1;
    void Start() {
        run = PlayerPrefs.GetInt("Run", 1);
        spawnDelay = PlayerPrefs.GetFloat("Delay", 0.5f);
        runText.text += run.ToString();
        int deg = 180;
        for (int i = 0; i < amountOfSpawnpoints; ++i) {
            Vector3 pos = new Vector3(9 * Mathf.Pow(-1, i), 3, 15 * i);
            Quaternion rot = new Quaternion(0, deg, 0, 0);
            GameObject obj = (GameObject)Instantiate(spawnPoint, pos, rot);
            spawnPoints[i] = (GameObject)obj;
            if (i % 2 == 0) {
                deg = 0;
            }
            else {
                deg = 180;
            }
        }
        InvokeRepeating("SpawnWall", 0, spawnDelay);
    }
    void SpawnWall() {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnpoint = spawnPoints[randomIndex].transform;
        instantiatedObj = (GameObject)Instantiate(wall, spawnpoint.position, spawnpoint.rotation);

        Destroy(instantiatedObj, destroyDelay);

    }
    private void Update() {
        if (player.position.y < -1f)
            EndGame();
    }
    public void CompleteLevel() {
        percent.enabled = false;
        completeLevelUI.SetActive(true);
        spawnDelay *= 0.9f;
        ++run;
        PlayerPrefs.SetInt("Run", run);
        PlayerPrefs.SetFloat("Delay", spawnDelay);
        Invoke("RestartGame", restartDelay);
    }
    public void EndGame() {
        if (!gameHasEnded) {
            gameHasEnded = true;
            percent.enabled = false;
            /*
            spawnDelay = 0.5f;
            run = 1;
            PlayerPrefs.SetInt("Run", run);
            PlayerPrefs.SetFloat("Delay", spawnDelay);
            */
            Invoke("RestartGame", restartDelay);
        }
    }
    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetData() {
        PlayerPrefs.DeleteAll();
        RestartGame();
    }
}

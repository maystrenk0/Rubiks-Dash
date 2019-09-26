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
    private Transform spawnParent;
    private Transform wallParent;
    void Start() {
        for (int i = 0; i < amountOfSpawnpoints; i++)
        {
            activeSpawnPoints[i] = true;
        }
        spawnParent = new GameObject(nameof(spawnParent)).transform;
        wallParent = new GameObject(nameof(wallParent)).transform;
        run = PlayerPrefs.GetInt("Run", 1);
        spawnDelay = PlayerPrefs.GetFloat("Delay", 0.5f);
        runText.text += run;
        int deg = 180;
        for (int i = 0; i < amountOfSpawnpoints; ++i) {
            Vector3 pos = new Vector3(9 * Mathf.Pow(-1, i), 3, 15 * i);
            Quaternion rot = new Quaternion(0, deg, 0, 0);
            spawnPoints[i] = Instantiate(spawnPoint,pos, rot, spawnParent);
            deg = (i % 2) * 180; 
        }
        InvokeRepeating("SpawnWall", 0, spawnDelay);
    }
    [HideInInspector] public bool[] activeSpawnPoints = new bool[amountOfSpawnpoints];
    void SpawnWall() {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        if (!activeSpawnPoints[randomIndex])
            return;
        Transform spawnpoint = spawnPoints[randomIndex].transform;
        instantiatedObj = (GameObject)Instantiate(wall, spawnpoint.position, spawnpoint.rotation,wallParent);

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

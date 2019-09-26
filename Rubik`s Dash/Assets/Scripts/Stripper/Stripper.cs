using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stripper : MonoBehaviour
{
    private Transform worldCanvas => GameObject.FindGameObjectWithTag("WorldCanvas").transform;
    public GameObject stripPrefab;
    private GameManager _gameManager;
    private GameManager gameManager => _gameManager == null ? _gameManager = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<GameManager>() : _gameManager;
    private float playerZPos => gameManager.player.transform.position.z;
    private void Start()
    {
        stripPrefab = Instantiate(stripPrefab, transform.GetChild(0).position,Quaternion.identity,worldCanvas);
        stripPrefab.SetActive(false);
    }

    void Update()
    {
        if (playerZPos > transform.position.z)
        {
            gameManager.activeSpawnPoints[transform.GetSiblingIndex()] = false;
            stripPrefab.SetActive(true);
            Destroy(this);
        }
    }
}

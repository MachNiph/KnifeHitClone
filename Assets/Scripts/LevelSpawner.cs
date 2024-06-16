using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] woods;
    [SerializeField]
    private Vector2 spawnPositon;
    [SerializeField]
    private Vector2 shatteringWoodSpawnPositon;

    public GameObject curremtWood;
    public ShatteringWood shatteringWoodPrefab;
    public ShatteringWood currentShatteringWood;
    [SerializeField]
    private KnifeController knifeController;
    [SerializeField]
    private int currentLevelIndex=0;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    public int level;

  

    private void Start()
    {
        StartCoroutine(LevelChange());
    }

    private IEnumerator LevelChange()
    {

        while (true)
        {
            level++;
            PlayerPrefs.SetInt("Level", level);
            audioSource.Play();
            GameObject wood = Instantiate(woods[currentLevelIndex], spawnPositon, Quaternion.identity);
            currentShatteringWood = Instantiate(shatteringWoodPrefab, shatteringWoodSpawnPositon, Quaternion.identity);
            curremtWood = wood;
            yield return new WaitUntil(() => knifeController.lastKnifeHitWood);
            yield return new WaitForSeconds(1f);
            currentLevelIndex++;
            knifeController.lastKnifeHitWood = false;

            knifeController.SpawnKnife(Random.Range(5,10));
            knifeController.KnifeViewSpawn();
        }
    }
}

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

    private void Start()
    {
        StartCoroutine(LevelChange());
    }

    private IEnumerator LevelChange()
    {

        while (true)
        {
            GameObject wood = Instantiate(woods[currentLevelIndex], spawnPositon, Quaternion.identity);
            currentShatteringWood = Instantiate(shatteringWoodPrefab, shatteringWoodSpawnPositon, Quaternion.identity);
            curremtWood = wood;
            yield return new WaitUntil(() => knifeController.lastKnifeHitWood);
            yield return new WaitForSeconds(2f);
            currentLevelIndex++;
            knifeController.lastKnifeHitWood = false;

            knifeController.SpawnKnife(Random.Range(5,14));
            knifeController.KnifeViewSpawn();
        }
    }
}

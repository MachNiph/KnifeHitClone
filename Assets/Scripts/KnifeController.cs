using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField]
    private Knife[] knives;
    [SerializeField]
    private int knifeHitIndex;

    [SerializeField]
    private GameObject knifeThrownView;
    [SerializeField]
    private GameObject[] knifeThrownViews;

    [SerializeField]
    private LevelSpawner levelSpawner;

    [SerializeField]
    private Vector2 initialPosition;
    [SerializeField]
    private float spacing;
    public bool lastKnifeHitWood = false;
    [SerializeField]
    private float gameViewHeight;

    [SerializeField]
    private TextMeshProUGUI scoreTextMeshPro;
    private int score = 0;
    [SerializeField]
    private TextMeshProUGUI appleScoreShow;
   

    [SerializeField]
    private Knife knifePrefab;
    public Sprite[] knifeSprites;

    private int totalAppleScore = 0;


    private void Start()
    {
        SpawnKnife(9);
        knifeThrownViews = new GameObject[knives.Length];

        KnifeViewSpawn();
    }

    public void KnifeViewSpawn()
    {
        // Clear existing knife views
        if (knifeThrownViews != null)
        {
            for (int i = 0; i < knifeThrownViews.Length; i++)
            {
                if (knifeThrownViews[i] != null)
                {
                    Destroy(knifeThrownViews[i]);
                }
            }
        }

        // Initialize the knifeThrownViews array
        knifeThrownViews = new GameObject[knives.Length];

        float totalKnifeHeight = knives.Length * spacing;
        float adjustedInitialY = Mathf.Max(initialPosition.y, totalKnifeHeight / 2 - gameViewHeight / 2);
        Vector2 adjustedInitialPos = new Vector2(initialPosition.x, adjustedInitialY);

        for (int i = 0; i < knives.Length; i++)
        {
            Vector2 spawnPosition = adjustedInitialPos - new Vector2(0, i * spacing);
            GameObject knifeView = Instantiate(knifeThrownView, spawnPosition, Quaternion.identity);
            knifeView.transform.rotation = quaternion.Euler(0, 0, 120);
            knifeThrownViews[i] = knifeView;
        }
    }

    private void ChangeKnifeColor(int index)
    {
        if (index >= 0 && index < knifeThrownViews.Length)
        {
            SpriteRenderer sr = knifeThrownViews[index].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.black;
            }
        }
    }

    public void SpawnKnife(int howManyKnives)
    {
        knifeHitIndex = 0;
        knives = new Knife[howManyKnives];

        for (int i = 0; i < howManyKnives; i++)
        {
            Knife knife = Instantiate(knifePrefab, new Vector2(0, -3), quaternion.identity);
            knife.spriteRenderer.sprite = knifeSprites[PlayerPrefs.GetInt("knifeIndex")];
            knife.transform.SetParent(transform);
            knives[i] = knife;
            knife.knifeController = this;
            knives[i].gameObject.SetActive(i == 0); // Activate only the first knife initially
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && knifeHitIndex < knives.Length)
        {
            if (knifeHitIndex == 0 || !knives[knifeHitIndex - 1].dead)
            {
                knives[knifeHitIndex].Throw();
                ChangeKnifeColor(knifeHitIndex);
                knifeHitIndex++;
                if (knifeHitIndex < knives.Length)
                {
                    knives[knifeHitIndex].gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnKnifeHitWood(Knife knife)
    {
        score++;
        UpdateScore();

        if (knifeHitIndex == knives.Length) // Winning that level condition here
        {
            lastKnifeHitWood = true;
            CheckWoodDestruction();
        }
    }

    private void CheckWoodDestruction()
    {
        if (lastKnifeHitWood)
        {
            levelSpawner.currentShatteringWood.Shatter();
            Destroy(levelSpawner.curremtWood.gameObject);
        }
    }

    private void UpdateScore()
    {
        scoreTextMeshPro.text = score.ToString();
    }

    public void UpdateAppleScore()
    {
        
        totalAppleScore += 2;
        appleScoreShow.text = totalAppleScore.ToString();
        
    }


}

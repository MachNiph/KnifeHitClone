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
    private ShatteringWood shatteringWood;
    [SerializeField]
    private Wood wood;
    [SerializeField]
    private GameObject knifeThrownView;
    [SerializeField]
    private GameObject[] knifeThrownViews;

    [SerializeField]
    private Vector2 initialPosition;
    [SerializeField]
    private float spacing;
    private bool lastKnifeHitWood = false;
    [SerializeField]
    private float gameViewHeight;

    [SerializeField]
    private TextMeshProUGUI scoreTextMeshPro;
    private int score = 0;

    private void Start()
    {
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
        if (index >= 0 && index < knives.Length)
        {
            SpriteRenderer sr = knifeThrownViews[index].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.black;
            }
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
            Debug.Log("Shattering wood");
            shatteringWood.Shatter();
            Destroy(wood.gameObject);
        }
    }

    private void UpdateScore()
    {
        scoreTextMeshPro.text = score.ToString();
    }
}

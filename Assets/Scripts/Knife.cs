using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float throwForce = 10;
    [SerializeField]
    private float rotateSpeed = 10;
    [SerializeField]
    private float backForce = 8;
    [SerializeField]
    private ParticleSystem woodParticleSystem;
    [SerializeField]
    private GameObject appleCutSystem;
    public bool dead;
    public bool canThrow = true;
    public KnifeController knifeController;
    public float delayBeforeGameOver = 1f;
    public SpriteRenderer spriteRenderer;
    public int appleScore = 0;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClip;

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }
        if (audioClip.Length == 0)
        {
            Debug.LogError("AudioClip array is empty!");
        }
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            rb.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            StartCoroutine(LoadGameOverAfterDelay());
        }
    }

    public void Throw()
    {
        if (canThrow && !dead)
        {
            PlayAudio(0);
            rb.AddForce(new Vector2(0, throwForce), ForceMode2D.Impulse);
        }
    }

    private IEnumerator LoadGameOverAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            PlayAudio(1);
            transform.SetParent(collision.transform);
            rb.velocity = Vector2.zero;
            Instantiate(woodParticleSystem, transform.position, Quaternion.identity);
           
            rb.isKinematic = true;

            if (knifeController != null)
            {
                knifeController.OnKnifeHitWood(this);
            }

            StartCoroutine(VibrateWood(collision.gameObject));

        }
        else if (collision.gameObject.CompareTag("Knife") && collision.GetComponent<Knife>().rb.isKinematic)
        {
            PlayAudio(2);
            rb.velocity = Vector2.zero;
            rb.isKinematic = false;
            transform.SetParent(null);
            dead = true;
            rb.AddForce(new Vector2(0, -backForce), ForceMode2D.Impulse);
            rb.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if (collision.gameObject.CompareTag("Apple"))
        {
            PlayAudio(3);
            Instantiate(appleCutSystem, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            appleScore += 2;
            knifeController.UpdateAppleScore();
        }
    }

    private IEnumerator VibrateWood(GameObject woodObject)
    {
        Vector3 originalPosition = woodObject.transform.position;
        float vibrationDuration = 0.05f;  
        float vibrationIntensity = 0.05f; 
        float elapsedTime = 0f;

        while (elapsedTime < vibrationDuration)
        {
            Vector3 randomOffset = Random.insideUnitCircle * vibrationIntensity;
            woodObject.transform.position = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        woodObject.transform.position = originalPosition;
    }

    private void PlayAudio(int clipIndex)
    {
        if (audioClip.Length > clipIndex && audioClip[clipIndex] != null)
        {
            audioSource.clip = audioClip[clipIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"AudioClip at index {clipIndex} is not assigned!");
        }
    }
}

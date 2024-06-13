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

    private void Update()
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
            transform.SetParent(collision.transform);
            rb.velocity = Vector2.zero;
            Instantiate(woodParticleSystem, transform.position, Quaternion.identity);
            rb.isKinematic = true;

            if (knifeController != null)
            {
                knifeController.OnKnifeHitWood(this);
            }
        }
        else if (collision.gameObject.CompareTag("Knife") && collision.GetComponent<Knife>().rb.isKinematic)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = false;
            transform.SetParent(null);
            dead = true;
            rb.AddForce(new Vector2(0, -backForce), ForceMode2D.Impulse);
            rb.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if (collision.gameObject.CompareTag("Apple"))
        {
            Instantiate(appleCutSystem, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}

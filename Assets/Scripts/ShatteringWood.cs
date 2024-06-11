using UnityEngine;

public class ShatteringWood : MonoBehaviour
{
    public Transform[] woodPieces;
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 0.5f;
    public float maxTorque = 20f; 
    public float maxAngularVelocity = 100f; 
    public void Shatter()
    {
        foreach (Transform woodPiece in woodPieces)
        {
            Rigidbody2D rb = woodPiece.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 explosionDirection = woodPiece.position - transform.position;
                float distance = explosionDirection.magnitude;
                if (distance < explosionRadius)
                {
                    float explosionEffect = 1 - (distance / explosionRadius);
                    Vector2 force = explosionDirection.normalized * explosionForce * explosionEffect;
                    force.y += upwardsModifier * explosionForce * explosionEffect;

                    rb.AddForce(force, ForceMode2D.Impulse);

                   
                    float torque = Random.Range(-maxTorque, maxTorque);
                    rb.AddTorque(torque, ForceMode2D.Impulse);

                   
                    if (Mathf.Abs(rb.angularVelocity) > maxAngularVelocity)
                    {
                        rb.angularVelocity = Mathf.Sign(rb.angularVelocity) * maxAngularVelocity;
                    }
                }
            }
        }
    }
}

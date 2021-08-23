using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float invincibilityFrame = 0.5f;
    [SerializeField] private int numFlickers = 15;
    [SerializeField] private SpriteRenderer[] flickerRenderers;

    private bool isInvincible = false;

    private new Rigidbody2D rigidbody2D;
    private YieldInstruction invincibilityFrameInstruction;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        invincibilityFrameInstruction = new WaitForSeconds(invincibilityFrame / (float)numFlickers);
    }

    void OnBecameInvisible()
    {
        if (!flickerRenderers[0].enabled) return;
        transform.position = Vector2.zero;
    }

    public void TakeDamage(int damage)
    {
        //If invincible, don't take damage
        if (isInvincible)
        {
            return;
        }

        StartCoroutine(InvincibilityFrame());
    }

    private IEnumerator InvincibilityFrame()
    {
        isInvincible = true;

        for (int i = 0; i < numFlickers; i++)
        {
            yield return invincibilityFrameInstruction;
            foreach (SpriteRenderer r in flickerRenderers)
            {
                r.enabled = !r.enabled;
            }
        }

        isInvincible = false;

        //This is to ensure that the renderer is always enabled at the end of this function
        //The wait time is so the flicker won't look weird
        yield return invincibilityFrameInstruction;
        foreach (SpriteRenderer r in flickerRenderers)
        {
            r.enabled = true;
        }
    }

    public void ApplyKnockback(Vector2 knockbackForce)
    {
        rigidbody2D.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}

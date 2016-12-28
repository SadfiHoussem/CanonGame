using System.Collections;
using UnityEngine;

class GiveDamageToEnemy : MonoBehaviour
{
    public int DamageToGive = 20;
    public GameObject Explosion;
    public float TimerBeforeSelfExplose = 5f;

    public void Start()
    {
        StartCoroutine(SelfDestruction());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var alien = other.GetComponent<Alien>();
        var alienShip = other.GetComponent<AlienShip>();

        if ((alien == null) && (alienShip == null))
            return;
        
        if (alien != null)
        {
            alien.TakeDamage(DamageToGive);
            Explose();
            DestroyProjectile();
        }
        else
        {
            alienShip.TakeDamage(DamageToGive);
            Explose();
            DestroyProjectile();
        }

    }

    public void Explose()
    {
        // Explosion Effect
        GameObject explosion = Instantiate(Explosion) as GameObject;
        explosion.AddComponent<Rigidbody2D>().transform.position = transform.position;
        explosion.GetComponent<Rigidbody2D>().gravityScale = 0;
        Destroy(explosion, 0.1f);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(TimerBeforeSelfExplose);

        if (gameObject != null)
        {
            Explose();
            DestroyProjectile();
        }

    }

}
 

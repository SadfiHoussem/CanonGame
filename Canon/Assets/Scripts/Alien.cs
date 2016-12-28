using UnityEngine;
using System.Collections;

class Alien : MonoBehaviour
{
    public int MaxHealth = 100;
    public int DamageToGive = 25;
    public Player target;
    public float speed;

    public int Health { get; private set; }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.TakeDamage(DamageToGive);
        // Die after damaging the player
        KillAlien();

    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            KillAlien();

    }

    public void KillAlien()
    {
        StartCoroutine(KillAlienCo());
    }

    public IEnumerator KillAlienCo()
    {
        speed = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
        GetComponent<Rigidbody2D>().gravityScale = 0.8f;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
    
    public void Start()
    {
        Health = MaxHealth;
    }

    public void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

}


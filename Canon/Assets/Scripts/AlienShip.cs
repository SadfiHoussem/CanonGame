using UnityEngine;

public class AlienShip : MonoBehaviour
{
    public GameObject DestroyedAlienShip;
    public GameObject Alien;
    public Transform[] AlienSpawnPoints;
    public int MaxHealth = 200;
    public float FloatStrength = 1;
    public Transform SpawnPoint;
    public float spawnTime = 3f;
    //public Transform DestinationPoint;
    //public float Speed;
    public int Health { get; private set; }
    public bool IsDead { get; private set; }

    private float originalY;
    //private Transform Target;
    //private float FallSpeed;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            LevelManager.instance.KillAlienShip();

    }

    public void Kill()
    {
        IsDead = true;
        Health = 0;
        //Target.position = new Vector3(0, 0, 0);
        //FallSpeed = Speed * 5;
    }

    public void Respawn()
    {
        IsDead = false;
        Health = MaxHealth;
        transform.position = SpawnPoint.position;
        GetComponent<Collider2D>().enabled = true;
        //Target = DestinationPoint;
        //FallSpeed = Speed;
    }

    void SpawnAlien()
    {
        if (!IsDead)
        {
            var newAlien = Instantiate(Alien) as GameObject;
            newAlien.AddComponent<Rigidbody2D>().gravityScale = 0f;
            int numSpawnPoint = Random.Range(0, AlienSpawnPoints.Length);
            newAlien.transform.position = AlienSpawnPoints[numSpawnPoint].position;
        }
    }

    public void Start()
    {
        InvokeRepeating("SpawnAlien", spawnTime, spawnTime);
        Health = MaxHealth;
        originalY = transform.position.y;
        //Target = DestinationPoint;
        //FallSpeed = Speed;
    }

    public void Update()
    {
        if (!IsDead)
        {
            transform.position = new Vector3(transform.position.x,
                originalY + (Mathf.Sin(Time.time) * FloatStrength),
                transform.position.z);
        }
    }

}

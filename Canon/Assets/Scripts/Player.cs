using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int MaxHealth = 100;
    public int Health { get; private set; }
    public bool IsDead { get; private set; }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
            LevelManager.instance.KillPlayer();
        
    }

    public void Kill()
    {
        IsDead = true;
        Destroy(gameObject);
    }
    
    // Use this for initialization
    void Start () {
        Health = MaxHealth;
        IsDead = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

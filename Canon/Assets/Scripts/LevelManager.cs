using System.Collections;
using UnityEngine;

class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    public Player Player { get; private set; }
    public AlienShip AlienShip { get; private set; }

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        Player = FindObjectOfType<Player>();
        AlienShip = FindObjectOfType<AlienShip>();
    }

    public void Update()
    {

    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
        Player.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
        Player.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);

        Player.Kill();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void KillAlienShip()
    {
        StartCoroutine(KillAlienShipCo());
    }
    private IEnumerator KillAlienShipCo()
    {
        AlienShip.GetComponent<Renderer>().enabled = false;
        GameObject destroyedAlienShip = Instantiate(AlienShip.DestroyedAlienShip) as GameObject;
        destroyedAlienShip.AddComponent<Rigidbody2D>().transform.position = AlienShip.transform.position;
        destroyedAlienShip.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 8);
        destroyedAlienShip.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
        AlienShip.GetComponent<Collider2D>().enabled = false;
        AlienShip.Kill();
        yield return new WaitForSeconds(6f);

        Destroy(destroyedAlienShip);
        AlienShip.GetComponent<Renderer>().enabled = true;
        AlienShip.Respawn();

    }

}
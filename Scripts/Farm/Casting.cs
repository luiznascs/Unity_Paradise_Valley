using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private int percentage; // porcentagem de chance de pescar um peixe a cada tentativa
    [SerializeField] private GameObject fishPrefab;

    private PlayerItems player;
    private PlayerAnim playerAnim;

    private bool detectingPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerItems>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if(detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStarted();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if(randomValue <= percentage)
        {
            // conseguiu pescar um peixe
            Instantiate(fishPrefab, player.transform.position + new UnityEngine.Vector3(Random.Range(-3f, -1f), 0f, 0f), UnityEngine.Quaternion.identity);
        }
        else
        {
            // falhou
            Debug.Log("Não pescou...");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}

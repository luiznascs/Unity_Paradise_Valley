using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerItems>().fishes++;
            Destroy(gameObject);
        }
    }
}

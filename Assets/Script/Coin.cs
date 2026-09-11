using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.CompareTag("Player"))
      {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        playerMovement.Coins += 1;
        Destroy (this.gameObject);
      }
    }
}

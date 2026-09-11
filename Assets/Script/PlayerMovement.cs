using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;
    public int Coins;
   

    public TMPro.TextMeshProUGUI cointText;
    private Vector2 arahGerak;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMove(InputValue value)
    {
      arahGerak = value.Get<Vector2>();  
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            Coins += 1;
            gameObject.GetComponent<GameManager>().AmbilKoin();
            Destroy (collision.gameObject);
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah*kecepatan*Time.deltaTime;

        cointText.text = Coins.ToString();
    }
}

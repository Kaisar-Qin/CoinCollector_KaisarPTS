using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winUI;
    public int totalCoins;
    private int koinTerkumpul = 0;
    [SerializeField] private int skor = 0;
    private int jumlahZombieMati = 0;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Enemy.OnZombieMati += TambahSkorSaatZombieMati;
        Enemy.OnZombieMati += SaatZombieMati;
    }

    void OnDisable()
    {
        Enemy.OnZombieMati -= TambahSkorSaatZombieMati;
        Enemy.OnZombieMati -= SaatZombieMati;
    }
    void TambahSkorSaatZombieMati(Enemy zombie)
    {
        skor += 10;
        Debug.Log("Skor: " + skor);
    }
    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

     void SaatZombieMati(Enemy zombie)
    {
        jumlahZombieMati++;
        Debug.Log("GameManager dengar event. Zombie mati: " + jumlahZombieMati + " (" + zombie.name + ")");
    }
    public void AmbilKoin()
    {
        koinTerkumpul++;
        if(koinTerkumpul == totalCoins)
        {
            Menang();
        }
    }

    void Menang()
    {
        winUI.SetActive(true);
    }

    // Update is called once per frame
}

using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;
    [SerializeField] public int hp =  100;
    [SerializeField] private float radiusPatrol = 3f; 
    [SerializeField] private int damageSaatTabrakan = 20;
    private Vector2 titikAwal;     
     private Vector2 tujuanPatrol;   
    public float ms = 2f;

    float JarakKePlayer()
    {
        if (player == null)
        return Mathf.Infinity;

        return Vector2.Distance(transform.position, player.position);
    }

    private StateZombie state = StateZombie.IDLE; 
    private float waktuSerangTerakhir; 
    
    public static event Action<Enemy> OnZombieMati;
    
    protected Transform player;
    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        titikAwal = transform.position; 
        PilihTujuanPatrolBaru(); 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Kejar();
         
         PeriksaTransisi();           
         switch (state)     
         {         
            case StateZombie.IDLE:   PerilakuIdle();   break;         
            case StateZombie.PATROL: PerilakuPatrol(); break;         
            case StateZombie.CHASE:  PerilakuChase();  break;         
            case StateZombie.ATTACK: PerilakuAttack(); break;     
        } 
    }
    void PerilakuIdle()   { } 
    void PerilakuPatrol() 
        {transform.position = Vector2.MoveTowards
        (  transform.position, 
        tujuanPatrol, 
        ms * 0.5f * Time.deltaTime);       
        if (Vector2.Distance(transform.position, tujuanPatrol) < 0.1f)         
        PilihTujuanPatrolBaru();  
         Debug.Log("Enemy sedang PATROL");
        } 
    void PilihTujuanPatrolBaru() 
    {     Vector2 acak = UnityEngine.Random.insideUnitCircle * radiusPatrol;     
        tujuanPatrol = titikAwal + acak; 
        } 
    void PerilakuChase()  { 
        Kejar(); 
       Debug.Log("Enemy sedang CHASE");
        }   
    void PerilakuAttack() 
     {     
        if (Time.time >= waktuSerangTerakhir + jedaSerang)     
        {         Serang();     waktuSerangTerakhir = Time.time;     } 
        Debug.Log("Enemy sedang ATTACK");
    } 

    void PeriksaTransisi() {     float jarak = JarakKePlayer();       
                if (jarak <= jarakSerang)         state = StateZombie.ATTACK;         
                else if (jarak <= jarakDeteksi)         state = StateZombie.CHASE;      
                 else         state = StateZombie.PATROL;     
         } 
     

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position, 
            player.position, 
            ms * Time.deltaTime
        );
    }
    public virtual  void Serang()
    {
        Debug.Log("Enemy Menyerang");
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;    
         Debug.Log(name + " kena " + jumlah + " damage. Sisa HP: " + hp);   

         if (hp <= 0) 
         {
            Mati(); 
         }
    }

    protected virtual void Mati()
    {
        Debug.Log("Enemy Mati");
        OnZombieMati?.Invoke(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KenaDamage(damageSaatTabrakan);
        }
    }

    
}

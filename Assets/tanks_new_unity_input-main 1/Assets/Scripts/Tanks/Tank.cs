using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Tank : MonoBehaviour
{
    public Vector3 spawnPosition;
    Vector2 tankMovementSpeed;
    public float tankSpeed = 1f;
    public float tankRotSpeed = 10f;
    public int maxHealth = 100;
    public HealthBar healthBar;
    public int forcePunch = 30000;
    public GameObject Tanque;
    public Renderer chasis;
    public Renderer top;
    int tankIndex;
    public int counter;
    public int counterKills;
    public TextMeshProUGUI Player;
    

   // public Material tank;

    private int currHealth;

    public GameObject bulletPrefab;
    //public GameObject bulletPrefab;
    public Transform spawnBulletPos;

    private void Awake()
    {
        chasis.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        top.material.color = chasis.material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        chasis = GetComponent<Renderer>();
        top = GetComponent<Renderer>();
        counter = 0;
        counterKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(tankSpeed * tankMovementSpeed.y * Time.deltaTime * Vector3.forward);
        transform.Rotate(tankRotSpeed * tankMovementSpeed.x * Time.deltaTime * Vector3.up);

        
    }
    public void setIndex(int index)
    {
        tankIndex = index;
        Player.SetText($"P{index + 1}");
    }

    public void SetInitialPos(Vector3 position)
    {
        spawnPosition = position;
        transform.position = position;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        if (!gameObject.activeInHierarchy) return;

        /*Rigidbody bullet = Instantiate(bulletPrefab, spawnBulletPos.position, spawnBulletPos.rotation) as Rigidbody;
        bullet.velocity = 50 * spawnBulletPos.forward;*/
        var bullet = Instantiate(bulletPrefab, spawnBulletPos.position, spawnBulletPos.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * forcePunch);
        var bulletSc = bullet.GetComponent<Bullet>();
        bulletSc.shootingTank = this.gameObject;
        bulletSc.shootingTankIndex = tankIndex;
    }

    public void Move(InputAction.CallbackContext context)
    {
        tankMovementSpeed = context.ReadValue<Vector2>();
        //transform.Translate(10 * context.ReadValue<Vector2>().y * Time.deltaTime * Vector3.forward);
    }

    //Funcion para recibir damage
    public void TakeDamage(int damage, int killingTankIndex)
    {
       
        currHealth -= damage;
        healthBar.UpdateHealth((float)currHealth / (float)maxHealth);

        //reiniciando al tanque
        if (currHealth <= 0)
        {
            currHealth = maxHealth;
            healthBar.UpdateHealth(100f);

            GameManager.instance.UpdateKills(killingTankIndex);
            
            SetInitialPos(spawnPosition);
            counter++;
        }
    }

    //Funcion para sumar muertes ganadas
    void winKill()
    {
        counterKills++;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
            
            
        }
    }*/

}

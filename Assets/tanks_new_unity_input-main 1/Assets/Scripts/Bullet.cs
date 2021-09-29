using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public int shootingTankIndex;
    public GameObject shootingTank;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Tank myTank = other.GetComponent<Tank>();

        if(myTank!=null)
        {
            if (myTank.gameObject == shootingTank) return;

            myTank.TakeDamage(damage, shootingTankIndex);
        }
        
    }
}

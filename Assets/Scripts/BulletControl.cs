using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    Enemy enemy;
    Rigidbody2D physc;
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy").GetComponent<Enemy>(); // Enemy e ulaşmak için yapıldı
        physc = GetComponent<Rigidbody2D>();
        physc.AddForce(enemy.getDirection()*1000);
    }

    
    void Update()
    {
        
    }
}

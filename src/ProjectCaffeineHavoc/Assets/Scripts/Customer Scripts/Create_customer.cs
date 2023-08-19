using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_customer : MonoBehaviour
{
    private int customer_count = 0;

    public GameObject customer_prefab;
    public int xPos;
    public int zPos;

    void Update()
    {   
        GameObject[] initialCustomers = GameObject.FindGameObjectsWithTag("Customer");
        customer_count = initialCustomers.Length;

        // müşteri sayısı 4 müşteriden az ise yeni müşteri oluştur.
        if(customer_count <= 4) {
            xPos = Random.Range(50, 100);
            zPos = Random.Range(50, 100);
            Vector3 spawnPosition = new Vector3(xPos, 2.66f, zPos);
            Instantiate(customer_prefab, new Vector3(xPos, 2.66f, zPos), Quaternion.identity);
        }
    }
}

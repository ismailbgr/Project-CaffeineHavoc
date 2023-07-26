using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    public NavMeshAgent customer;
    public int destination_number = 0;
    public bool is_in_order = false;
    public List<GameObject> order_places;
    
    void Start()
    {
        customer = GetComponent<NavMeshAgent>();
        order_places = new List<GameObject>(); // Listeyi başlatın (initialize).
        foreach (GameObject place in GameObject.FindGameObjectsWithTag("OrderPlace"))
        {
            order_places.Add(place); // Listeye müşteri objelerini ekleyin.
        }
    }

    void Update()
    {   
        customer.SetDestination(order_places[destination_number].transform.position);

        // Destination'daki OrderPlace objesinin is_there_customer'ı true ise destination_number'ı 1 arttır.
        if (order_places[destination_number].GetComponent<CustomerCheck>().is_there_customer == true)
        {   
            if(!is_in_order) {
                destination_number++; // destination_number'ı bir sonraki OrderPlace'e yönlendirmek için arttırın.  
                destination_number %= order_places.Count; // destination_number'ı liste sınırına göre sıfırlayın.
            }
            
        }

        float distanceToDestination = Vector3.Distance(transform.position, order_places[destination_number].transform.position);
        if (distanceToDestination < 0.1f)
        {
            is_in_order = true;
            customer.speed = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{   
    private Animator animator;
    public NavMeshAgent customer;
    public int destination_number = 0;
    // create only positive number
    public bool is_in_order = false; // müşteri sırada mı?
    public List<GameObject> order_places;
    public float threshold = 0.5f;

    void Start()
    {   
        animator = GetComponent<Animator>();
        customer = GetComponent<NavMeshAgent>();
        order_places = new List<GameObject>(); // Listeyi başlatın (initialize).
        foreach (GameObject place in GameObject.FindGameObjectsWithTag("OrderPlace"))
        {
            order_places.Add(place); // Listeye müşteri objelerini ekleyin.
        }
    }

    void Update()
    {   
        if(!is_in_order) {
            animator.SetBool("isStanding", false);
            customer.SetDestination(order_places[destination_number].transform.position);
        }

        float distanceToDestination = GetDistanceXZ(transform.position, order_places[destination_number].transform.position);
        //Debug.Log(distanceToDestination);
        
        if (distanceToDestination < threshold)
        {   
            is_in_order = true;
            animator.SetBool("isStanding", true);
            customer.SetDestination(order_places[destination_number].transform.position);
            customer.speed = 0;
            order_places[destination_number].GetComponent<CustomerCheck>().is_there_customer = true;
        } 

        // Destination'daki OrderPlace objesinin is_there_customer'ı true ise destination_number'ı 1 arttır.
        if (order_places[destination_number].GetComponent<CustomerCheck>().is_there_customer == true)
        {   
            if(!is_in_order) {
                destination_number++; // destination_number'ı bir sonraki OrderPlace'e yönlendirmek için arttırın.  
                destination_number %= order_places.Count; // destination_number'ı liste sınırına göre sıfırlayın.
            }
            
        }

        if(is_in_order) { //karakterin yüzünü kameraya döndür. Smooth bir dönüş için slerp kullan.
            Vector3 direction = (Camera.main.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }        

        
    }

    float GetDistanceXZ(Vector3 pos1, Vector3 pos2)
    {
        // Sadece x ve z eksenlerine göre uzaklığı hesaplamak için y eksenlerini dikkate almayın.
        Vector2 pos1XZ = new Vector2(pos1.x, pos1.z);
        Vector2 pos2XZ = new Vector2(pos2.x, pos2.z);

        // Sadece x ve z eksenlerine göre uzaklığı hesaplayın.
        float distanceXZ = Vector2.Distance(pos1XZ, pos2XZ);

        return distanceXZ;
    }
        
}


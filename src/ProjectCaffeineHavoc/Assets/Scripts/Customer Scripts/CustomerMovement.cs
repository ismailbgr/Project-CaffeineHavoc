using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{   
    public GameObject customerObject;

    private Animator animator;
    public NavMeshAgent customer;
    public int destination_number = 0;
    // create only positive number
    public bool is_in_order = false; // müşteri sırada mı?
    public List<GameObject> order_places;
    public float threshold = 1.2f;

    public bool is_in_pickup_place = false; // müşteri sipariş sırasında mı?
    public bool is_picked_order = false; // müşteri siparişi aldı mı?

    public float destroyDelay = 6.0f;

    public GameObject mainCamera;

    void Start()
    {   
        animator = GetComponent<Animator>();
        customer = GetComponent<NavMeshAgent>();
        order_places = new List<GameObject>(); // Listeyi başlatın (initialize).
        mainCamera = GameObject.Find("Player");

        foreach (GameObject place in GameObject.FindGameObjectsWithTag("OrderPlace"))
        {
            order_places.Add(place); // Listeye müşteri objelerini ekleyin.
        }
    }

    void Update()
    {   
        if(destination_number == order_places.Count - 1) {
            //5 saniye sonra müşteriyi yok et.
            Invoke("DestroyObject", destroyDelay);
        }

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
                customer.SetDestination(order_places[destination_number].transform.position);
                destination_number %= order_places.Count; // destination_number'ı liste sınırına göre sıfırlayın.
            }
        }

        if (destination_number != 0 && destination_number != order_places.Count-1 && is_in_order && order_places[destination_number-1].GetComponent<CustomerCheck>().is_there_customer == false)
        {   
            destination_number -= 1;
            is_in_order = false;
            customer.SetDestination(order_places[destination_number].transform.position);
            customer.speed = 1f;
            order_places[destination_number+1].GetComponent<CustomerCheck>().is_there_customer = false;
            threshold = 1.2f;
        }

        if(is_in_order) { //karakterin yüzünü kameraya döndür. Smooth bir dönüş için slerp kullan.
            Vector3 direction = (mainCamera.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }        

        if(is_in_order && destination_number == 0) {
            is_in_pickup_place = true;
        }

        if(is_in_order && is_picked_order) {
            destination_number = order_places.Count - 1;
            is_in_order = false;
            customer.SetDestination(order_places[destination_number].transform.position);
            customer.speed = 1f;
            order_places[0].GetComponent<CustomerCheck>().is_there_customer = false;
            threshold = 5f;
            is_picked_order = false;
            is_in_pickup_place = false;
        }

        
    }

    void DestroyObject()
    {
        Destroy(customerObject);
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


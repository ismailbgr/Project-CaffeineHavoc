using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlaceController : MonoBehaviour
{
    // Create object queue and append the main object's children to this list.
    public Queue<GameObject> places = new Queue<GameObject>();
    public List<GameObject> customers; // Customer listesi oluşturun.


    void Awake()
    {
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("OrderPlace"))
        {
            places.Enqueue(child);
        }
    }


    void Start()
    {
        customers = new List<GameObject>(); // Listeyi başlatın (initialize).
        foreach (GameObject customer in GameObject.FindGameObjectsWithTag("Customer"))
        {
            customers.Add(customer); // Listeye müşteri objelerini ekleyin.
        }

        // places objelerinin oyundaki obje isimlerini index sırasına göre yazdır
        
    }

    void Update()
    {
        // Queue'daki tüm objeleri kontrol et. Eğer aynı pozisyonda Customer yoksa is_there_customer'ı false yap.
        foreach (GameObject place in places)
        {
            bool is_there_customer = false;
            foreach (GameObject customer in customers)
            {
                // Eğer müşteri hedefe çok yaklaştıysa, place'in is_there_customer'ını true yap.
                if (GetDistanceXZ(customer.transform.position, place.transform.position) < 0.1f)
                {
                    place.GetComponent<CustomerCheck>().is_there_customer = true;
                    is_there_customer = true; // is_there_customer'ı true olarak işaretle.
                    customer.GetComponent<CustomerMovement>().is_in_order = true; // Müşteri sipariş verdiğinde is_in_order'ı true yap.
                }
            }
            place.GetComponent<CustomerCheck>().is_there_customer = is_there_customer;
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
*/
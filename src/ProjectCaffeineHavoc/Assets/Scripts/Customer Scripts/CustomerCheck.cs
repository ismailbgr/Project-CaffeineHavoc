using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCheck : MonoBehaviour
{
    public bool is_there_customer;

    void Update()
    {
        if (is_there_customer == true)
        {
            Debug.Log(gameObject.name + " is true");
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_on_enter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy();
        //Debug.Log("entered");
        collision.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

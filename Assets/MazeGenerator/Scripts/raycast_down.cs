using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_down : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        //  y + 0.4f
        Ray downRay = new Ray(new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), -Vector3.up);
        //Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), -Vector3.up, Color.red);
        



        if (Physics.Raycast(downRay, out hit))
        {
            //Debug.Log(hit.distance);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f - hit.distance, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
        }

    }
}

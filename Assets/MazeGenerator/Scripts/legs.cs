using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legs : MonoBehaviour
{
    public GameObject foot;
    private float step_distance = 2f;
    float step_radius = 2f;
    Vector3 prev_point;
    float speed = 0.25f;
    public float floor = 1f;
    public bool step_done = true;
    public GameObject step_sensor;
    Vector3 step_sensor_pos;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        prev_point = foot.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(step_sensor.transform.position, foot.transform.position) >= step_distance && step_done) {
            step_done = false;
            dir = -1* (foot.transform.position - step_sensor.transform.position).normalized;
            step_sensor_pos = step_sensor.transform.position;
            //Debug.DrawLine(foot.transform.position, dir, Color.red, Mathf.Infinity); 
        }

        if (Vector3.Distance(foot.transform.position, prev_point) < 
            Vector3.Distance(step_sensor_pos, prev_point) && !step_done)
        {
            step_radius = Vector3.Distance(step_sensor.transform.position, prev_point) / 2f;

            Vector3 c = new Vector3((prev_point.x + step_sensor_pos.x) / 2f,
                                    (prev_point.y + step_sensor_pos.y) / 2f,
                                    (prev_point.z + step_sensor_pos.z) / 2f);




            foot.transform.position += dir * speed;

            this.transform.position = new Vector3(this.transform.position.x,
                System.Convert.ToSingle(System.Math.Sqrt(step_radius * step_radius
                - System.Math.Pow(foot.transform.position.z - c.z, 2) - System.Math.Pow(foot.transform.position.x - c.x, 2)) + c.y),
                this.transform.position.z);
        }
        else
        if (!step_done)
        {
            foot.transform.position = step_sensor_pos;
            prev_point = foot.transform.position;
            step_done = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheels;
    float enginePower = 150.0f;
    public Rigidbody rb;
    float power = 0.0f;
    float brake = 0.0f;
    float steer = 0.0f ;

    float maxSteer = 25.0f;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
        steer = Input.GetAxis("Horizontal") * maxSteer;
        brake = Input.GetKey("space") ? rb.mass * 0.1f : 0.0f;
        if (brake > 0.0f)
        {
            wheels[0].brakeTorque = brake;
            wheels[1].brakeTorque = brake;
            wheels[2].brakeTorque = brake;
            wheels[3].brakeTorque = brake;
            wheels[0].motorTorque = 0.0f;
            wheels[2].motorTorque = 0.0f;
        }
        else
        {
            wheels[0].brakeTorque = 0.0f;
            wheels[1].brakeTorque = 0.0f;
            wheels[2].brakeTorque = 0.0f;
            wheels[3].brakeTorque = 0.0f;
            wheels[0].motorTorque = power;
            wheels[2].motorTorque = power;
        }
        foreach (WheelCollider wheel in wheels)
        {
            wheel.transform.Rotate(new Vector3(steer, 0.0f,0.0f));
        }
    }
}

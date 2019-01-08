using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
    public void GetInput()
    {
        
        
        float[] temp = input.getValues();
        m_nitro = temp[2]==1f;
        m_verticalInput = temp[1];
        m_horizontalInput = temp[0];
        //Debug.Log(temp);
        
    }
    private void Steer()
    {
        m_steeringAngle = MaxSteerAngle * m_horizontalInput;
        WheelRF.steerAngle = m_steeringAngle;
        WheelLF.steerAngle = m_steeringAngle;
    }
    private void Accelerate()
    {
        WheelRF.motorTorque = curMotorForce * m_verticalInput;
        WheelLF.motorTorque = curMotorForce * m_verticalInput;
    }
    private void Brake()
    {
        WheelRF.brakeTorque = 2000.0f;
        WheelLF.brakeTorque = 2000.0f;
    }
    private void releaseBrakes()
    {
        WheelRF.brakeTorque = 0.0f;
        WheelLF.brakeTorque = 0.0f;
    }
    private void Nitro()
    {
        if(m_nitro)
            curMotorForce = MotorForce * 2.5f;
        else
            curMotorForce = MotorForce;
    }
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(WheelRF, TRF);
        UpdateWheelPose(WheelLF, TLF);
        UpdateWheelPose(WheelRB, TRB);
        UpdateWheelPose(WheelLB, TLB);
    }
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Nitro();
        Accelerate();
        /*if(m_brake == true)
            Brake();
        else
            releaseBrakes();*/
        UpdateWheelPoses();
    }
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    private bool m_nitro;
    private bool m_brake;
    //public gameObject driver;
    public WheelCollider WheelRF, WheelLF;
    public WheelCollider WheelRB, WheelLB;
    public Transform TRF, TLF;
    public Transform TRB, TLB;
    public float MaxSteerAngle = 30;
    public float MotorForce;
    private float curMotorForce;
    private getInput input;
    public void Start()
    {
        curMotorForce = MotorForce;
        input = gameObject.GetComponent<getInput>();
        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject.name != "Cylinder001")
            {
                r.enabled = false;
                //Debug.Log(r.gameObject.name);
            }
        }


    }
    /*public void Start()
    {
        Driver d = driver.GetComponent<Driver>();
        m_horizontalInput = d.horizontal;
        m_verticalInput = d.vertical;
        m_nitro = d.nitro;
        m_brake = d.brake;
    }*/

}

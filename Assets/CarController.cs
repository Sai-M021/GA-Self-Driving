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
    private void fitnessH()
    {
        //fitness += Vector3.Distance(LastPos, transform.position);
        //LastPos=transform.position;
        fitness = Vector3.Distance(startPos,transform.position);
    }
    private void FixedUpdate()
    {
        if (!collided)
        {
            
            GetInput();
            Steer();
            Nitro();
            Accelerate();
            UpdateWheelPoses();
            fitnessH();
        }
        
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
    public Boolean collided;

    public float fitness;
    private Vector3 startPos;
    private Quaternion startRot;
    
    private Vector3 LastPos;
    public void Start()
    {
        collided = false;
        curMotorForce = MotorForce;
        input = gameObject.GetComponent<getInput>();
        setRender(false);
        LastPos = transform.position;
        startPos = transform.position;
        startRot = transform.rotation;


    }
    public void setRender(Boolean b)
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject.name != "Cylinder001")
            {
                r.enabled = b;
                //Debug.Log(r.gameObject.name);
            }
        }

    }

    public void reset()
    {
        transform.SetPositionAndRotation(startPos, startRot);
        collided = false;
    }
    public void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject.tag=="Wall")
        {
            collided = true;
        }
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        Debug.Log(fitness);
    }

}


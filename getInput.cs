using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getInput : MonoBehaviour {
    public string type;
   
    private float curTurn = -1;
    private float inc = 0.05f;
    [HideInInspector]
    public NN net;
    [Header("Sensors")]
    public float sensorLength;
    public float frontOffset;
    public float heightOffset;
    public float sideOffset;
    public float angleOffset;
    public LayerMask layerMask;
    // Use this for initialization
    void Start() {
        //Debug.Log(transform.root.gameObject.name);
    }
    public void setType(string t)
    {
        type = t;
    }
	// Update is called once per frame
	public float[] getValues() {
        RaycastHit hit;


        Vector3 FsensorStartPos=transform.position+ transform.forward*frontOffset+transform.up*heightOffset;
        float FSensor;

        Vector3 FRsensorStartPos = transform.position + transform.forward * frontOffset + transform.up * heightOffset + transform.right * sideOffset;
        float FRSensor;

        Vector3 FLsensorStartPos = FRsensorStartPos - transform.right * (2 * sideOffset);
        float FLSensor;

        Vector3 RsensorStartPos = FRsensorStartPos - transform.forward * frontOffset;
        float RSensor;

        Vector3 LsensorStartPos = FLsensorStartPos - transform.forward * frontOffset;
        float LSensor;

        if (Physics.Raycast(FsensorStartPos, transform.forward,out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(FsensorStartPos, hit.point);
            FSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            FSensor = sensorLength;
        }

            
        if (Physics.Raycast(FRsensorStartPos, Quaternion.AngleAxis(angleOffset,transform.up)*transform.forward, out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(FRsensorStartPos, hit.point);
            FRSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            FRSensor = sensorLength;
        }

            
        if (Physics.Raycast(FLsensorStartPos, Quaternion.AngleAxis(-1*angleOffset, transform.up) * transform.forward, out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(FLsensorStartPos, hit.point);
            FLSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            FLSensor = sensorLength;
        }

        if (Physics.Raycast(RsensorStartPos, transform.right, out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(RsensorStartPos, hit.point);
            RSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            RSensor = sensorLength;
        }


        if (Physics.Raycast(LsensorStartPos, -1 * transform.right, out hit, sensorLength, layerMask))
        {
            Debug.DrawLine(LsensorStartPos, hit.point);
            LSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            LSensor = sensorLength;
        }



        float[,] inputs = new float[,] { { LSensor, FLSensor, FSensor, FRSensor, RSensor } };
        //Debug.Log(inputs);
        float[,] outputs = net.forward(inputs);
        return new float[] { outputs[0,0], outputs[0,1], /*Input.GetKey(KeyCode.Space) ? 1f : */ 0f };
     
	}
    public float[,] getRawSensor()
    {
        RaycastHit hit;


        Vector3 FsensorStartPos=transform.position+ transform.forward*frontOffset+transform.up*heightOffset;
        float FSensor;

        Vector3 FRsensorStartPos = transform.position + transform.forward * frontOffset + transform.up * heightOffset + transform.right * sideOffset;
        float FRSensor;

        Vector3 FLsensorStartPos = FRsensorStartPos - transform.right * (2 * sideOffset);
        float FLSensor;

        Vector3 RsensorStartPos = FRsensorStartPos - transform.forward * frontOffset;
        float RSensor;

        Vector3 LsensorStartPos = FLsensorStartPos - transform.forward * frontOffset;
        float LSensor;

        if (Physics.Raycast(FsensorStartPos, transform.forward,out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(FsensorStartPos, hit.point);
            FSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            FSensor = sensorLength;
        }

            
        if (Physics.Raycast(FRsensorStartPos, Quaternion.AngleAxis(angleOffset,transform.up)*transform.forward, out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(FRsensorStartPos, hit.point);
            FRSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            FRSensor = sensorLength;
        }

            
        if (Physics.Raycast(FLsensorStartPos, Quaternion.AngleAxis(-1*angleOffset, transform.up) * transform.forward, out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(FLsensorStartPos, hit.point);
            FLSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            FLSensor = sensorLength;
        }

        if (Physics.Raycast(RsensorStartPos, transform.right, out hit, sensorLength,layerMask))
        {
            Debug.DrawLine(RsensorStartPos, hit.point);
            RSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            RSensor = sensorLength;
        }


        if (Physics.Raycast(LsensorStartPos, -1 * transform.right, out hit, sensorLength, layerMask))
        {
            Debug.DrawLine(LsensorStartPos, hit.point);
            LSensor = hit.distance;
            //Debug.Log("hit");
        }
        else
        {
            LSensor = sensorLength;
        }



        float[,] inputs = new float[,] { { LSensor, FLSensor, FSensor, FRSensor, RSensor } };
        return inputs;
    }
    public void setNN(NN a)
    {
        net = a;
    }
    void FixedUpdate()
    {
        
    }
    
}

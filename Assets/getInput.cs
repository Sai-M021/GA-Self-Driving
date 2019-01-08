using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getInput : MonoBehaviour {
    public string type;
   
    private float curTurn = -1;
    private float inc = 0.05f;

    [Header("Sensors")]
    public float sensorLength;
    public float frontOffset;
    public float heightOffset;
    public float sideOffset;
    public float angleOffset;
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
		if (type.Equals("test"))
        {
            return new float[] { testTurn(), testSpeed() ,1f};
        }
        else
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

            if (Physics.Raycast(FsensorStartPos, transform.forward,out hit, sensorLength))
            {
                Debug.DrawLine(FsensorStartPos, hit.point);
                FSensor = hit.distance;
                //Debug.Log("hit");
            }
            else
            {
                FSensor = sensorLength;
            }

            
            if (Physics.Raycast(FRsensorStartPos, Quaternion.AngleAxis(angleOffset,transform.up)*transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(FRsensorStartPos, hit.point);
                FRSensor = hit.distance;
                //Debug.Log("hit");
            }
            else
            {
                FRSensor = sensorLength;
            }

            
            if (Physics.Raycast(FLsensorStartPos, Quaternion.AngleAxis(-1*angleOffset, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(FLsensorStartPos, hit.point);
                FLSensor = hit.distance;
                //Debug.Log("hit");
            }
            else
            {
                FLSensor = sensorLength;
            }

            if (Physics.Raycast(RsensorStartPos, transform.right, out hit, sensorLength))
            {
                Debug.DrawLine(RsensorStartPos, hit.point);
                RSensor = hit.distance;
                //Debug.Log("hit");
            }
            else
            {
                RSensor = sensorLength;
            }


            if (Physics.Raycast(LsensorStartPos, -1*transform.right, out hit, sensorLength))
            {
                Debug.DrawLine(LsensorStartPos, hit.point);
                LSensor = hit.distance;
                //Debug.Log("hit");
            }
            else
            {
                LSensor = sensorLength;
            }



            float[] inputs = new float[] { LSensor, FLSensor, FSensor, FRSensor, RSensor };
            //Debug.Log(inputs);

            return new float[] { Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetKey(KeyCode.Space) ? 1f : 0f };
        }
	}
    float testTurn()
    {
        return curTurn;
    }
    float testSpeed()
    {
        return 1;
    }
    void FixedUpdate()
    {
        curTurn += inc;
        if (curTurn >= 1)
        {
            inc = -Mathf.Abs(inc);
        }
        else if (curTurn <= -1)
        {
            inc = Mathf.Abs(inc);
        }
        //Debug.Log(curTurn);
        
    }
    
}

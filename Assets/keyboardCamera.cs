using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardCamera : MonoBehaviour
{
    public float speed;
    public float angleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(-speed * Time.deltaTime*transform.up);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(speed * Time.deltaTime*transform.up);
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.T))
        {
            transform.Rotate(new Vector3(-angleSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.G))
        {
            transform.Rotate(new Vector3(angleSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(new Vector3(0, angleSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(new Vector3(0,-angleSpeed*Time.deltaTime, 0));
        }
    }
}

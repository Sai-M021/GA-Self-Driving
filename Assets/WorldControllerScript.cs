using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControllerScript : MonoBehaviour
{
    public GameObject car;
    // Start is called before the first frame update
    public void Start()
    {
        GameObject car1 = Instantiate(car) as GameObject;
        GameObject car2 = Instantiate(car) as GameObject;
        car1.transform.position = transform.position;
        car2.transform.position = transform.position;
        car1.GetComponent<getInput>().type = "test";
        car2.GetComponent<getInput>().type = "";
        generateCar(""); //code does not create a new car
    }
    // TODO: The below code does not work, I think it is because temp is only created locally
    // and therefore it is not created in the Start function rather only in the generateCar function
    public void generateCar(string type)
    {
        GameObject temp = Instantiate(car) as GameObject;
        temp.transform.position = transform.position;
        temp.GetComponent<getInput>().type = type;
    }

}

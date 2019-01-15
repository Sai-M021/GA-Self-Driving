using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController : MonoBehaviour
{
    public GameObject car;
    public List<GameObject> allCars;
    private GAlgorithm ga;
    public int population;
    public float mutationrate;
    public int time;
    private int timeremaining;
    private GameObject bestCar;

    public void Start()
    {
        population = 50;
        ga = new GAlgorithm(population,mutationrate,5);
        firstGeneration();
        Time.timeScale *= 2;

    }
    public GameObject generateCar(string Type)
    {
        GameObject temp = Instantiate(car) as GameObject;
        temp.transform.position = transform.position;
        temp.GetComponent<getInput>().type = Type;
        return temp;
    }
    public void firstGeneration()
    {
        timeremaining = time;
        foreach(GameObject g in allCars)
        {
            Destroy(g);
        }
        allCars.Clear();
        for(int i=0; i<population; i++)
        {
            GameObject temp = generateCar("");
            temp.GetComponent<getInput>().setNN(new NN());
            allCars.Add(temp);
        }
        InvokeRepeating("checkDone", 1, 1);
    }
    public void nextGeneration()
    {
        int partner;
        System.Random rng = new System.Random();
        timeremaining = time;
        ga.Thanos(allCars);
        foreach(GameObject g in allCars)
        {
            g.GetComponent<CarController>().reset();
        }
        List<GameObject> newCars = new List<GameObject>();
        foreach(GameObject g in allCars)
        {
            GameObject temp = Instantiate(car) as GameObject;
            partner = rng.Next(allCars.Count);
            temp.GetComponent<getInput>().setNN(ga.mutate(ga.breed(g.GetComponent<getInput>().net, allCars[partner].GetComponent<getInput>().net)));
            newCars.Add(temp);
        }
        allCars.AddRange(newCars);
        InvokeRepeating("checkDone", 1, 1);
    }
    public void checkDone()
    {
        timeremaining--;
        bool done = true;
        foreach(GameObject g in allCars)
        {
            if(!g.GetComponent<CarController>().collided)
            {
                done = false;
                break;
            }
        }
        if(done)
        {
            CancelInvoke("checkDone");
            nextGeneration();
        }
        else if(timeremaining==0)
        {
            CancelInvoke("checkDone");
            nextGeneration();
        }
        else
        {
            //bestCar.GetComponent<CarController>().setRender(false);
            //getBest(allCars).GetComponent<CarController>().setRender(true);

        }

    }
    private GameObject getBest(List<GameObject> list)
    {
        float fit = -99f;
        GameObject temp = list[0];
        foreach(GameObject g in list)
        {
            //g.GetComponent<CarController>().setRender(true);
            if(g.GetComponent<CarController>().fitness>fit)
            {
                fit = g.GetComponent<CarController>().fitness;
                temp = g;
                
            }
        }
        bestCar = temp;
        return temp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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
    private int layers;
    private int nodes;

    public void Start()
    {
        
        ga = new GAlgorithm(population,mutationrate,5);
        
        Time.timeScale *= 2;
        layers = 1;
        nodes = 5;
        firstGeneration();


    }
    public GameObject generateCar(string Type)
    {
        GameObject temp = Instantiate(car) as GameObject;
        temp.transform.position = gameObject.transform.position;
        temp.transform.rotation = gameObject.transform.rotation;
        temp.GetComponent<getInput>().type = Type;
        return temp;
    }
    public void save()
    {
        allCars.Sort( (c1, c2) => c2.GetComponent<CarController>().fitness.CompareTo(c1.GetComponent<CarController>().fitness) );
        float[][,] best = allCars[0].GetComponent<getInput>().net.getAllWeights();
        try 
        {
            String str = "";
            for (int i = 0; i < best.GetLength(0); i++)
            {
                for (int j = 0; j < best[i].GetLength(0); j++)
                {
                    str = "";
                    for(int k = 0; k < best[i].GetLength(1); k++)
                    {
                        str += " " + (best[i][j, k]).ToString();
                    }
                    File.AppendAllText(@"data.txt", str);
                }
                File.AppendAllText(@"data.txt", "\n");
            }
            
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally 
        {
            Console.WriteLine("Finally.");
        }
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
            temp.GetComponent<getInput>().setNN(new NN(layers, nodes));
            //Debug.Log(temp.GetComponent<getInput>().net.getAllWeights()[0].GetLength(0));
            allCars.Add(temp);
            temp.GetComponent<CarController>().reset(gameObject.transform.position,gameObject.transform.rotation);
        }
        InvokeRepeating("checkDone", 1, 1);
    }
    public void nextGeneration()
    {
        int partner;
        System.Random rng = new System.Random();
        timeremaining = time;
        foreach (GameObject g in allCars)
        {
            g.GetComponent<CarController>().setFitness();
        }
        

        ga.Thanos(allCars);
        
        
        List<GameObject> newCars = new List<GameObject>();
        foreach(GameObject g in allCars)
        {
            GameObject temp = Instantiate(car) as GameObject;
            partner = rng.Next(allCars.Count);
            NN a = g.GetComponent<getInput>().net;
            NN b = allCars[partner].GetComponent<getInput>().net;
            NN c = ga.mutate(ga.breed(a, b));
            temp.GetComponent<getInput>().setNN(c);
            
            newCars.Add(temp);
        }
       
        allCars.AddRange(newCars);
        foreach(GameObject g in allCars)
        {
            g.GetComponent<CarController>().reset(gameObject.transform.position, gameObject.transform.rotation);
        }
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
        if(timeremaining == 0)
        {
            done = true;
        }
        if(done)
        {
            CancelInvoke("checkDone");
            foreach(GameObject g in allCars)
            {
                g.GetComponent<CarController>().setFitness();
            }
            save();
            nextGeneration();
        }
        else
        {
            //bestCar.GetComponent<CarController>().setRender(false);
            //getBest(allCars).GetComponent<CarController>().setRender(true);

        }

    }
    public GameObject getBest(List<GameObject> list)
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

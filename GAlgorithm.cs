//GENERAL TODO:
// - Fitness
// - Ranking
// - Mutation
// - Breed
// - Systematically update the run function at the bottom
// - Complete fetchSensorData using GetComponent<T>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GAlgorithm
{
//    private int generations;
    private int size;
    private float mutationRate;
    bool tooLong = false;
    public int threshold;
    public int gensLeft;
    public GAlgorithm(int initial, float mutation, int t)
    {   
        size = initial;
        mutationRate = mutation;
        threshold = t;
    }
    public NN mutate(NN a)
    {
        float tempRate = mutationRate * (tooLong ? 4f : 1f);
        float[][,] newWeights = new float[][,] { (float[,]) a.weightsHE.Clone(), (float[,])a.weightsIH.Clone() };
        foreach (float[,] arr in newWeights )
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = UnityEngine.Random.Range(0f, 1f) > tempRate ? arr[i, j] : UnityEngine.Random.Range(-5f, 5f);
                }
            }
        }
        if (tooLong)
        {
            gensLeft -= 1;
            if(gensLeft==0)
            {
                tooLong = false;
            }
        }
        NN ret = new NN(newWeights[1],newWeights[0]);
        return ret;
        
    }
    public NN breed(NN a, NN b)
    {
        float[][,] newWeights = new float[][,] { (float[,])a.weightsHE.Clone(), (float[,])a.weightsIH.Clone() };
       
        for (int i = 0; i < newWeights[0].GetLength(0); i++)
        {
            for (int j = 0; j < newWeights[0].GetLength(1); j++)
            {
            newWeights[0][i, j] = UnityEngine.Random.Range(0f, 1f) > 0.5 ? a.getHEWeights()[i, j] : b.getHEWeights()[i, j];
            }
        }
        for (int i = 0; i < newWeights[1].GetLength(0); i++)
        {
            for (int j = 0; j < newWeights[1].GetLength(1); j++)
            {
                newWeights[1][i, j] = UnityEngine.Random.Range(0f, 1f) > 0.5 ? a.getIHWeights()[i, j] : b.getIHWeights()[i, j];
            }
        }

        NN ret = new NN(newWeights[1], newWeights[0]);
        return ret;
    }
    public void Thanos(List<GameObject> allCars)
    {
        allCars.Sort( (c1, c2) => c2.GetComponent<CarController>().fitness.CompareTo(c1.GetComponent<CarController>().fitness) );
        foreach(GameObject g in allCars.GetRange(allCars.Count / 2, allCars.Count / 2))
        {
            UnityEngine.Object.Destroy(g);
        }
        allCars.RemoveRange(allCars.Count / 2, allCars.Count / 2);

        
    }
    public string timestamp()
    {
        return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": ";
    }
    public void increaseMutation()
    {
        tooLong = true;
        gensLeft = threshold;

    }
}

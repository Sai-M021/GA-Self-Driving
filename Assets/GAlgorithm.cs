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
        //Break
        float[][,] oldWeightsA = a.getAllWeights();
        float[][,] newWeights = new float[oldWeightsA.GetLength(0)][,];
        for (int ind = 0; ind < oldWeightsA.GetLength(0); ind++)
        {
            newWeights[ind] = new float[oldWeightsA[ind].GetLength(0), oldWeightsA[ind].GetLength(1)];
        }
        for(int ind=0; ind<oldWeightsA.GetLength(0); ind++ )
        {
            for (int i = 0; i < oldWeightsA[ind].GetLength(0); i++)
            {
                for (int j = 0; j < oldWeightsA[ind].GetLength(1); j++)
                {
                    newWeights[ind][i, j] = UnityEngine.Random.Range(0f, 1f) > tempRate ? oldWeightsA[ind][i, j] : UnityEngine.Random.Range(-5f, 5f);
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
        NN ret = new NN(newWeights, a.hiddenLayerCount, a.hiddenLayerSize);
        return ret;

    }
    public NN breed(NN a, NN b)
    {
        float[][,] oldWeightsA = a.getAllWeights();
        float[][,] oldWeightsB = b.getAllWeights();
        float[][,] newWeights = new float[oldWeightsA.GetLength(0)][,];
        for(int ind=0; ind<oldWeightsA.GetLength(0);ind++)
        {
            newWeights[ind] = new float[oldWeightsA[ind].GetLength(0), oldWeightsA[ind].GetLength(1)];
        }
        

        for (int i = 0; i < newWeights.GetLength(0); i++)
        {
            for (int j = 0; j < newWeights[i].GetLength(0); j++)
            {
                for (int k = 0; k < newWeights[i].GetLength(1); k++)
                {
                    newWeights[i][j, k] =
                    UnityEngine.Random.Range(0f, 1f) > 0.5 ? oldWeightsA[i][j, k] : oldWeightsB[i][j, k];
                }
            }
        }
        
        NN ret = new NN(newWeights, a.hiddenLayerCount, a.hiddenLayerSize);
        return ret;
    }
    public void Thanos(List<GameObject> allCars)
    {
        allCars.Sort( (c1, c2) => c2.GetComponent<CarController>().fitness.CompareTo(c1.GetComponent<CarController>().fitness) );
        foreach(GameObject g in allCars)
        {
            Debug.Log(g.GetComponent<CarController>().fitness);
           
        }
        foreach (GameObject g in allCars.GetRange(allCars.Count/2, allCars.Count/2))
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

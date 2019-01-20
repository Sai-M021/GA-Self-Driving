using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NN
{
    public int hiddenLayerCount;
    public int hiddenLayerSize;
    private int inputSize = 5;
    //public float[,] weightsIH;
    //public float[,] weightsHE;
    private float[][,] weights;
    private int outputSize = 2;
    public NN(int hiddenLayers, int hiddenNodes)
    {
        hiddenLayerCount = hiddenLayers;
        hiddenLayerSize = hiddenNodes;
        weights = new float[hiddenLayerCount + 1][,];
        weights[0] = new float[inputSize, hiddenLayerSize];
        for(int i = 1; i < hiddenLayerCount; i++)
        {
            weights[i] = new float[hiddenLayerSize, hiddenLayerSize];
        }
        
        weights[hiddenLayerCount] = new float[hiddenLayerSize, outputSize];

        weights[0] = randomizeWeights(weights[0], inputSize, hiddenLayerSize);
        for(int i = 1; i < hiddenLayerCount; i++)
        {
            weights[i] = randomizeWeights(weights[i], hiddenLayerSize, hiddenLayerSize);
        }
        weights[hiddenLayerCount] =
        randomizeWeights(weights[hiddenLayerCount], hiddenLayerSize, outputSize);
    }
    public NN(float[][,] w, int hiddenLayers, int hiddenNodes)
    {
        hiddenLayerCount = hiddenLayers;
        hiddenLayerSize = hiddenNodes;

        weights = w;
    }
    public float[,] randomizeWeights(float[,] matrix, int rows, int cols)
    {
        for(int i = 0; i < rows; i++)
            for(int j = 0; j < cols; j++)
                matrix[i, j] = UnityEngine.Random.Range(-5.0f, 5.0f);
        return matrix;
    }
    public float[,] forward(float[,] input)
    {
        float[,] Z;
        Z = activation(multiply(input, weights[0]));
        for(int i = 1; i < hiddenLayerCount + 1; i++)
        {
            Z = activation(multiply(Z, weights[i]));
        }
        return Z;
    }
    public static float[,] multiply(float[,] one, float[,] two)
    {
        float[,] result = new float[one.GetLength(0), two.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < one.GetLength(1); k++)
                {
                    result[i, j] += one[i, k] * two[k, j];
                }
            }
        }
        return result;
    }
    public float[,] activation(float[,] matrix)
    {
        for(int i = 0; i < matrix.GetLength(0); i++)
            for(int j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = (float) Math.Tanh(matrix[i, j]);
        return matrix;
    }
    public float[][,] getAllWeights()
    {
        return weights;
    }
    /*public float[,] getIHWeights()
    {
        return weightsIH;
    }
    public float[,] getHEWeights()
    {
        return weightsHE;
    }*/
    public void setAllWeights(float[][,] w)
    {
        weights = w;
    }
    /*public void setIHWeights(float[,] w)
    {
        weightsIH = w;
    }
    public void setHEWeights(float[,] w)
    {
        weightsHE = w;
    }*/

}

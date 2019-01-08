using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NN
{
    private int hiddenLayerCount;
    private int hiddenLayerSize;
    private int inputSize = 5;
    private float[,] weightsIH;
    private float[,] weightsHE;
    private int outputSize = 2;
    public NN(int hiddenLayers, int hiddenNodes)
    {
        hiddenLayerCount = hiddenLayers;
        hiddenLayerSize = hiddenNodes;
        weightsIH = new float[inputSize, hiddenSize];
        weightsHE = new float[hiddenSize, outputSize];
        weightsIH = randomizeWeights(weightsIH, inputSize, hiddenSize);
        weightsHE = randomizeWeights(weightsHE, hiddenSize, outputSize);
    }
    public NN()
    {
        hiddenCount = 1;
        hiddenSize = 5;
        weightsIH = new float[inputSize, hiddenSize];
        weightsHE = new float[hiddenSize, outputSize];
        weightsIH = randomizeWeights(weightsIH, inputSize, hiddenSize);
        weightsHE = randomizeWeights(weightsHE, hiddenSize, outputSize);
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
        float[,] Z1 = activation(multiply(input, weightsIH));
        float[,] Z2 = activation(multiply(Z1, weightsHE));
        return Z2;
    }
    public float[,] multiply(float[,] one, float[,] two)
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
    /*public float[,,] getAllWeights()
    {
        return [weightsIH, weightsHe];
    }*/
    public float[,] getIHWeights()
    {
        return weightsIH;
    }
    public float[,] getHEWeights()
    {
        return weightsHE;
    }
    /*public void setAllWeights(float[,,] w)
    {
        weightsIH = w[];
        weightsHE = w[]
    }*/
    public void setIHWeights(float[,] w)
    {
        weightsIH = w;
    }
    public void setHEWeights(float[,] w)
    {
        weightsHE = w;
    }

}

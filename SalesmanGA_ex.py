#Sai Manchikalapati
# NOVEMBER 4, 2018 6 pm
# Genetic Algorithm solution to the TSP, input values are:
# - Amount of Cities
# - Amount of Reproducing parents
# - Amount of Members in the Tournament (see selection function)
# - Mutation Rate (randomely determined)
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import random
#Initial Population Size must be over 10 cities
class City:
    def __init__(self, xVal, yVal):
        self.x = xVal
        self.y = yVal

    def distance(self, city):
        return np.sqrt((abs(self.x - city.x) ** 2) + (abs(self.y - city.y) ** 2))

    def represent(self):
        print("(" + str(self.x) + ", " + str(self.y) + ")")

def f(route):
    distance = 0.0
    for i in range(0, len(route)):
        if i + 1 < len(route):
            distance = distance + (route[i]).distance(route[i + 1])
        else:
            distance = distance + (route[i]).distance(route[0])
    return 1 / distance

def intial(cities, size):
    population = list()
    for i in range(0, size):
        population.append(random.sample(cities, len(cities)))
    return population

def fitness(population):
    ret = dict()
    for i in range(0, len(population)):
        ret[i] : f(population[i])
    return ret

def select(fit, size, k):
    individuals = list()
    for i in fit:
        individuals.append((fit[i], i))
    parents = list()
    k_list = list()
    while len(parents) < size:
        random.shuffle(individuals)
        k_list = individuals[0:k + 1]
        parents.append(max(k_list)[1])
    return parents

#swap mutation
def mutate(population, rate):
    for i in population:
        if random.random() < rate:
            index = int(random.random * len(i))
            temp = i[index]
            i[index] = i[0]
            i[0] = temp
    return population

#TODO: breed the parents and return the population
def breed(parents):
    return

def generation(current, elite, k, mutation):
    fit = fitness(current)
    parents = select(fit, elite, k)
    children = breed(parents)
    next_gen = mutate(children, mutation)
    return next_gen

#TODO: generate n amount of cities each with a random weight
def generateCities(n):
    return

def main(void):
    count = int(input("Amount of Cities: "))
    elite = int(input("Amount of Parents: "))
    k = int(input("Tournament Member Count: "))
    mutation = float(input("Mutation Rate: "))
    cities = generateCities(count)
    pop = initial(cities, 1000)
    for i in range(0, 100):
        generation(pop, elite, k, mutation)
    print("COMPLETED")

if __name__ == "__main__":
    main()

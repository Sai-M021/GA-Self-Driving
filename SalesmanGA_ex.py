#Sai Manchikalapati
# NOVEMBER 4, 2018 6 pm
# Genetic Algorithm solution to the TSP, input values are:
# - Amount of Cities
# - Amount of Reproducing parents
# - Amount of Members in the Tournament (see selection function)
# - Mutation Rate (randomely determined)
# Note: Number of Cities needs to be considerably large (10 - 25)
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import random
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

def initial(cities, size):
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
        k_list = individuals[0 : k]
        parents.append(max(k_list)[1])
    return parents

def mutate(population, rate):
    for i in population:
        if random.random() < rate:
            index = int(random.random * len(i))
            temp = i[index]
            i[index] = i[0]
            i[0] = temp
    return population

def breed(parent1, parent2):
    child = []
    childP1 = []
    childP2 = []

    geneA = int(random.random() * len(parent1))
    geneB = int(random.random() * len(parent1))

    startGene = min(geneA, geneB)
    endGene = max(geneA, geneB)

    for i in range(startGene, endGene):
        childP1.append(parent1[i])

    childP2 = [item for item in parent2 if item not in childP1]

    child = childP1 + childP2
    return child

def breedPopulation(parents, elite):
    children = []
    length = len(parents) - elite
    pool = random.sample(parents, len(parents))

    for i in range(0, elite):
        children.append(parents[i])
    for i in range(0, length):
        children.append(breed(parents[i], parents[len(matingpool) - i - 1]))
    return children

def generation(current, elite, k, mutation):
    fit = fitness(current)
    parents = select(fit, elite, k)
    children = breedPopulation(parents, elite)
    next_gen = mutate(children, mutation)
    return next_gen

#bounds x: -100, 100 and y:-100, 100
def generateCities(n):
    cities = list()
    for i in range(0, n):
        cities.append(City(int(random.random() * 100), int(random.random() * 100)))
    return cities


def main():
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

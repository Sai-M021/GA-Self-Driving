# Training Autonomous Vehicles using a Genetic Algorithm

Collaborators: Sai Manchikalapati, Aneesh Boreda, Ritvik Jayakumar

## Introduction

Self driving vehicles (SDVs) will become increasingly important in the future as the global population increases and roads become densely populated. Approximately 1.25 million people die of car crashes each year with an additional 50 million disabled or injured. With the use of automation to control cars, expert predict that these fatalities can be significantly reduced with the removal of human error. SDVs are often controlled using a neural network. The complexity of driving and the impossibility of hardcoding threshold values ushers the need to use these tactics. Neural networks learn weight values using large training datasets, which is an issue with driving due to the complex nature of navigation and the lack of abundant data

## Implementation

A  genetic algorithm (GA) was implemented in order to train the neural network. The specifics of the functionality of the GA is illustrated in Figure 1 which summarizes the actions which are conducted. The genetic algorithm produces an array of organisms which make up the population, which is  fed in by the user and is defaulted to 50. It then runs a digitized process of evolution for X  generations which is also determined by the user. Specifically, it simulates, ranks, breeds, and mutates the population until the user decides that the network has been trained sufficiently. The better an organism drives, the better its fitness is, making it more likely to breed and pass on favorable traits. Figure 3 details the specific mechanisms behind the GA.
The network that was implemented was a sequential neural network. Structurally, the neural network had two hidden layers each of size five, an input layer of size seven, and an output layer of size four. Figure 2 is an exact depiction of our neural network

![Figure 1](figure1.png)
![Figure 2](figure2.png)
![Figure 3](figure3.png)




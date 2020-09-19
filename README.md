# Training Autonomous Vehicles using a Genetic Algorithm

Collaborators: Sai Manchikalapati, Aneesh Boreda, Ritvik Jayakumar

## Introduction

Self driving vehicles (SDVs) will become increasingly important in the future as the global population increases and roads become densely populated. Approximately 1.25 million people die of car crashes each year with an additional 50 million disabled or injured. With the use of automation to control cars, expert predict that these fatalities can be significantly reduced with the removal of human error. SDVs are often controlled using a neural network. The complexity of driving and the impossibility of hardcoding threshold values ushers the need to use these tactics. Neural networks learn weight values using large training datasets, which is an issue with driving due to the complex nature of navigation and the lack of abundant data

## Implementation

A  genetic algorithm (GA) was implemented in order to train the neural network. The specifics of the functionality of the GA is illustrated in Figure 1 which summarizes the actions which are conducted. The genetic algorithm produces an array of organisms which make up the population, which is  fed in by the user and is defaulted to 50. It then runs a digitized process of evolution for X  generations which is also determined by the user. Specifically, it simulates, ranks, breeds, and mutates the population until the user decides that the network has been trained sufficiently. The better an organism drives, the better its fitness is, making it more likely to breed and pass on favorable traits. Figure 3 details the specific mechanisms behind the GA.
The network that was implemented was a sequential neural network. Structurally, the neural network had two hidden layers each of size five, an input layer of size seven, and an output layer of size four. Figure 2 is an exact depiction of our neural network


![Figure1](https://github.com/Sai-M021/GA-Self-Driving-Vehicle/blob/master/Figure1.png?raw=true)
![Figure2](https://github.com/Sai-M021/GA-Self-Driving-Vehicle/blob/master/Figure2.png?raw=true)
![Figure3](https://github.com/Sai-M021/GA-Self-Driving-Vehicle/blob/master/Figure3.png?raw=true)

## Results

Extensive statistics and data were collected on the effectiveness of our neural network as well as its efficiency and overall learning rate. Overall, the SDVs were able to be trained on seperate courses teaching it separate aspects of driving and were able to integrate those independent aspects when being tested in a relatively hard course. An important statistic that was collected was the completion of the courses as a function of the number of generations that ran. Specifically, this allows us to calculate the instantaneous learning rate simply by differentiating the the functions displayed. The appropriate approximation function and the corresponding derivative function are listed and plotted below. Figure 4 is the graph of percent completion of training courses against the amount of generations the GA trained for. 

![Figure4](https://github.com/Sai-M021/GA-Self-Driving-Vehicle/blob/master/Figure4.png?raw=true)

Another important evaluation of the genetic algorithm involves changing the population size and determining its effect on the generations required until completion. A graph of this provides crucial information on how to optimize the algorithm’s parameters such that we can teach the SDVs using the least time and computational power. Figure 5 is the relationship between the population count and the generations required until mastery of the training courses was acquired.

![Figure5](https://github.com/Sai-M021/GA-Self-Driving-Vehicle/blob/master/Figure5.png?raw=true)

## Conclusion

After training on multiple courses, each of which had disjoint aspects of driving, the SDVs were able to integrate what they learned while retaining important knowledge such as turning to successfully and completely navigate the testing course.



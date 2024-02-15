# Multiagent Queue Simulation

### Quick Disclaimer before the start:
**This project was created in my free time and does not represent my full potential.** I wanted to experiment with **multi-agent simulation** and **Unity engine** to create a realistic scenario of people passing through ticket queues. This project is not a complete representation of my programming skills, but rather a demonstration of my **creativity** and **curiosity**. I hope that you will appreciate this project as an example of my **passion** for learning new things and solving problems.


The purpose of this simulation is to gain insight into how people behave when passing through ticket queues to enter a specific facility. In the simulation, I also aim to understand how increasing the number of available ticket queues affects the throughput of people entering the location. Additionally, I consider both traditional and self-service ticket queues. The number and type of ticket queues affect the speed of people entering the facility. As a result, the simulation will help to better understand how to optimize the management of ticket queues to increase the efficiency and comfort of people entering the facility.

As a simulation environment, I used the Unity engine for creating computer games, and the code was written in C#.

## Description of the implemented multi-agent model

Passing through a standard, traditional ticket queue at a facility (the facility is here an amusement park/zoo, concert or larger grocery store) usually takes from 4 to 8 minutes. Self-service ticket queues operate 30% faster. Of course, the individual waiting time depends on the simplicity of the self-service system and the skills of the person. In my simulation, I assume that the system is equally complicated for everyone. Each person receives their pseudorandom time, which will be added to the total waiting time in the queue.

In the multi-agent simulation, I set several constant parameters. One of them is the percentage of people using self-service ticket queues, which always averages from 42% to 48%. People aged 18 to 34 are the main group using these ticket queues, while people in the age range of 55 to 65 decide to use them in 40% of cases. Due to the diversity, I assume that 40% of people will decide to use self-service ticket queues, assuming that the queue there will be shorter than at traditional ticket queues. This parameter will be set at the beginning to 40% with the possibility of changing it, as this will show how encouraging people (increasing this %) to use these ticket queues can affect the speed of people flow.
Summarizing the data I have, they look as follows:
- Number of classic ticket queues, adjustable parameter,
- Number of self-service ticket queues, adjustable parameter,
- Number of guests, adjustable parameter,
- Service time at the classic ticket queue, constant pseudorandom parameter,
- Service time at the self-service ticket queue, constant pseudorandom parameter,
- Up to 40% of the number of guests will use the self-service ticket queue if their queue is shorter, constant/adjustable parameter,
- The time of using the self-service ticket queue will be shortened by 30%, constant pseudorandom parameter.
In order to create a multi-agent simulation, the ticket queues, both classic and self-service, are agents cooperating with each other. These ticket queues achieve a common goal of letting people into the facility. People standing in line, trying to get to the facility are a distributed element. This element is influenced by various factors such as the number of people in front of them in the queue, the service time of a given person at the classic or self-service ticket queue, the length of the queue to the self-service ticket queue.
The project is possible to be continuously expanded, in the simulation you can include such factors as e.g. service time for different age groups, costs of service of the classic and self-service ticket queue, customer satisfaction, different types of tickets (for couples, groups, children) or even weather conditions.

Interface with parameters:
![image](https://github.com/Mileek/Multiagent-Queue-Simulation/assets/95537833/dbbb4ddf-4d57-4fd3-95a3-2e7c42368c52)

Real live example:
![image](https://github.com/Mileek/Multiagent-Queue-Simulation/assets/95537833/1a1ed5a6-fcdb-4a4a-9ec9-831f39b93d83)

Low chance for self service:
![image](https://github.com/Mileek/Multiagent-Queue-Simulation/assets/95537833/2efb4acd-cc7b-4a26-a4b1-b28fa6b27b65)


Best Example:
![image](https://github.com/Mileek/Multiagent-Queue-Simulation/assets/95537833/89ac25f1-c523-45fe-9f83-6970437c1880)




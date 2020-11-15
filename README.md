# Reinforcement-Learning-Stock-Trading

An implementation of Reinforcement Learning for trading using DQN.

To Know more read the medium Article accomapined - https://medium.com/@abhishek_maheshwarappa/reinforcement-learning-for-stock-trading-d4e487b1b280

# **Actions**

Stock trading, the possible actions are

1.  Buy
2.  Sell
3.  Hold

# **Reward**

Current value amount compared with the previous step

How to use the Repo

Clone the Repository

One can use environment.yml file or requirement.txt to get the neccessaary dependencies

**To train**
```
python train.py
```
Then provide stock name on which agent has to trained and then the window size and the number of episodes

**To Evaluate**
```
python evaluate.py
```

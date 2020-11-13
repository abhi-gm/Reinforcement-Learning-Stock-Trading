'''
Author - Abhishek MAheshwarappa & Jiaxin Tong

Training the agent episodes are read from the 
commnad line

window - read from the command line
stock name - will choose that stock from data directory


'''


from agent.agent import Agent
from functions import *
import sys
import os

path = 'data/'
files = os.listdir(path)

print("\n\n")
print("***************************************************************")
print("------------------------The Stock Data-------------------------")
print("\n")

for f in files:
	print("\t",f)
print('\n\n')
print()



stock_name = input('Which Stock to train the agent? ')
# if stock_name

window_size = int(input("Input your Window size "))

episode_count = int(input("The number of episode you want train your agent "))


# stock_name, window_size, episode_count = sys.argv[1], int(
#     sys.argv[2]), int(sys.argv[3])

agent = Agent(window_size)
data = getStockDataVec(stock_name)
l = len(data) - 1
batch_size = 32

for e in range(episode_count + 1):
    print("Episode " + str(e) + "/" + str(episode_count))
    state = getState(data, 0, window_size + 1)

    total_profit = 0
    agent.inventory = []

    for t in range(l):
        action = agent.act(state)

        # sit
        next_state = getState(data, t + 1, window_size + 1)
        reward = 0

        if action == 1:  # buy
            agent.inventory.append(data[t])
            print("Buy: " + formatPrice(data[t]))

        elif action == 2 and len(agent.inventory) > 0:  # sell
            bought_price = agent.inventory.pop(0)
            reward = max(data[t] - bought_price, 0)
            total_profit += data[t] - bought_price
            print("Sell: " + formatPrice(data[t]) + " | Profit: " + formatPrice(data[t] - bought_price))

        done = True if t == l - 1 else False
        agent.memory.append((state, action, reward, next_state, done))
        state = next_state

        if done:
            print("--------------------------------")
            print("Total Profit: " + formatPrice(total_profit))
            print("--------------------------------")

        if len(agent.memory) > batch_size:
            agent.expReplay(batch_size)

    if e % 10 == 0:
        agent.model.save("models/model_ep" + str(e))

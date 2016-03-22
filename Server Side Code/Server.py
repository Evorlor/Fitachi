from flask import Flask
import time
import json
app = Flask(__name__)

@app.route('/start')
def start():
    return "Launching server..."

class Player:
    hitPoints = 0

players = {}
turn = ""

@app.route('/create_player/<player>')
def createPlayer(player):
    global players
    playerJson = json.loads(player)
    token = playerJson['token']
    players[token] = Player()
    players[token].hitPoints = int(playerJson['hitPoints'])

@app.route('/find_match')
def findMatch():
    global players
    if len(players) > 1:
        for key in players:
            turn = key
            return "true"
    return "false"

@app.route('/attack/<player>')
def attack(player):
    global players
    global turn
    playerJson = json.loads(player)
    token = playerJson['token']
    if token != turn:
        return token + " is not turn. its: " + turn
    for key in players:
        if key != turn:
            turn = key
            break
    players[turn].hitPoints -= 1
    return players[turn].hitPoints

if __name__ == "__main__":
    app.debug = True
    app.run(host='0.0.0.0')

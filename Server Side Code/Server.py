from flask import Flask
import time
import json
app = Flask(__name__)

@app.route('/start')
def start():
    return "Launching server..."

class Player:
    hitPoints = 0
    attackPower = 0

players = {}
turn = ""

@app.route('/get_player/<player>')
def getPlayer(player):
    global players
    playerJson = json.loads(player)
    token = playerJson['token']
    return getPlayerJson(token)

@app.route('/create_player/<player>')
def createPlayer(player):
    global players
    playerJson = json.loads(player)
    token = playerJson['token']
    players[token] = Player()
    players[token].hitPoints = int(playerJson['hitPoints'])
    players[token].attackPower = int(playerJson['attackPower'])

@app.route('/find_match')
def findMatch():
    global players
    global turn
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
    attackPower = int(playerJson['attackPower'])
    if token == turn:
        for key in players:
            if key != turn:
                turn = key
                break
        players[turn].hitPoints -= attackPower
    return getPlayerJson(turn)

def getPlayerJson(token):
    return '{"token":"' + token + '","hitPoints":' + str(players[token].hitPoints) + ',"attackPower":' + str(players[token].attackPower) + '}'
    
    json = '{'
    json += '"hitPoints": "' + str(players[token].hitPoints) + '",'
    json += '"attackPower": "' + str(players[token].attackPower) + '"'
    json += '}'
    return json

if __name__ == "__main__":
    app.debug = True
    app.run(host='0.0.0.0')

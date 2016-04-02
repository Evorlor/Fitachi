from flask import Flask
import time
import json
import random
app = Flask(__name__)

class Player:
    id = None
    hitPoints = 0
    attackPower = 0

class Match:
    id = 0
    player0 = None
    player1 = None
    turn = None

playersWaiting = []
matchesReady = {}

matchesWaiting = []
matches = []
matchCount = 0

#Default entry point for server
@app.route('/start')
def start():
    return "Launching server..."

#Finds the player a match, or enqueues them if none are available
@app.route('/find_match/<player>')
def findMatch(player):
    global playersWaiting
    searchingPlayer = createPlayerFromJson(player)
    if searchingPlayer not in playersWaiting:
        playersWaiting.append(searchingPlayer)
    return getMatchStatus(player)

#Checks if the match is ready to begin
@app.route('/get_match_status/<player>')
def getMatchStatus(player):
    global playersWaiting
    global matchCount
    searchingPlayer = createPlayerFromJson(player)
    if searchingPlayer.id in matchesReady:
        matchReady = matchesReady[searchingPlayer.id]
        for playerWaiting in playersWaiting:
            if playerWaiting.id == searchingPlayer.id:
                playersWaiting.remove(playerWaiting)
        del matchesReady[searchingPlayer.id]
        return getMatchJson(matchReady)
    for playerWaiting in playersWaiting:
        if playerWaiting.id == searchingPlayer.id or isMatched(playerWaiting, searchingPlayer):
            continue
        matchCount += 1
        match = Match()
        match.id = matchCount
        match.player0 = playerWaiting
        match.player1 = searchingPlayer
        match.turn = playerWaiting if random.choice([True, False]) else searchingPlayer
        matches.append(match)
        matchesReady[playerWaiting.id] = match
        for playerWaiting in playersWaiting:
            if playerWaiting.id == searchingPlayer.id:
                playersWaiting.remove(playerWaiting)
        return getMatchJson(match)
    invalidMatch = createInvalidMatch()
    print invalidMatch.id
    return getMatchJson(invalidMatch)

#Attacks for the player in the specified match
@app.route('/attack/<player>')
def attack(player):
    #global matches
    #attackingPlayer = createPlayer(player)
    #playerJson = json.loads(player)
    #matchNumber = int(playerJson['matchNumber'])
    #for match in matches:
        #if match.number == matchNumber:
            #if attackingPlayer == match.player0:
                #match.player1.hitPoints -= match.player0.attackPower
            #elif attackingPlayer == match.player1:
                #match.player0.hitPoints -= match.player1.attackPower
    return ""

#checks if two players are currently in a match
def isMatched(player0, player1):
    global matches
    for match in matches:
        if match.player0.id == player0.id and match.player1.id == player1.id or match.player0.id == player1.id and match.player1.id == player0.id:
            return True
    return False

#creates an invalid match
def createInvalidMatch():
    match = Match()
    invalidPlayer = createInvalidPlayer()
    match.player0 = invalidPlayer
    match.player1 = invalidPlayer
    match.turn = invalidPlayer
    return match

#creates an invalid player
def createInvalidPlayer():
    player = Player()
    player.id = ""
    return player

#converts a match to json
def getMatchJson(match):
    matchData = {
        'id':match.id,
        'player0':
            {
            'id':match.player0.id,
            'hitPoints':match.player0.hitPoints,
            'attackPower':match.player0.attackPower,
            },
        'player1':
            {
            'id':match.player1.id,
            'hitPoints':match.player1.hitPoints,
            'attackPower':match.player1.attackPower,
            },
        'turn':
            {
            'id':match.turn.id,
            'hitPoints':match.turn.hitPoints,
            'attackPower':match.turn.attackPower,
            },
        }
    matchJson = json.dumps(matchData)
    return matchJson

#takes the json for a player and creates a player from it
def createPlayerFromJson(player):
    playerJson = json.loads(player)
    newPlayer = Player()
    newPlayer.id = playerJson['id']
    newPlayer.hitPoints = int(playerJson['hitPoints'])
    newPlayer.attackPower = int(playerJson['attackPower'])
    return newPlayer

if __name__ == "__main__":
    app.debug = True
    app.run(host='0.0.0.0')

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
    for playerWaiting in playersWaiting:
        if playerWaiting.id == searchingPlayer.id or isMatched(playerWaiting, searchingPlayer):
            continue
        matchCount += 1
        match = Match()
        match.id = matchCount
        match.player0 = playerWaiting
        match.player1 = searchingPlayer
        match.turn = match.player0 if random.choice([True, False]) else match.player1
        return getMatchJson(match)
    invalidMatch = createInvalidMatch()
    print invalidMatch.id
    return getMatchJson(invalidMatch)

#Attacks for the player in the specified match
@app.route('/attack/<player>')
def attack(player):
    global matches
    attackingPlayer = createPlayer(player)
    playerJson = json.loads(player)
    matchNumber = int(playerJson['matchNumber'])
    for match in matches:
        if match.number == matchNumber:
            if attackingPlayer == match.player0:
                match.player1.hitPoints -= match.player0.attackPower
            elif attackingPlayer == match.player1:
                match.player0.hitPoints -= match.player1.attackPower

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
    'id': str(match.id),
    'player0': getPlayerJson(match.player0),
    'player1': getPlayerJson(match.player1),
    'turn': getPlayerJson(match.turn),
    }
    matchJson = json.dumps(matchData)
    return matchJson

#converts a player to json
def getPlayerJson(player):
    playerData = {
    'id': player.id,
    'hitPoints': str(player.hitPoints),
    'attackPower': str(player.attackPower),
    }
    playerJson = json.dumps(playerData)
    return playerJson

#starts a match between two players
def startMatch(player):
    global matchesWaiting
    global matchCount
    match = Match()
    match.number = matchCount
    matchCount += 1
    match.player0 = player
    match.turn = player
    matchesWaiting.append(match)
    return match

#takes the json for a player and creates a player from it
def createPlayerFromJson(player):
    playerJson = json.loads(player)
    newPlayer = Player()
    newPlayer.id = playerJson['id']
    newPlayer.hitPoints = int(playerJson['hitPoints'])
    newPlayer.attackPower = int(playerJson['attackPower'])
    return newPlayer



#@app.route('/get_player/<player>')
#def getPlayer(player):
#    global players
#    playerJson = json.loads(player)
#    token = playerJson['token']
#    return getPlayerJson(token)

#@app.route('/create_player/<player>')
#def createPlayer(player):
    #global players
    #playerJson = json.loads(player)
    #token = playerJson['token']
    #players[token] = Player()
    #players[token].hitPoints = int(playerJson['hitPoints'])
    #players[token].attackPower = int(playerJson['attackPower'])

#def initializeMatch(token0, token1):
    #matches.append((token, potentialToken))

#def isMatched(token0, token1):
    #for match in matches:
    #    if token0 in match and token1 in match:
    #        return true
    #return false

#@app.route('/attack/<player>')
#def attack(player):
    #global players
    #global turn
    #playerJson = json.loads(player)
    #token = playerJson['token']
    #attackPower = int(playerJson['attackPower'])
    #if token == turn:
    #    for key in players:
    #        if key != turn:
    #            turn = key
    #            break
    #    players[turn].hitPoints -= attackPower
    #return getPlayerJson(turn)

#def getPlayerJson(token):
    #return '{"token":"' + token + '","hitPoints":' + str(players[token].hitPoints) + ',"attackPower":' + str(players[token].attackPower) + '}'
    
    #json = '{'
    #json += '"hitPoints": "' + str(players[token].hitPoints) + '",'
    #json += '"attackPower": "' + str(players[token].attackPower) + '"'
    #json += '}'
    #return json

if __name__ == "__main__":
    app.debug = True
    app.run(host='0.0.0.0')

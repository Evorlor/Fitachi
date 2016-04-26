from flask import Flask, request
import time
import json
import random
app = Flask(__name__)

class PlayerData:
    dairy = 0
    protein = 0
    grain = 0
    vegetable = 0
    fruit = 0
    sweets = 0

class Player:
    id = None
    playerdata = None

class Match:
    id = 0
    player0 = None
    player1 = None

playerWaiting = None
matchesReady = None
matchCount = 0

#Default entry point for server
@app.route('/start')
def start():
    return "Launching server..."

#Finds the player a match, or enqueues them if none are available
@app.route('/find_match', methods=['POST'])
def findMatch():
    global playerWaiting
    postData = request.form['post_data']
    print(postData)
    searchingPlayer = createPlayerFromJson(postData)
    if playerWaiting is None:
        playerWaiting = searchingPlayer
    return getMatchStatus(postData)

#Checks if the match is ready to begin
@app.route('/get_match_status', methods=['POST'])
def getMatchStatus():
    postData = request.form['post_data']
    print(postData)
    return getMatchStatus(postData)


def getMatchStatus(player):
    global playerWaiting
    global matchCount
    global matchesReady
    searchingPlayer = createPlayerFromJson(player)
    if playerWaiting is not None:
        if playerWaiting.id != searchingPlayer.id:
            matchCount += 1
            match = Match()
            match.id = matchCount
            match.player0 = playerWaiting
            match.player1 = searchingPlayer
            matchesReady = match
            playerWaiting = None
            return getMatchJson(match)
    if matchesReady is not None:
        playerWaiting = None
        matchesTemp = matchesReady
        matchesReady = None
        return getMatchJson(matchesTemp)
    invalidMatch = createInvalidMatch()
    print invalidMatch.id
    return getMatchJson(invalidMatch)


#creates an invalid match
def createInvalidMatch():
    match = Match()
    invalidPlayer = createInvalidPlayer()
    match.player0 = invalidPlayer
    match.player1 = invalidPlayer
    return match

#creates an invalid player
def createInvalidPlayer():
    player = Player()
    player.id = ""
    player.playerdata = PlayerData()
    return player

#converts a match to json
def getMatchJson(match):
    matchData = {
        'id':match.id,
        'player0':
            {
            'id':match.player0.id,
            'playerdata':
                {
                    'dairy':match.player0.playerdata.dairy,
                    'protein':match.player0.playerdata.protein,
                    'grain':match.player0.playerdata.grain,
                    'vegetable':match.player0.playerdata.vegetable,
                    'fruit':match.player0.playerdata.fruit,
                    'sweets':match.player0.playerdata.sweets,
                }
            },
        'player1':
            {
            'id':match.player1.id,
            'playerdata':
                {
                    'dairy':match.player1.playerdata.dairy,
                    'protein':match.player1.playerdata.protein,
                    'grain':match.player1.playerdata.grain,
                    'vegetable':match.player1.playerdata.vegetable,
                    'fruit':match.player1.playerdata.fruit,
                    'sweets':match.player1.playerdata.sweets,
                }
            },
        }
    matchJson = json.dumps(matchData)
    return matchJson

#takes the json for a match and creates a match from it
def createMatchFromJson(match):
    matchJson = json.loads(match)
    newMatch = Match()
    newMatch.id = matchJson['id']
    player0Json = json.dumps(matchJson['player0'])
    player1Json = json.dumps(matchJson['player1'])
    newMatch.player0 = createPlayerFromJson(player0Json)
    newMatch.player1 = createPlayerFromJson(player1Json)
    return newMatch

#takes the json for a player and creates a player from it
def createPlayerFromJson(player):
    playerJson = json.loads(player)
    newPlayer = Player()
    newPlayer.id = playerJson['id']
    playerdataJson = json.dumps(playerJson['playerdata'])
    newPlayer.playerdata = createPlayerDataFromJson(playerdataJson)
    return newPlayer

def createPlayerDataFromJson(playerdata):
    playerdataJson = json.loads(playerdata)
    newPlayerdata = PlayerData()
    newPlayerdata.dairy = playerdataJson['dairy']
    newPlayerdata.protein = playerdataJson['protein']
    newPlayerdata.grain = playerdataJson['grain']
    newPlayerdata.vegetable = playerdataJson['vegetable']
    newPlayerdata.fruit = playerdataJson['fruit']
    newPlayerdata.sweets = playerdataJson['sweets']
    return newPlayerdata

if __name__ == "__main__":
    app.debug = True
    #app.run()
    app.run(host='0.0.0.0')

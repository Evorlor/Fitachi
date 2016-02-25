from flask import Flask
app = Flask(__name__)

@app.route('/start')
def start():
    return "Launching server..."

class Player:
    hitPoints = 100
    damage = 10

currentPlayerTurn = 0 #whether it's player 1 or player 2's turn
player0 = Player()
player1 = Player()

@app.route('/take_turn/<playerNumber>')
def takeTurn(playerNumber):
    global currentPlayerTurn
    if(str(currentPlayerTurn) != playerNumber):
        return "false"
    currentPlayer = None
    otherPlayer = None
    global player0
    global player1
    if(playerNumber == str(0)):
        currentPlayer = player0
        otherPlayer = player1
    elif(playerNumber == str(1)):
        currentPlayer = player1
        otherPlayer = player0    
    attack(currentPlayer, otherPlayer)
    currentPlayerTurn = (currentPlayerTurn + 1) % 2;
    return "true"

@app.route('/check_for_turn/<playerNumber>')
def checkForTurn(playerNumber):
    if(str(currentPlayerTurn) == playerNumber):
        return "true"
    else:
        return "false"

def attack(player, otherPlayer):
    otherPlayer.hitPoints -= player.damage

if __name__ == "__main__":
    app.debug = True
    app.run()
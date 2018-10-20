using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Player {

    public Image panel;
    public Text text;
    public Button button;

}

[Serializable]
public class PlayerColor {

    public Color panelColor;
    public Color textColor;

}

public class GameController : MonoBehaviour {

    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;

    private string playerSide;
    private int moveCount;

    //???
    void Awake() {
        setGameControllerReferenceOnButtons();
        //playerSide = "X";
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        //setPlayerColors(playerX, playerO);
    }

    void setPlayerColors(Player newPlayer, Player oldPlayer) {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    //???
    void setGameControllerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridSpace>().setGameControllerReference(this);
        }
    }
    
    public string getPlayerSide() {
        return playerSide;
    }

    //???
    public void endTurn() {

        moveCount++;
        
        //Top horizontal
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) {
            gameOver(playerSide);
        }

        //Middle horizontal:
        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) {
            gameOver(playerSide);
        }

        //Bottom horizontal:
        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) {
            gameOver(playerSide);
        }

        //Left vertical:
        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) {
            gameOver(playerSide);
        }

        //Middle vertical:
        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) {
            gameOver(playerSide);
        }

        //Right vertical:
        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) {
            gameOver(playerSide);
        }

        //Left diagonal
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) {
            gameOver(playerSide);
        }

        //Right diagonal
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide) {
            gameOver(playerSide);
        }

        if (moveCount >= 9) {
            gameOver("Draw");
        }
        changeSides();
    }

    void gameOver(string winningPlayer) {
        setBoardInteractable(false);
        if (winningPlayer == "Draw") {
            setGameOverText("It's a Draw!");
        }
        else {
            setGameOverText(winningPlayer + " Wins!");
        }

        restartButton.SetActive(true);

        //for (int i = 0; i < buttonList.Length; i++) {
        //    buttonList[i].GetComponentInParent<Button>().interactable = false;
        //}
        //gameOverPanel.SetActive(true);
        //gameOverText.text = playerSide + " Wins!";
        //setGameOverText(playerSide + " Wins!");
        //restartButton.SetActive(true);
    }

    private void setGameOverText(string value) {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void changeSides() {
        //Ternary Operator:
        //playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X") {
            playerSide = "O";
        }
        else {
            playerSide = "X";
        }
        if (playerSide == "X") {
            setPlayerColors(playerX, playerO);
        }
        else {
            setPlayerColors(playerO, playerX);
        }
    }

    public void restartGame() {
        //playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        //setPlayerColors(playerX, playerO);
        setBoardInteractable(true);
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].text = "";
        }
        startInfo.SetActive(true);
    }

    void setBoardInteractable(bool toggle) {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void setStartingSide(string startingSide) {
        playerSide = startingSide;
        if (playerSide == "X") {
            setPlayerColors(playerX, playerO);
        }
        else {
            setPlayerColors(playerO, playerX);
        }
        startGame();
    }

    void startGame() {
        setBoardInteractable(true);
        setPlayerButtons(false);
        startInfo.SetActive(false);
    }

    void setPlayerButtons(bool toggle) {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

}

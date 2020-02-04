using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    public GameObject endGamePanel;
    public Text winnerText;

    public void CloseEndGame() {
        endGamePanel.SetActive(false);
    }

    public void OpenEndGame(string winner) {
        endGamePanel.SetActive(true);

        winnerText.text = winner + "won!";
    }

    public void PlayAgain() {
        this.gameObject.GetComponent<GameManager>().InitializeGame();
    }
}

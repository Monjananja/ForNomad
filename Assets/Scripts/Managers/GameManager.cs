using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cardsPerDeck;
    public int cardsBlocked;
    public int maxHealth;

    CardsData cardsData;
    DecksManager decks;
    PlayerManager player1;
    PlayerManager player2;
    PlayerUI player1UI;
    PlayerUI player2UI;
    GeneralUI ui;

    bool turnPlayer1;
    bool gameEnded;

    // Start is called before the first frame update
    private void Start() {
        cardsData = this.gameObject.GetComponent<CardsData>();
        decks = this.gameObject.GetComponent<DecksManager>();
        ui = this.gameObject.GetComponent<GeneralUI>();
        player1 = GameObject.Find("Player1").GetComponent<PlayerManager>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerManager>();
        player1UI = GameObject.Find("Player1").GetComponent<PlayerUI>();
        player2UI = GameObject.Find("Player2").GetComponent<PlayerUI>();

        InitializeGame();
    }

    public void InitializeGame()
    {
        ui.CloseEndGame();

        cardsData.LoadGameData();

        player1.InitializeDeck(cardsPerDeck);
        decks.PopulateDeck(player1.deck, cardsPerDeck, cardsData.cards);
        decks.ShuffleDeck(player1.deck);
        player1.cardFromDeck = 0;
        player1UI.ClearAllSlots();
        player1.health = maxHealth;
        player1UI.UpdateHealthUI(player1.health);

        player2.InitializeDeck(cardsPerDeck);
        decks.PopulateDeck(player2.deck, cardsPerDeck, cardsData.cards);
        decks.ShuffleDeck(player2.deck);
        player2.cardFromDeck = 0;
        player2UI.ClearAllSlots();
        player2.health = maxHealth;
        player2UI.UpdateHealthUI(player2.health);

        RandomizeTurn();
    }

    void RandomizeTurn() {
        int player = Random.Range(0, 100);

        if (player < 50) {
            player1.StartTurn();
            player2.EndTurn();
            turnPlayer1 = true;
        } else {
            player2.StartTurn();
            player1.EndTurn();
            turnPlayer1 = false;
        }

        gameEnded = false;
        //Debug.Log(player);
    }

    public void FinishTurn() {
        if (!gameEnded) {
            if (turnPlayer1) {
                player2.StartTurn();
                player1.EndTurn();
                turnPlayer1 = false;
            }
            else {
                player1.StartTurn();
                player2.EndTurn();
                turnPlayer1 = true;
            }
        }
    }

    public void EndGame() {
        player1.EndTurn();
        player2.EndTurn();

        string winner = "";

        if (turnPlayer1) {
            winner = "Player 1";
        }
        else {
            winner = "Player 2";
        }

        gameEnded = true;

        ui.OpenEndGame(winner);
    }
}

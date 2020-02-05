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
    PlayerActions player1Actions;
    PlayerActions player2Actions;
    GeneralUI ui;

    bool turnPlayer1;
    bool turnPlayer2;
    bool gameEnded;
    string actionPlayer1;
    string actionPlayer2;

    // Start is called before the first frame update
    private void Start() {
        cardsData = this.gameObject.GetComponent<CardsData>();
        decks = this.gameObject.GetComponent<DecksManager>();
        ui = this.gameObject.GetComponent<GeneralUI>();
        player1 = GameObject.Find("Player1").GetComponent<PlayerManager>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerManager>();
        player1UI = GameObject.Find("Player1").GetComponent<PlayerUI>();
        player2UI = GameObject.Find("Player2").GetComponent<PlayerUI>();
        player1Actions = GameObject.Find("Player1").GetComponent<PlayerActions>();
        player2Actions = GameObject.Find("Player2").GetComponent<PlayerActions>();

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

        gameEnded = false;
        player1.StartTurn();
        turnPlayer1 = true;
        player2.StartTurn();
        turnPlayer2 = true;
    }

    public void FinishTurn(int player, string action) {
        if (!gameEnded) {
            switch (player) {
                case 1:
                    player1.EndTurn();
                    turnPlayer1 = false;
                    actionPlayer1 = action;
                    break;
                case 2:
                    player2.EndTurn();
                    turnPlayer2 = false;
                    actionPlayer2 = action;
                    break;
            }
        }

        if (!turnPlayer1 && !turnPlayer2) {
            if (actionPlayer1 == "Charge") {
                player1Actions.Charge();
            }
            if (actionPlayer2 == "Charge") {
                player2Actions.Charge();
            }

            if (actionPlayer1 == "Block") {
                player1Actions.Block();
            }
            if (actionPlayer2 == "Block") {
                player2Actions.Block();
            }

            if (actionPlayer1 == "Attack") {
                player1Actions.Attack();
            }
            if (actionPlayer2 == "Attack") {
                player2Actions.Attack();
            }

            actionPlayer1 = "";
            actionPlayer2 = "";

            player1Actions.CheckIfUnblock();
            player1.StartTurn();
            turnPlayer1 = true;

            player2Actions.CheckIfUnblock();
            player2.StartTurn();
            turnPlayer2 = true;
        }
    }

    public void EndGame(int player) {
        player1.EndTurn();
        player2.EndTurn();

        string winner = "";

        switch (player) {
            case 1:
                winner = "Player 1";
                break;
            case 2:
                winner = "Player 2";
                break;
        }

        gameEnded = true;

        ui.OpenEndGame(winner);
    }
}

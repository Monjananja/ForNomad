using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Deck deck;
    public int cardFromDeck;

    PlayerUI ui;
    GameManager manager;

    public int health;
    public bool isBlocking;
    public int blockValue;

    public void InitializeDeck(int cards) {
        ui = this.gameObject.GetComponent<PlayerUI>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        deck = new Deck();
        deck.card = new Cards[cards];

        isBlocking = false;
        blockValue = 0;
    }

    public void StartTurn() {
        ui.UpdateTurnUI(true);
    }

    public void EndTurn() {
        ui.UpdateTurnUI(false);
    }

    public void DealDamage(int damage, int player) {
        //Debug.Log("Block " + isBlocking);

        if (isBlocking) {
            damage -= blockValue;
            isBlocking = false;
            blockValue = 0;
            //Debug.Log("Blocking " + blockValue + ", Damage" + damage);
        }

        health -= damage;

        ui.UpdateHealthUI(health);

        if (health <= 0) {
            manager.EndGame(player);
        }
    }
}

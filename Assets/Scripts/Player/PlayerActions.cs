using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject otherPlayer;

    PlayerManager player;
    PlayerUI ui;
    GameManager manager;
    DecksManager decks;

    PlayerManager enemy;
    PlayerUI enemyUI;

    private void Start() {
        player = this.gameObject.GetComponent<PlayerManager>();
        ui = this.gameObject.GetComponent<PlayerUI>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        decks = GameObject.Find("GameManager").GetComponent<DecksManager>();

        enemy = otherPlayer.GetComponent<PlayerManager>();
        enemyUI = otherPlayer.GetComponent<PlayerUI>();
    }

    public void Attack() {
        int damage = 0;

        for (int i = 0; i < ui.slots.Length; i++) {
            if (!ui.slots[i].isVoid) {
                int value = int.Parse(ui.slots[i].value.text);
                damage += value;
            }
            else {
                break;
            }
        }

        enemy.DealDamage(damage);

        ui.ClearAllSlots();

        CheckIfUnblock();
        manager.FinishTurn();
    }

    public void Charge() {
        int index = player.cardFromDeck;
        int slot = 0;

        for (int i = 0; i < ui.slots.Length; i++) {
            if (ui.slots[i].isVoid) {
                slot = i;
                break;
            }
        }

        ui.PopulateSlot(player.deck.card[index], slot);
        player.cardFromDeck++;
        CheckIfReshuffle();
        CheckIfUnblock();

        manager.FinishTurn();
    }

    public void Block() {
        player.isBlocking = true;

        int block = 0;

        for (int i = 0; i < manager.cardsBlocked; i++) {
            Debug.Log("Card value: " + enemyUI.slots[i].value.text);
            int value = int.Parse(enemyUI.slots[i].value.text);
            block += value;
        }

        //Debug.Log("Block Value: " + block);
        player.blockValue = block;

        manager.FinishTurn();
    }

    void CheckIfReshuffle() {
        if (player.cardFromDeck == (manager.cardsPerDeck - 1)) {
            decks.ShuffleDeck(player.deck);
            player.cardFromDeck = 0;
        }
    }

    void CheckIfUnblock() {
        if (player.isBlocking) {
            player.isBlocking = false;
            player.blockValue = 0;
        }
    }
}

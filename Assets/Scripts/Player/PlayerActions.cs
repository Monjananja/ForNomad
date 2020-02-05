using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject otherPlayer;
    public int numPlayer;

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

    public void AttackUI() {
        manager.FinishTurn(numPlayer, "Attack");
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

        enemy.DealDamage(damage, numPlayer);

        ui.ClearAllSlots();
    }

    public void ChargeUI() {
        manager.FinishTurn(numPlayer, "Charge");
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
    }

    public void BlockUI() {
        manager.FinishTurn(numPlayer, "Block");
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
    }

    void CheckIfReshuffle() {
        if (player.cardFromDeck == (manager.cardsPerDeck - 1)) {
            decks.ShuffleDeck(player.deck);
            player.cardFromDeck = 0;
        }
    }

    public void CheckIfUnblock() {
        if (player.isBlocking) {
            player.isBlocking = false;
            player.blockValue = 0;
        }
    }
}

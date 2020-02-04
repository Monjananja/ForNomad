using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecksManager : MonoBehaviour
{

    public void PopulateDeck(Deck deck, int cardsPerDeck, Cards[] cards) {
        Cards[] shuffled = cards;

        for (int i = 0; i < shuffled.Length; i++) {
            Cards temp = shuffled[i];
            int randomIndex = Random.Range(i, shuffled.Length);
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }

        for (int i = 0; i < cardsPerDeck; i++) {
            deck.card[i] = shuffled[i];
        }

        //DebugCards(deck);
    }

    public void ShuffleDeck(Deck deck) {
        Cards[] shuffled = deck.card;

        for (int i = 0; i < shuffled.Length; i++) {
            Cards temp = shuffled[i];
            int randomIndex = Random.Range(i, shuffled.Length);
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }

        deck.card = shuffled;

        DebugCards(deck);
    }

    void DebugCards(Deck deck) {
        if (deck.card != null) {
            string log = "";
            for (int i = 0; i < deck.card.Length; i++) {
                log += deck.card[i].name + ": ";
                log += deck.card[i].pointsAttack.ToString() + ", ";
                log += deck.card[i].typeAttack + "\n";
            }
            Debug.Log(log);
        }
        else {
            Debug.Log("Cards is null");
        }
    }
}

public class Deck {
    public Cards[] card;
}

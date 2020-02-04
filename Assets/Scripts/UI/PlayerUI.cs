using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slot[] slots;
    public Text hpText;
    public Text turn;

    public Button attackBtn;
    public Button chargeBtn;
    public Button blockBtn;

    public void ClearAllSlots() {
        for (int i = 0; i < slots.Length; i++) {
            slots[i].name.text = "";
            slots[i].type.text = "";
            slots[i].value.text = "";
            slots[i].isVoid = true;
        }
    }

    public void ClearOneSlot(int index) {
        slots[index].name.text = "";
        slots[index].type.text = "";
        slots[index].value.text = "";
        slots[index].isVoid = true;
    }

    public void PopulateSlot(Cards card, int index) {
        slots[index].name.text = card.name;
        slots[index].type.text = card.typeAttack;
        slots[index].value.text = card.pointsAttack.ToString();
        slots[index].isVoid = false;
    }

    public void UpdateHealthUI(int health) {
        hpText.text = "HP: " + health.ToString();
    }

    public void UpdateTurnUI(bool isTurn) {
        if (isTurn) {
            turn.text = "TURN";
        }
        else {
            turn.text = "";
        }
        attackBtn.gameObject.SetActive(isTurn);
        chargeBtn.gameObject.SetActive(isTurn);
        blockBtn.gameObject.SetActive(isTurn);
    }
}

[System.Serializable]
public class Slot {
    public bool isVoid;
    public Text name;
    public Text type;
    public Text value;
}

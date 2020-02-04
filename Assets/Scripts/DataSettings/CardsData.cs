using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardsData : MonoBehaviour
{
    public Cards[] cards;

    private string fileName = "CardsData.json";

    public void LoadGameData() {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText(filePath);

            //Debug.Log(dataAsJson);

            CardsRoot loadedData = JsonUtility.FromJson<CardsRoot>(dataAsJson);

            cards = loadedData.cardsRoot;

            Debug.Log("Streaming Cards: " + loadedData.cardsRoot.Length);

            DebugCards();
        }
        else {
            Debug.LogError("Inventory Data file missing!");
        }
    }

    void DebugCards() {
        if (cards != null) {
            string log = "";
            for (int i = 0; i < cards.Length; i++) {
                log += cards[i].name + ": ";
                log += cards[i].pointsAttack.ToString() + ", ";
                log += cards[i].typeAttack + "\n";
            }
            Debug.Log(log);
        } else {
            Debug.Log("Cards is null");
        }
    }
}

[System.Serializable]
public class CardsRoot {
    public Cards[] cardsRoot;
}

[System.Serializable]
public class Cards {
    public string name;
    public int pointsAttack;
    public string typeAttack;
}

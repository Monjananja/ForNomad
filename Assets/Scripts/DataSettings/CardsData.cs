using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class CardsData : MonoBehaviour
{
    public Cards[] cards;

    private string fileName = "CardsData.json";

    public void LoadGameData() {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        string newPath = Path.Combine(Application.persistentDataPath, fileName);

        DoWWW_Android(fileName, filePath, newPath);

        if (File.Exists(newPath)) {
            string dataAsJson = File.ReadAllText(newPath);

            CardsRoot loadedData = JsonUtility.FromJson<CardsRoot>(dataAsJson);

            cards = loadedData.cardsRoot;

            Debug.Log("Streaming Armors: " + loadedData.cardsRoot.Length);
        }
        else {
            Debug.LogError("Inventory Data file missing!");
        }

    }

    static void DoWWW_Android(string name, string origPath, string newPath) {
        using (UnityWebRequest reader = UnityWebRequest.Get(origPath)) {
            reader.SendWebRequest();
            while (!reader.isDone) {
            }
            if (!reader.isHttpError && !reader.isNetworkError) {
                File.WriteAllBytes(newPath, reader.downloadHandler.data);
            }
            else {
                Debug.LogError("Database " + name + " not found at " + origPath);
                Debug.LogError("Error: " + reader.error);
            }
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

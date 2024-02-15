using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPersonEvent : MonoBehaviour
{
    // Deklaracja zdarzenia, kt�re b�dzie nas�uchiwane przez inne skrypty
    public delegate void ButtonClickEventHandler();

    public static event ButtonClickEventHandler OnButtonClick;

    public static List<GameObject> personList = new List<GameObject>();
    public GameObject personPrefab;

    private void Start()
    {
        // Dodaj nas�uchiwanie klikni�cia przycisku
        GetComponent<Button>().onClick.AddListener(ShowMessage);
    }

    // Metoda wywo�ywana po klikni�ciu przycisku
    private void ShowMessage()
    {
        GameObject spawnedObject = Instantiate(personPrefab, new Vector3(0, -15, 0), Quaternion.identity);
        personList.Add(spawnedObject);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddNormalCashEvent : MonoBehaviour
{
    public GameObject normalCashPrefab;
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public static List<GameObject> normalCashList = new List<GameObject>();
    private int offsetX = 6;

    //Tworzenie List dla Œcie¿ek i ludzi:
    public static List<PathAndPerson> PersonAndPathList = new List<PathAndPerson>();

    // Start is called before the first frame update
    private void Start()
    {
        ChangeNormalCash(slider.value); // Pierwsze wywo³anie metody
        slider.onValueChanged.AddListener(delegate { ChangeNormalCash(slider.value); });
    }

    private void ChangeNormalCash(float value)
    {
        ClearAndDestroy();

        if (normalCashPrefab != null)
        {
            for (int i = 0; i < (int)value; i++)
            {
                PersonAndPathList.Add(new PathAndPerson() { path = (i + 1), people = new List<Person>() });
                // Tworzymy nowy obiekt na podstawie pobranego prefabu
                GameObject spawnedObject = Instantiate(normalCashPrefab, new Vector3(-30 + i * offsetX, 15, 0), Quaternion.identity);
                spawnedObject.name = "CashRegister" + (i + 1);
                normalCashList.Add(spawnedObject);
            }
        }
    }

    private static void ClearAndDestroy()
    {
        foreach (var item in normalCashList)
        {
            Destroy(item);
        }
        normalCashList.Clear();
        PersonAndPathList.Clear();
    }

    private void Update()
    {
        sliderText.text = slider.value.ToString();
    }
}
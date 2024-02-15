using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddSelfServiceCashEvent : MonoBehaviour
{
    public GameObject selfServiceCashPrefab;
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public static List<GameObject> selfServiceCashList = new List<GameObject>();
    private int offsetX = 6;

    //Tworzenie List dla Œcie¿ek i ludzi:
    public static List<PathAndPerson> SelfServicePersonAndPathList = new List<PathAndPerson>();

    private int MaxAmountOfNormalCashes = 6;

    // Start is called before the first frame update
    private void Start()
    {
        ChangeNormalCash(slider.value); // Pierwsze wywo³anie metody
        slider.onValueChanged.AddListener(delegate { ChangeNormalCash(slider.value); });
    }

    private void ChangeNormalCash(float value)
    {
        ClearAndDestroy();

        if (selfServiceCashPrefab != null)
        {
            for (int i = 0; i < (int)value; i++)
            {
                SelfServicePersonAndPathList.Add(new PathAndPerson() { path = (MaxAmountOfNormalCashes + i + 1), people = new List<Person>() });
                // Tworzymy nowy obiekt na podstawie pobranego prefabu
                GameObject spawnedObject = Instantiate(selfServiceCashPrefab, new Vector3(6 + i * offsetX, 15, 0), Quaternion.identity);
                spawnedObject.name = "SelfServiceCashRegister" + (MaxAmountOfNormalCashes + i + 1);
                selfServiceCashList.Add(spawnedObject);
            }
        }
    }

    private static void ClearAndDestroy()
    {
        foreach (var item in selfServiceCashList)
        {
            Destroy(item);
        }
        selfServiceCashList.Clear();
        SelfServicePersonAndPathList.Clear();
    }

    private void Update()
    {
        sliderText.text = slider.value.ToString();
    }
}
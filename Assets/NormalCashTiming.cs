using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class NormalCashTiming : MonoBehaviour
{
    public TextMeshProUGUI cashTime;
    private string myName;
    private int myNumer;

    // Start is called before the first frame update
    private void Start()
    {
        myName = gameObject.name;
        myNumer = int.Parse(Regex.Match(myName, @"\d+$").Value);
    }

    // Update is called once per frame
    private void Update()
    {
        if (AddNormalCashEvent.PersonAndPathList != null)
        {
            foreach (var PersonAndPath in AddNormalCashEvent.PersonAndPathList)
            {
                var pathNumber = int.Parse(Regex.Match(PersonAndPath.path.ToString(), @"\d+").Value);
                if (pathNumber == myNumer)
                {
                    float time = 0;
                    foreach (var person in PersonAndPath.people)
                    {
                        time += person.time;
                    }
                    cashTime.text = System.Math.Round(time, 3).ToString() + " min";
                }
            }
        }
    }
}
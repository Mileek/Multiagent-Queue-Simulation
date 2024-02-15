using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    private float selfServiceChance = 0.4f;
    public float speed = 0.05f;
    private int index = 0;

    //Kasy Normalne
    public List<GameObject> path1;

    public List<GameObject> path2;
    public List<GameObject> path3;
    public List<GameObject> path4;
    public List<GameObject> path5;
    public List<GameObject> path6;

    //Kasy samoobs³ugowe
    public List<GameObject> path7;
    public List<GameObject> path8;
    public List<GameObject> path9;
    private List<GameObject> selectedPath = new List<GameObject>();
    private Rigidbody2D rb;

    private bool firstFinishedMoving = false;
    private bool alreadyHit = false;
    private bool isFirst = false;
    private PathAndPerson smallestQueue = new PathAndPerson();
    private PathAndPerson smallestSelfServiceQueue = new PathAndPerson();
    private PathAndPerson selectedQueue = new PathAndPerson();
    private Person selectedPerson;

    //OnEnabled uuchomi siê przed startem
    void OnEnable()
    {
        SelfServiceChanceEvent.OnSliderValueChangedEvent += SelfServiceChanceValueChanged;
        PersonSpeedEvent.OnSliderValueChangedEvent += PersonSpeedValueChanged;
    }
    void SelfServiceChanceValueChanged(float obj)
    {
        selfServiceChance = obj / 100;
    }
    void PersonSpeedValueChanged(float obj)
    {
        speed = obj;
    }
    // Start is called before the first frame update
    private void Start()
    {
        selfServiceChance = SelfServiceChanceEvent.ServiceChance / 100;
        speed = PersonSpeedEvent.PersonSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; //Nie ma grawitacji
        PathSelector(); // Wybór œcie¿ek
    }

    private void PathSelector()
    {
        if (AddNormalCashEvent.PersonAndPathList != null && AddSelfServiceCashEvent.SelfServicePersonAndPathList != null)
        {
            //Czas w normalnej kolejce
            float timeInQueue = 4 + UnityEngine.Random.value * 4;
            //Redukcja czasu w kolejce
            float reductionFactor = 0.7f + UnityEngine.Random.value * 0.3f;
            //Wybranie najmniejszej kolejki
            smallestQueue = AddNormalCashEvent.PersonAndPathList.OrderBy(x => x.people.Count).FirstOrDefault();
            smallestSelfServiceQueue = AddSelfServiceCashEvent.SelfServicePersonAndPathList.OrderBy(x => x.people.Count).FirstOrDefault();

            //Mapowanie œcie¿ek
            List<List<GameObject>> normalPaths = new List<List<GameObject>> { path1, path2, path3, path4, path5, path6 };
            List<List<GameObject>> selfServicePaths = new List<List<GameObject>> { path7, path8, path9 };

            if ((smallestSelfServiceQueue.people.Count < smallestQueue.people.Count) && (UnityEngine.Random.value < selfServiceChance))
            {
                if (smallestSelfServiceQueue.path >= 7 && smallestSelfServiceQueue.path <= 9)
                {
                    selectedPath = selfServicePaths[smallestSelfServiceQueue.path - 7];
                    selectedQueue = smallestSelfServiceQueue;
                    //Jeœli jeszcze tego nie zrobi³eœ, to dodaj t¹ osobê do listy osób na danej œcie¿ce
                    AddPersonOnPath(timeInQueue * reductionFactor);
                }
            }
            else
            {
                if (smallestQueue.path >= 1 && smallestQueue.path <= 6)
                {
                    selectedPath = normalPaths[smallestQueue.path - 1];
                    selectedQueue = smallestQueue;
                    //Jeœli jeszcze tego nie zrobi³eœ, to dodaj t¹ osobê do listy osób na danej œcie¿ce, czas w zakresie 4-8 sekund
                    AddPersonOnPath(timeInQueue);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Zmiana pierwszej osoby w kolejce, po usuniêciu
        if (selectedQueue.people.FirstOrDefault().person == gameObject)
        {
            isFirst = true;
        }
        //Ruch po œcie¿ce, zazwyczaj s¹ 4 waypointy, warunek uwzglêdnia równie¿ ¿e na liœcie nie ma nikogo, to znaczy ¿e ta "osoba" jest pierwsza w kolejce
        if ((selectedQueue.people.Count == 1 && !firstFinishedMoving) || isFirst)
        {
            //Ruch po œcie¿ce
            Vector2 destination = new Vector2();
            if (index == selectedPath.Count - 1)
            {
                destination = new Vector2(selectedPath.Last().transform.position.x, selectedPath.Last().transform.position.y + 1);
            }
            else
            {
                destination = selectedPath[index].transform.position;
            }
            var newPos = Vector2.MoveTowards(rb.transform.position, destination, speed);
            rb.transform.position = newPos;

            var distance = Vector2.Distance(rb.transform.position, destination);

            if (distance < 0.01f)
            {
                if (index < selectedPath.Count - 1)
                {
                    index++;
                }
                else if (index == selectedPath.Count - 1)
                {
                    //Pierwszy skoñczy³ ruch
                    firstFinishedMoving = true;
                }
            }
        }
        else if (((selectedQueue.people.Count > 1 && !firstFinishedMoving) || !selectedPerson.moveQueue))
        {
            Vector2 destination = new Vector2();
            if (index == selectedPath.Count - 1)
            {
                destination = new Vector2(selectedPath.Last().transform.position.x, selectedPath.Last().transform.position.y - selectedPerson.distance);
            }
            else
            {
                destination = selectedPath[index].transform.position;
            }
            var newPos = Vector2.MoveTowards(rb.transform.position, destination, speed);
            rb.transform.position = newPos;

            var distance = Vector2.Distance(rb.transform.position, destination);

            if (distance < 0.01f)
            {
                if (index < selectedPath.Count - 1)
                {
                    index++;
                }
                else if (index == selectedPath.Count - 1)
                {
                    //Kolejna osoba skoñczy³a ruch
                    selectedPerson.moveQueue = true;
                }
            }
        }
    }

    private void AddPersonOnPath(float timeInQueue)
    {
        selectedPerson = new Person(gameObject, timeInQueue, selectedQueue.people.Count);
        selectedQueue.people.Add(selectedPerson);
    }

    public void MoveForward()
    {
        foreach (var item in selectedQueue.people)
        {
            //Ruch w przód w kolejce, "Wymuszenie" funkcji Update
            item.distance -= 1;
            item.moveQueue = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdzamy, czy obiekt, z którym wyst¹pi³a kolizja, ma odpowiedni tag lub inn¹ cechê identyfikacyjn¹
        if ((collision.gameObject.CompareTag("NormalCash") || collision.gameObject.CompareTag("SelfServiceCash")) && !alreadyHit)
        {
            alreadyHit = true;
            Debug.Log("Kolizja z Kas¹");
            // Zatrzymujemy obiekt i uruchamiamy Coroutine oczekiwania
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0; // Zatrzymuje obrót obiektu
            StartCoroutine(WaitAndDelete());
        }
    }

    // Przyk³adowa metoda do usuwania obiektu z listy i zniszczenia go
    private void RemoveAndDestroyObject(Person objToRemove)
    {
        if (selectedQueue.people.Contains(objToRemove))
        {
            // Zniszcz obiekt
            Destroy(objToRemove.person);
            // Usuñ obiekt z listy
            selectedQueue.people.Remove(objToRemove);
            MoveForward();
        }
    }

    private IEnumerator WaitAndDelete()
    {
        // Czekamy przez losowy czas dla danej osoby, na usuniêcie tej osoby z kolejki
        yield return new WaitForSeconds(selectedQueue.people.FirstOrDefault().time);

        Person firstPerson = selectedQueue.people.FirstOrDefault();
        if (selectedQueue.people.Count > 0)
        {
            RemoveAndDestroyObject(firstPerson);
        }
    }
}
//Klasy powinny byæ w innych klasach, ale nie chcia³o mi siê ich przenosiæ
public class PathAndPerson
{
    public int path;
    public List<Person> people;

    public PathAndPerson()
    {
    }
}

public class Person
{
    public float time;
    public float distance;
    public bool moveQueue = false;
    public GameObject person = new GameObject();

    public Person(GameObject person, float time, float distance)
    {
        this.person = person;
        this.time = time;
        this.distance = distance;
    }
}
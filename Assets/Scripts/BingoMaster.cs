using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BingoMaster : MonoBehaviour
{
    [SerializeField] private GameObject[] numbers;
    private TextMeshPro[] numbersTexts;
    public int numbersPoolLength = 90;
    public float showNumbersInterval = 3f;
    
    public List<int> numbersPool = new List<int>();

    private void Awake()
    {
        numbersTexts = new TextMeshPro[numbers.Length];
        
        for (int i = 0; i < numbers.Length; i++)
        {
            numbersTexts[i] = numbers[i].GetComponentInChildren<TextMeshPro>();
        }
    }

    void Start()
    {
        for (int i = 1; i <= numbersPoolLength; i++)
        {
            numbersPool.Add(i);
        }

        StartCoroutine(ShowNumbers());
    }

    IEnumerator ShowNumbers()
    {
        while (numbersPool.Count > 0)
        {
            yield return new WaitForSeconds(showNumbersInterval);

            // Извлекаем случайное число из пула
            int index = Random.Range(0, numbersPool.Count);
            int drawnNumber = numbersPool[index];
            numbersPool.RemoveAt(index);

            foreach (var number in numbers)
            {
                number.name = "Number " + drawnNumber;
            }

            foreach (var numberText in numbersTexts)
            {
                numberText.text = drawnNumber.ToString();
            }
        }
        if (numbersPool.Count == 0)
        {
            Debug.Log("Більше немає чисел!");
        }
    }
}

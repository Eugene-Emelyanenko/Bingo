using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Column : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    public GameObject[] cells = null;
    private Grid grid;

    private void Awake()
    {
        grid = GetComponentInParent<Grid>();
        cells = new GameObject[grid.rowCount];
        
        GenerateCells();
    }

    void GenerateCells()
    {
        List<int> availableNumbers = new List<int>();
        for (int i = GetMinNumber(); i <= GetMaxNumber(); i++)
        {
            availableNumbers.Add(i);
        }

        for (int i = 0; i < grid.rowCount; i++)
        {
            GameObject cell = Instantiate(cellPrefab, transform);
            cell.name = "Cell_" + i;
            cells[i] = cell;
            Cell cellScript = cell.GetComponent<Cell>();

            // Проверяем, есть ли еще доступные числа
            if (availableNumbers.Count == 0)
            {
                Debug.LogError("Недостатньо чисел у стовпці.");
                return;
            }

            // Выбираем случайное число из доступных
            int randomIndex = Random.Range(0, availableNumbers.Count);
            int currentNumber = availableNumbers[randomIndex];
            availableNumbers.RemoveAt(randomIndex);

            cellScript.SetNumber(currentNumber);
        }
    }

    int GetMinNumber()
    {
        return transform.GetSiblingIndex() * 10 + 1;
    }
    
    int GetMaxNumber()
    {
        return (transform.GetSiblingIndex() + 1) * 10;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public int emptyCellCountInRow = 4;
    public int columnCount = 9;
    public int rowCount = 3;
    [SerializeField] private GameObject columnPrefab;
    public GameObject[] columns = null;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text cardText;

    private void Awake()
    {
        columns = new GameObject[columnCount];
    }

    void Start()
    {
        gameOverMenu.SetActive(false);
        GenerateColumns();
    }

    void GenerateColumns()
    {
        for (int i = 0; i < columnCount; i++)
        {
            GameObject column = Instantiate(columnPrefab, transform);
            column.name = "Column_" + i;
            column.transform.SetParent(this.transform);
            columns[i] = column;
        }
        
        SetEmptyCellsInRows();
    }

    public void SetEmptyCellsInRows()
    {
        for (int j = 0; j < rowCount; j++)
        {
            GameObject[] row = new GameObject[columnCount];
            
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = columns[i].GetComponent<Column>().cells[j];
            }

            int[] randomIndexes = GetRandomIndexes(row.Length, emptyCellCountInRow);
            
            foreach (int index in randomIndexes)
            {
                row[index].gameObject.GetComponent<Cell>().SetEmpty();
            }
        }
    }
    
    private int[] GetRandomIndexes(int arrayLength, int count)
    {
        if (count > arrayLength)
        {
            Debug.LogError("Количество случайных индексов не может быть больше длины массива!");
            return null;
        }

        int[] indexes = new int[count];
        System.Random random = new System.Random();

        for (int i = 0; i < count; i++)
        {
            int newIndex = random.Next(0, arrayLength);
            // Проверяем, что этот индекс еще не был выбран
            while (Array.IndexOf(indexes, newIndex) != -1)
            {
                newIndex = random.Next(0, arrayLength);
            }
            indexes[i] = newIndex;
        }

        return indexes;
    }

    public void CheckIsFilled()
    {
        for (int j = 0; j < rowCount; j++)
        {
            GameObject[] row = new GameObject[columnCount];
            
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = columns[i].GetComponent<Column>().cells[j];
            }
            
            foreach (GameObject cell in row)
            {
                if(!cell.GetComponent<Cell>().IsFilled())
                    return;
            }
        }
        
        GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game is Over");
        gameOverMenu.SetActive(true);
        gameOverText.text = $"Переможець {cardText.text}!";
    }
}

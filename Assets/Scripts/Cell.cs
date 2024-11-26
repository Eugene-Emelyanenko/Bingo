using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Grid grid;
    private BingoMaster bingoMaster;
    private GameObject cap = null;
    private int number = 0;
    private Text numberText = null;
    public bool isFilled = false;
    private bool isEmpty = false;

    private void Awake()
    {
        numberText = transform.GetComponentInChildren<Text>();
        cap = transform.Find("Cap").gameObject;
        grid = transform.parent.parent.GetComponent<Grid>();
        bingoMaster = GameObject.Find("BingoMaster").GetComponent<BingoMaster>();
    }

    void Start()
    {
        cap.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isFilled && !isEmpty && IsNumberMatch())
        {
            isFilled = true;
            cap.SetActive(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        grid.CheckIsFilled();
    }

    private bool IsNumberMatch() => !bingoMaster.numbersPool.Contains(number);

    public bool IsFilled() => isFilled;

    public void SetEmpty()
    {
        isEmpty = true;
        isFilled = true;
        number = 0;
        numberText.text = string.Empty;
    }

    public void SetNumber(int number)
    {
        this.number = number;
        numberText.text = this.number.ToString();
    }

    public int GetNumber() => number;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CalorieDayOne : MonoBehaviour
{
    [SerializeField] private string ListNumber;
    [SerializeField] List<Lutin> Lutins = new List<Lutin>();
     [SerializeField] private List<int> ListTotalCalorieCalculate;

    [SerializeField] int TotolCalorieBest = 0;
    [SerializeField] int BestElf = 0; //Elf +1 :)
    //
    [SerializeField] int amountLutin= 3;
    [SerializeField] int TotalCalorie= 0;

    private string dataStockage;
    private int indexLutin = 0;
    private int indexCalorie = 0;
    private bool space = false;
    private int TotolCalorieCalculate = 0;
    
    private void Start()
    {
        SetEftList();
        CalculateAllLutinCalorie();
        CheckBestElf();
    }

    public void SetEftList()
    {
        space = false;
        
        Lutins.Add(new Lutin()); 
        Lutins[indexLutin].calorie.Add(new int());
        
        foreach (char charactere in ListNumber)
        {
            if (space)
            {
                //detect Double espace
                if (charactere.ToString() == " ")
                {
                    Lutins.Add(new Lutin()); 
                    indexLutin++;
                    indexCalorie = 0;
                    Lutins[indexLutin].calorie.Add(new int());
                }
            }
            
            space = false;

            //detect Si une fois un espace
            if (charactere.ToString() == " ")
            {
                Lutins[indexLutin].calorie.Add(new int());
                indexCalorie++;
                space = true;
                dataStockage = "";
                continue;
            }
            
            dataStockage += charactere;
            Lutins[indexLutin].calorie[indexCalorie] = int.Parse(dataStockage);   
            }
    }
    public void CalculateAllLutinCalorie()
    {
        for (int i = 0; i < Lutins.Count; i++)
        {
            for (int j = 0; j < Lutins[i].calorie.Count; j++)
            {
                TotolCalorieCalculate += Lutins[i].calorie[j];
            }
            
            ListTotalCalorieCalculate.Add(TotolCalorieCalculate);
            
            if (TotolCalorieCalculate > TotolCalorieBest)
            {
                BestElf = i;
                TotolCalorieBest = TotolCalorieCalculate;
            }

            TotolCalorieCalculate = 0;
        }
    }
    
    public void CheckBestElf()
    {
        ListTotalCalorieCalculate.Sort();

        for (int i = 1; i <= amountLutin; i++)
        {
            TotalCalorie += ListTotalCalorieCalculate[ListTotalCalorieCalculate.Count - i];
        }
    }
}

[Serializable]
public class Lutin
{
    public List<int> calorie = new List<int>();
}


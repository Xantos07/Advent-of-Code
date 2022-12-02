using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPaperScissorsPart2 : MonoBehaviour
{
 [SerializeField] private TextAsset input;

    [SerializeField] private int score = 0;

    private String adversaryShot = "";
   private String playerShot = "";

    private String[,] winPart = {{"A", "Y"}, {"B", "Z"}, {"C", "X"}}; //Win 
    private String[,] egalityPart = {{"A", "X"}, {"B", "Y"}, {"C", "Z"}}; //egality 
    
    private String[,] mustLose = {{"A", "Z"}, {"B", "X"}, {"C", "Y"}};
    private String[,] mustWin = {{"A", "Y"}, {"B", "Z"}, {"C", "X"}};
    private String[,] mustEgality = {{"A", "X"}, {"B", "Y"}, {"C", "Z"}};
    
    private Dictionary<String, int> ourGame = new Dictionary<string, int>();
    
    //

    private void Start()
    {
        ourGame.Add("X", 1);
        ourGame.Add("Y", 2);
        ourGame.Add("Z", 3);

        Read();
    }

    public void Read()
    {
        foreach (char Letter in input.ToString())
        {
            if (Char.IsWhiteSpace(Letter)) continue;
            
            if (adversaryShot == "")
            {           
                adversaryShot = Letter.ToString();
                continue;
            }
            
            if (playerShot == "")
            {           
                playerShot = Letter.ToString();
                
                Convertisor(adversaryShot, playerShot);

                score += PlayOut(playerShot);
                score += Rules(adversaryShot,playerShot);

                adversaryShot = "";
                playerShot = "";
            }
        }
    }

    public int PlayOut(String _our)
    {
        foreach (var value in ourGame)
        {
            if (_our == value.Key)
            {
                return value.Value;
            }
        }
        return 0;
    }

    public int Rules(String _adversaryValue, String _our)
    {
        if (RulesComponent(egalityPart, _adversaryValue, _our,"Egality")) return 3;
        if (RulesComponent(winPart, _adversaryValue, _our, "Win")) return 6;

        //Lose
        Debug.Log("Lose");
        return 0;
    }

    public bool RulesComponent(String[,] _part, String _adversaryValue, String _our, String enumGame)
    {
        for (int y = 0; y < _part.GetLength(0); y++)
        {
            if (_part[y, 0] == _adversaryValue && _part[y, 1] == _our)
            {
                Debug.Log(enumGame);
                return true;
            }
        }
        
        return false;
    }
    public void Convertisor(String _adversaryValue, String _our)
    {
        if(ConvertissorComponent(_our, "X", mustLose, _adversaryValue)){return;}
        if(ConvertissorComponent(_our, "Y", mustEgality, _adversaryValue)){return;}
        ConvertissorComponent(_our, "Z", mustWin, _adversaryValue);
    }

    public bool ConvertissorComponent(String _OurValue, String _CheckValue, String[,] _mustCheck, String _AdversaryValue)
    {

        if (_OurValue != _CheckValue) return false;
        
        for (int y = 0; y < _mustCheck.GetLength(0); y++)
        {
            if (_mustCheck[y, 0] == _AdversaryValue)
            {
                playerShot = _mustCheck[y, 1];
                return true;
            }
        }
        
        return false;
    }
}

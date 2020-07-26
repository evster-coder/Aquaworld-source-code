using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Highscore {
    public struct records
    {
        public string name;
        public int score;
        public static bool operator >(records a, records b)
        {
            if (a.score > b.score)
                return true;
            else
                return false;
        }
        public static bool operator <(records a, records b)
        {
            if (a.score < b.score)
                return true;
            else
                return false;
        }
    }

    private string filename = @"Data/Highscore.aqua";
    private List<records> massive; //10 лучших результатов
    private int amount;


    public Highscore()
    {
        massive = new List<records>();
        try
        {

            string currString;
            if (File.Exists(filename))//файл существует
            {
                amount = 0;
                StreamReader file = new StreamReader(filename);
                while ((currString = file.ReadLine()) != null)
                {
                    string[] forSplit = currString.Split('#');//хранятся в файле через знак #
                    records tmp;
                    tmp.name = forSplit[0];
                    tmp.score = Int32.Parse(forSplit[1]);
                    massive.Add(tmp);
                    amount++;
                }
                file.Close();
            }
            else
            {
                if(System.IO.Directory.Exists(@"Data") == false)
                    System.IO.Directory.CreateDirectory(@"Data");
                File.Create(filename);
                amount = 0;
            }
        }
        catch
        {
            Application.Quit();
        }
    }
    public void AddScore(string name, int score)
    {
        records tmp;
        tmp.name = name;
        tmp.score = score;
        bool is_added = false; //добавлено или нет
        for(int i = 0; i < amount; i++)
        {
            if (tmp > massive[i])
            {
                massive.Insert(i, tmp);
                amount++;
                is_added = true;
                break;
            }
        }
        if (!is_added)
        {
            massive.Add(tmp);
            amount++;
        }
        if (amount > 10)//только топ 10
        {
            massive.RemoveAt(10);
            amount--;
        }
        StreamWriter file = new StreamWriter(filename);
        for(int i = 0; i < amount; i++)
        {
            file.WriteLine(massive[i].name + "#" + massive[i].score.ToString());
        }
        file.Close();
    }
    public List<records> getScore()
    {
        return massive;
    }
}

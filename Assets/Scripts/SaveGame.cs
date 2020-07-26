using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Xml;

public class SaveGame : MonoBehaviour {

    public Button Continue;

    private void Awake()
    {
        if (File.Exists("Data/Save.xml") == false)//Если файла нет
        {
            Continue.interactable = false;
        }
        else
        {
            Continue.interactable = true;
        }
    }

    public void StartNewGame()//начать новую(удалить сохранение если есть)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Information");
        xmlDoc.AppendChild(rootNode);

        XmlNode userNode;

        userNode = xmlDoc.CreateElement("Nickname");
        userNode.InnerText = TotalStatistics.Nickname;
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("Character");
        userNode.InnerText = TotalStatistics.Character.ToString();
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("TotalScore");
        userNode.InnerText = TotalStatistics.TotalScore.ToString();
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("CurrentLevel");
        userNode.InnerText = TotalStatistics.CurrentLevel.ToString();
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("ScoreLevel");
        userNode.InnerText = TotalStatistics.CurrentLevel.ToString();
        rootNode.AppendChild(userNode);

        xmlDoc.Save("Data/Save.xml");
    }
    public void ContinueGame()//продолжить игру(загрузить сохранение)
    {
        if (File.Exists("Data/Save.xml"))
        {
            try
            {
                int i = 0;
                XmlTextReader reader = new XmlTextReader("Data/Save.xml");
                while (reader.Read())
                {
                    if(reader.IsStartElement("Nickname") && !reader.IsEmptyElement)
                        TotalStatistics.Nickname = reader.ReadString();
                    if (reader.IsStartElement("Character") && !reader.IsEmptyElement)
                        TotalStatistics.Character = Int32.Parse(reader.ReadString());
                    if (reader.IsStartElement("TotalScore") && !reader.IsEmptyElement)
                        TotalStatistics.TotalScore = Int32.Parse(reader.ReadString());
                    if (reader.IsStartElement("CurrentLevel") && !reader.IsEmptyElement)
                        TotalStatistics.CurrentLevel = Int32.Parse(reader.ReadString());
                        if(reader.IsStartElement("LevelStats") && !reader.IsEmptyElement)
                            TotalStatistics.LevelsScore[i++] = Int32.Parse(reader.ReadString());
                }
                reader.Close();
            }
            catch
            {
                Application.Quit();
            }
        }
    }
    public static void SavingGame()//сохранение игры
    {
        if (File.Exists("Data/Save.xml"))
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Information");
            xmlDoc.AppendChild(rootNode);

            XmlNode userNode;

            userNode = xmlDoc.CreateElement("Nickname");
            userNode.InnerText = TotalStatistics.Nickname;
            rootNode.AppendChild(userNode);

            userNode = xmlDoc.CreateElement("Character");
            userNode.InnerText = TotalStatistics.Character.ToString();
            rootNode.AppendChild(userNode);

            userNode = xmlDoc.CreateElement("TotalScore");
            userNode.InnerText = TotalStatistics.TotalScore.ToString();
            rootNode.AppendChild(userNode);

            userNode = xmlDoc.CreateElement("CurrentLevel");
            userNode.InnerText = TotalStatistics.CurrentLevel.ToString();
            rootNode.AppendChild(userNode);

            for(int i = 0; i < TotalStatistics.CurrentLevel; i++)
            {
                userNode = xmlDoc.CreateElement("LevelStats");
                userNode.InnerText = TotalStatistics.LevelsScore[i].ToString();
                rootNode.AppendChild(userNode);
            }

            xmlDoc.Save("Data/Save.xml");
        }
    }
}

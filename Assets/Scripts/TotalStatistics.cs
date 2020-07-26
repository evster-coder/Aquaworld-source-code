using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalStatistics : MonoBehaviour {

    public static string Nickname; //имя пользователя
    public static int Character;//за кого играет player
    public static int TotalScore;//общий счёт за игру
    public static int CurrentLevel; //текущий уровень
    public static int[] LevelsScore = new int[10];
    public static int levelPlayer; // уровень игрока

    public struct Mission//цели на уровень
    {
        public float sizePlayer;
        public float sizeMap;

        public string name1;
        public int target_amount1;

        public string name2;
        public int target_amount2;

        public Mission(float Player, float Map, string n1, int am1, string n2, int am2)
        {
            sizePlayer = Player;
            sizeMap = Map;
            name1 = n1;
            target_amount1 = am1;
            name2 = n2;
            target_amount2 = am2;
        }
    };
    public static Mission[] targets;

    private void Awake()
    {
        if (targets == null)
        {
            targets = new Mission[10];
            targets[0] = new Mission(0.35f, 1f, "Enemys6", 10, "None", 0);
            targets[1] = new Mission(0.5f, 1.2f, "Enemys4", 10, "Enemys1", 5);
            targets[2] = new Mission(0.46f, 1.5f, "Enemys5", 20, "None", 0);
            targets[3] = new Mission(0.35f, 2f, "Enemys5", 5, "Enemys1", 10);
            targets[4] = new Mission(0.5f, 2f, "Enemys1", 5, "Enemys2", 2);
            targets[5] = new Mission(0.35f, 2f, "Enemys5", 5, "Enemys2", 5);
            targets[6] = new Mission(0.35f, 2f, "Enemys3", 1, "None", 0);
            targets[7] = new Mission(0.8f, 2f, "Enemys2", 5, "Enemys3", 5);
            targets[8] = new Mission(1f, 2f, "Enemys3", 20, "None", 0);
            targets[9] = new Mission(0.35f, 3f, "Enemys3", 10, "Enemys2", 10);
        }
    }
}

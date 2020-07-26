using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool {
    private static Transform deactivateObj;
    private static Dictionary<string, LinkedList<GameObject>> poolsDictionary; 

    public static void Init(Transform PooledObjContainer)//инициализируем объект, куда будут складываться деактивированные элементы
    {
        deactivateObj = PooledObjContainer;
        poolsDictionary = new Dictionary<string, LinkedList<GameObject>>();//и словарь
    }
    public static GameObject GetObjectFromPool(GameObject prefab)//получение объекта из пула
    {
        string name = prefab.name;
        if (!poolsDictionary.ContainsKey(name))//если нет такого в словаре
            poolsDictionary[name] = new LinkedList<GameObject>();
        GameObject return_res; //объект который будем возвращать
        if(poolsDictionary[name].Count > 0)//если щетчик больше 0
        {
            return_res = poolsDictionary[name].First.Value;
            poolsDictionary[name].RemoveFirst();//удаляем первый элемент на данном пункте словаря
            return_res.SetActive(true);
            return return_res;
        }
        else
        {
            return_res = GameObject.Instantiate(prefab);
            return_res.name = name;
            return return_res;
        }
    }
    public static void PushToPool(GameObject obj)//занесение в пул
    {
        poolsDictionary[obj.name].AddFirst(obj);
        obj.transform.parent = deactivateObj; //заносим в контейнер
        obj.SetActive(false);
    }
}

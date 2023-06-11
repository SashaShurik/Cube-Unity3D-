using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

//этот скрипт вешаем на непосредственно на камеру

public class MovementObjects : MonoBehaviour
{
    public int ID;
    public int layerObj;
    public bool memoryLayer;
    public float yObj;
    public string nameIgnore;

    public float hitPoint;


    RaycastHit hit;
    Ray ray;

    public GameObject obj;

    void Start()
    {
        memoryLayer = true;
    }

    void Update()
    {
        Motionobject();
    }

    //проверка перетаскиваемых объектов осуществляется по ID в имени объекта
    void Motionobject()
    {
        //проверяем нажатие кнопки мышки
        if (Input.GetMouseButtonDown(0))
        {
            //задаем направление луча
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //пускаем луч бесконечной длинны и если попали куда то, то
            if (Physics.Raycast(ray, out hit))
            {
                //проверяем есть ли в имени десятичные цифры
                var reg = @"[0-9]+";

                //получаем имя того во что попал райкаст и убираем у него приставку ID
                nameIgnore = hit.collider.gameObject.name.Replace("ID", "");

                Debug.Log("Regex.IsMatch(nameIgnore, reg) = " + Regex.IsMatch(nameIgnore, reg));

                if (Regex.IsMatch(nameIgnore, reg))
                {
                    //Debug.Log("hit.collider.name = " + nameIgnore);

                    //и конвектируем в числовое значение
                    ID = Convert.ToInt32(nameIgnore);

                    if (ID != 1)
                    {
                        //запоминаем во что попали лучем, для дальнейшей работы с этим ГО
                        obj = hit.collider.gameObject;
                        Debug.Log("1");
                    }
                }
            }
        }

        //тут мы и будем перемещать наш ГО
        if (obj != null)
        {
            Ray();

            //ставим флаг что уже запомнили слой
            if (memoryLayer)
            {
                //запоминаем слой ГО
                layerObj = obj.layer;
                memoryLayer = false;
            }

            //делаем ГО проницаемым для луча, что бы перемещать его в точку падения луча на терейне
            obj.layer = 2;
            //Debug.Log("hit.point = " + hit.point);

            //присваиваем их объекту на который попал луч
            //перемещаем соответственно только по горизонтальным плоскостям,
            //new Vector3(0, 0.5f, 0); приподнимаем ГО что бы он не сидел в текстурах
            //если подставить за место hit.distance например 10, то ГО будет двигаться на растоянии 10
            obj.transform.position = ray.GetPoint(hit.distance) + new Vector3(0, 0.5f, 0);
        }

        //если кнопка была отжата то перестаем работать с ГО и обнуляем переменную, возвращаем слой обратно какой был
        if (Input.GetMouseButtonUp(0))
        {
            if (obj != null)
            {
                obj.layer = layerObj;
                memoryLayer = true;
                obj = null;
            }
        }
    }

    void Ray()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
    }
}
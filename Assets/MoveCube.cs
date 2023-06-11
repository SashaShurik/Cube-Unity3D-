using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

//���� ������ ������ �� ��������������� �� ������

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

    //�������� ��������������� �������� �������������� �� ID � ����� �������
    void Motionobject()
    {
        //��������� ������� ������ �����
        if (Input.GetMouseButtonDown(0))
        {
            //������ ����������� ����
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //������� ��� ����������� ������ � ���� ������ ���� ��, ��
            if (Physics.Raycast(ray, out hit))
            {
                //��������� ���� �� � ����� ���������� �����
                var reg = @"[0-9]+";

                //�������� ��� ���� �� ��� ����� ������� � ������� � ���� ��������� ID
                nameIgnore = hit.collider.gameObject.name.Replace("ID", "");

                Debug.Log("Regex.IsMatch(nameIgnore, reg) = " + Regex.IsMatch(nameIgnore, reg));

                if (Regex.IsMatch(nameIgnore, reg))
                {
                    //Debug.Log("hit.collider.name = " + nameIgnore);

                    //� ������������ � �������� ��������
                    ID = Convert.ToInt32(nameIgnore);

                    if (ID != 1)
                    {
                        //���������� �� ��� ������ �����, ��� ���������� ������ � ���� ��
                        obj = hit.collider.gameObject;
                        Debug.Log("1");
                    }
                }
            }
        }

        //��� �� � ����� ���������� ��� ��
        if (obj != null)
        {
            Ray();

            //������ ���� ��� ��� ��������� ����
            if (memoryLayer)
            {
                //���������� ���� ��
                layerObj = obj.layer;
                memoryLayer = false;
            }

            //������ �� ����������� ��� ����, ��� �� ���������� ��� � ����� ������� ���� �� �������
            obj.layer = 2;
            //Debug.Log("hit.point = " + hit.point);

            //����������� �� ������� �� ������� ����� ���
            //���������� �������������� ������ �� �������������� ����������,
            //new Vector3(0, 0.5f, 0); ������������ �� ��� �� �� �� ����� � ���������
            //���� ���������� �� ����� hit.distance �������� 10, �� �� ����� ��������� �� ��������� 10
            obj.transform.position = ray.GetPoint(hit.distance) + new Vector3(0, 0.5f, 0);
        }

        //���� ������ ���� ������ �� ��������� �������� � �� � �������� ����������, ���������� ���� ������� ����� ���
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
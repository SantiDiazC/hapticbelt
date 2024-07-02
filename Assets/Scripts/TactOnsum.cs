using Bhaptics.Tact;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TactOnsum : MonoBehaviour
{
    [SerializeField] private int intensity1 = 30;
    [SerializeField] public int duration1 = 100;
    [SerializeField] public int interval1 = 100;

    private float timer = 0.0f;
    private bool timerReached = true;
    public float time_threshold = 0.33f;
    [SerializeField] public float allowableLength = 5;

    public LineRenderer line1;
    public LineRenderer line2;

    public GameObject obj0;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;

    public int tactnum1 = 0;
    public int tactnum2 = 0;

    private int flag = 0;

    public float D1;
    public float D2;
    public float D3;
    public float D4;
    public float D5;
    public float D6;
    public float D7;
    public float D8;
    public float D11;
    public float D12;
    public float D13;
    public float D14;
    public float D15;
    public float D16;
    public float D17;
    public float D18;

    public int IsCount;
    //private bool Flag1 = true;
    //private bool Flag2 = true;

    int angle1 = 0;
    public int preminIndex1 = -1;
    public int preminIndex2 = -1;
    GameObject Gobj;
    // Start is called before the first frame update
    void Start()
    {
        Gobj=GameObject.Find("LeadPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        float[] mindistance1 = new float[8];
        float[] mindistance2 = new float[8];
        GameObject[] arrayObject = new GameObject[] { obj0, obj1, obj2, obj3, obj4, obj5, obj6, obj7 };
        string[] Fartactfile = new string[] { "FarFront1.tact", "FarFront2.tact", "FarFront3.tact", "FarFront4.tact", "FarBack4.tact", "FarBack3.tact", "FarBack2.tact", "FarBack1.tact" };
        string[] Neartactfile = new string[] { "NearFront1.tact", "NearFront2.tact", "NearFront3.tact", "NearFront4.tact", "NearBack4.tact", "NearBack3.tact", "NearBack2.tact", "NearBack1.tact" };

        time_threshold = 0.01f * interval1;
       /* if (Gobj.GetComponent<Distance>().dist < allowableLength)
        {
            //BhapticsSender.Kill();
            BhapticsSender.PlayTheFile(0, 0.01f);
            BhapticsSender.Play(0, 0);
        }*/
        //Debug.Log("angle1: " + angle1);
        for (int index = 0; index < arrayObject.Length; index++)
        {
            mindistance1[index] = DistanceToLine(line1, arrayObject[index].transform.position);
            mindistance2[index] = DistanceToLine(line2, arrayObject[index].transform.position);

        }
        D1 = mindistance1[0];
        D2 = mindistance1[1];
        D3 = mindistance1[2];
        D4 = mindistance1[3];
        D5 = mindistance1[4];
        D6 = mindistance1[5];
        D7 = mindistance1[6];
        D8 = mindistance1[7];
        D11 = mindistance2[0];
        D12 = mindistance2[1];
        D13 = mindistance2[2];
        D14 = mindistance2[3];
        D15 = mindistance2[4];
        D16 = mindistance2[5];
        D17 = mindistance2[6];
        D18 = mindistance2[7];

        tactnum1 = GetMin(mindistance1);
        // tactnum2 = GetMin(mindistance2);
        timer += Time.deltaTime;
        //Debug.Log("timer: " + timer + "time_threshold: " + time_threshold);
        if (timer > time_threshold)
        {
            timerReached = true;
            timer = 0;
        }

        if (IsCount == 0 && flag == 0)  // collision X
        {
            for (int index = 0; index < arrayObject.Length; index++)
            {
                preminIndex1 = -1;
                preminIndex2 = -1;
                angle1 = index;
                if (tactnum1 == index)
                {
                    
                    // BhapticsSender.PlayTheFile(0, 0.01f);
                    if (timerReached == true)
                    {
                    arrayObject[index].GetComponent<Renderer>().material.color = Color.blue;
                    Debug.Log("tactnum1: " + tactnum1);
                    BhapticsSender.Play(intensity1, tactnum1, duration1); // turn on
                    //StartCoroutine(PlayAnim());
                    timerReached = false;
                    }
                }

                else
                {
                    arrayObject[index].GetComponent<Renderer>().material.color = Color.red;
                }

            }
        }

        else  // collision
        {
            /*for (int index = 0; index < arrayObject.Length; index++) 
            { 
            if (tactnum2 == index) // Only turn on tactnum2
            {
                    if (IsCount == 1 && flag == 0)
                    {
                        if (preminIndex1 != tactnum2)
                        {
                            preminIndex2 = -1;
                            BhapticsSender.PlayTheFile(0, 0.01f);
                            BhapticsSender.Register(Fartactfile[index]); //Far
                            BhapticsSender.PlayTheFile(100, 1.0f); // play one time
                            preminIndex1 = tactnum2;
                        }
                    }
                    else if (IsCount == 2 && flag == 0)
                {
                        if (preminIndex2 != tactnum2)
                        {
                            preminIndex1 = -1;
                            BhapticsSender.PlayTheFile(0, 0.01f);
                            preminIndex2 = tactnum2;

                        }
                }

            }

            else { arrayObject[index].GetComponent<Renderer>().material.color = Color.red; }
                
            } //preminIndex = tactnum2; */
            for (int index = 0; index < arrayObject.Length; index++) 
            {
                if (tactnum2 == index)
                {
                    arrayObject[index].GetComponent<Renderer>().material.color = Color.blue;
                    BhapticsSender.PlayTheFile(0, 0.01f);
                    BhapticsSender.Play(intensity1, tactnum2, duration1);
                    Debug.Log("condition set"); 
                }
                else
                {
                    arrayObject[index].GetComponent<Renderer>().material.color = Color.red;
                }
            }

        }
    }


    public void ClickFront()
    {
        
        flag = 1;
        tactnum1 = 3;
        tactnum2 = 3;
        // obj3.GetComponent<Renderer>().material.color = Color.blue;
        BhapticsSender.PlayTheFile(0, 0.01f);
        //BhapticsSender.Register("Front_pattern.tact");
        BhapticsSender.PlayTheFile(100, 1.0f);
        //arrayObject[3].GetComponent<Renderer>().material.color = Color.red;
    }
    public void ClickBack() 
    {
        flag = 1;
        tactnum1 = 3;
        tactnum2 = 5;
        BhapticsSender.PlayTheFile(0, 0.01f);
        //BhapticsSender.Register("Back_pattern.tact");
        BhapticsSender.PlayTheFile(100, 1.0f);
    }
    public void ClickRight()
    {
        flag = 1;
        tactnum1 = 3;
        tactnum2 = 0;
        BhapticsSender.PlayTheFile(0, 0.01f);
        //BhapticsSender.Register("Side_pattern.tact");
        BhapticsSender.PlayTheFile(100, 1.0f);
    }
    public void ClickLeft()
    {
        flag = 1;
        tactnum1 = 3;
        tactnum2 = 7;
        BhapticsSender.PlayTheFile(0, 0.01f);
        //BhapticsSender.Register("Side_pattern.tact");
        BhapticsSender.PlayTheFile(100, 1.0f);
    }

    public void ClickReset()
    {
        flag = 0;
        preminIndex1 = -1;
        preminIndex2 = -1;
        IsCount = 0;
    }

    public void ClickShutdown()
    {
        flag = 1;
        BhapticsSender.PlayTheFile(0, 0.01f);
    }


    public static float DistanceToLine(LineRenderer line, Vector3 point)
    {
        float length = Vector3.Distance(line.GetPosition(1),line.GetPosition(0));
        return Vector3.Cross((line.GetPosition(1)-line.GetPosition(0))/length, point - line.GetPosition(0)).magnitude;
    }

    public int GetMin(float[] array)
    {
        int tactnum = 0;
        float min = array[0];

        for (int i = 0; i < array.Length; i++)
        {
            if (min > array[i])
            {
                min = array[i];
                tactnum = i ;
            }
        }
        return tactnum;
    }
}

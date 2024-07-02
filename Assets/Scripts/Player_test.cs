using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    SerialPort seri = new SerialPort("COM3", 921600);
    public static float rotation;
    public static float xpos;
    public static float ypos;
    bool isIMU = true;

    private Thread readingImu;
    Vector3 yAxisRotation;

    void Start()
    {
        seri.Open();
        Debug.Log("1 opened");
        readingImu = new Thread(ReadingRotation);
        readingImu.Start();
    }



    private void ReadingRotation(object obj)
    {
        string[] item;
        int item_counter = 0;

        while (isIMU)
        {


            item = seri.ReadLine().Split(',');


            if (item[0].IndexOf('*') != -1) { item_counter = 0; item[0] = item[0].Replace("*", ""); }
            else if (item[0].IndexOf('-') != -1) { item_counter = 1; } // skip ch-id


            string[] imsi = item[item_counter].Split('.');
            float[] quat = new float[4];
            float[] angle = new float[3];


            if (imsi[1].Length == 4) // quaternion
            {
                if (item.Length >= item_counter + 4)  // chid,x,y,z,w
                {
                    quat[1] = float.Parse(item[item_counter++]);
                    quat[2] = float.Parse(item[item_counter++]);
                    quat[0] = float.Parse(item[item_counter++]);
                    quat[3] = float.Parse(item[item_counter++]);

                }
            }
            else //if (imsi[1].Length == 2) // euler
            {
                if (item.Length >= item_counter + 3)  // chid,x,y,z
                {
                    angle[0] = float.Parse(item[item_counter++]);
                    angle[1] = float.Parse(item[item_counter++]);
                    angle[2] = float.Parse(item[item_counter++]);
                }
            }

            Quaternion raw0 = new Quaternion(quat[0], quat[1], quat[2], quat[3]);
            //raw0 = Quaternion.Euler(new Vector3(-180, 0, 0))* raw0;
            Quaternion a0 = Quaternion.Euler(new Vector3(0, -90, 0)) * Quaternion.Euler(new Vector3(0, 0, -90)) * raw0;
            Quaternion b0 = new Quaternion(a0.z, a0.y, a0.x, a0.w);
            yAxisRotation = b0.eulerAngles;
            /*
            Quaternion raw0 = new Quaternion(quat[0], quat[1], quat[2], quat[3]);
            Quaternion a0 = Quaternion.Euler(new Vector3(0, -90, 0)) * Quaternion.Euler(new Vector3(0, 0, -90)) * raw0;
            Quaternion b0 = new Quaternion(a0.z, a0.y, a0.x, a0.w);
            yAxisRotation = b0.eulerAngles;*/
        }
    }

    void Update()
    {
        
        //Vector3 yAxisRotation2 = new Vector3(0, yAxisRotation.y - seinbuffer, 0);
        Vector3 yAxisRotation2 = new Vector3(yAxisRotation.x, yAxisRotation.y - cali_buffer, yAxisRotation.z);
        //Debug.Log("Current Buffer: " + cali_buffer);
        transform.eulerAngles = yAxisRotation2;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(0.0f, 0.0f, -0.01f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.0f, 0.0f, +0.01f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(+0.01f, 0.0f, 0.0f);
        }
    }

    private void OnDestroy()
    {
        isIMU = false;

        if (readingImu != null)
        {
            readingImu.Abort();
        }

        if (seri.IsOpen)
        {
            seri.Close();
        }

    }
    Thread cali;
    float cali_buffer = 0.0f;

    public void calibrationReset()
    {
        cali = new Thread(caliProc);
        cali.Start();
    }

    private void caliProc(object obj)
    {
        cali_buffer = yAxisRotation.y - 90.0f; // pointing to the same orientation (right gamescreen) seinbuffer = yAxisRotation.y; 
        //Debug.Log("Calibration buffer: " + seinbuffer);


    }

}
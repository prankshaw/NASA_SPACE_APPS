using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class rainManagement : MonoBehaviour {
    private class WaterLevel
    {
        public float minHeight, maxHeight, dangerHeight, volume, speed, currentHeight, constant;
        public WaterLevel(float minHeight,float maxHeight,float dangerHeight,float volume,float constant)
        {
            this.currentHeight = this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.dangerHeight = dangerHeight;
            this.volume = volume;
            this.constant = constant;

     
        }
    }
    // Use this for initialization
    WaterLevel water_plain_100, water_plain_80, water_plain_60, water_plain_40, water_plain_20;
    Slider s;
    Toggle t100, t80, t60, t40, t20;
    RainScript rscript;

    string url;
    void Start () {

        water_plain_100 = new WaterLevel(90, 97, 95, 100, 0);
        water_plain_80 = new WaterLevel(70, 77.8f, 95, 200, 0.001f);
        water_plain_60 = new WaterLevel(50, 58.55f, 95, 250, 0.001f);
        water_plain_40 = new WaterLevel(30, 37.7f, 95, 300, 0.001f);
        water_plain_20 = new WaterLevel(10, 18, 95, 200, 0.001f);

        s = GameObject.Find("Slider").GetComponent<Slider>();
        t100 = GameObject.Find("f100").GetComponent<Toggle>();
        t80 = GameObject.Find("f080").GetComponent<Toggle>();
        t60 = GameObject.Find("f060").GetComponent<Toggle>();
        t40 = GameObject.Find("f040").GetComponent<Toggle>();
        t20 = GameObject.Find("f020").GetComponent<Toggle>();
        rscript = GameObject.Find("RainPrefab").GetComponent<RainScript>();


        url = "http://localhost:8000/wsp/";

    }
    bool raining = false; 
	// Update is called once per frame
    private class Rain
    {
        float atl100, atl80, atl60, atl40, atl20;
        public Rain(float atl100, float atl80, float atl60, float atl40, float atl20)
        {
            this.atl100 = atl100;
            this.atl80 = atl80;
            this.atl60 = atl60;
            this.atl40 = atl40;
            this.atl20 = atl20;

        }
    }
    bool o_pressed, o_released;
    float time = 0;
    void Update () {
        
        if (s.value != 0)
        {

            rscript.RainIntensity = 1;//(float)s.value/10.0f;
            //print("Its raining");
            float intensity = (float)s.value / s.maxValue;
            float[] intake = { 0f, 0f, 0f, 0f, 0f }, outtake = { 0f, 0f, 0f, 0f, 0f};
            // region 1
            if (t100.isOn)
            {
                // increase the rain value of top layer intake
                intake[0] = water_plain_100.volume * intensity;
                outtake[0] = water_plain_100.volume * intensity;
                // increase height of the water layer gradually 
                water_plain_100.currentHeight = water_plain_100.minHeight + (water_plain_100.maxHeight - water_plain_100.minHeight) * intensity;
            }

            float dfact=1000f;
            // region 2
            
                // input
                intake[1] = outtake[0];
                if (t80.isOn) intake[1] += water_plain_80.volume * intensity;

                // output
                if (water_plain_80.currentHeight >= water_plain_80.maxHeight) outtake[1] = intake[1];
                else outtake[1] = water_plain_80.constant * (water_plain_80.currentHeight - water_plain_80.minHeight) / (water_plain_80.maxHeight - water_plain_80.minHeight);

                //height
                water_plain_80.currentHeight += (intake[1] - outtake[1]) / (water_plain_80.volume*dfact);

            // region 3
                // input
                intake[2] = outtake[1];
                if (t60.isOn) intake[2] += water_plain_60.volume * intensity;

                // output
                if (water_plain_60.currentHeight >= water_plain_60.maxHeight) outtake[2] = intake[2];
                else outtake[2] = water_plain_60.constant * (water_plain_60.currentHeight - water_plain_60.minHeight) / (water_plain_60.maxHeight - water_plain_60.minHeight);

                //height
                water_plain_60.currentHeight += (intake[1] - outtake[1]) / (water_plain_60.volume*dfact);

            // region 4
            // input
            intake[3] = outtake[2];
            if (t40.isOn) intake[3] += water_plain_40.volume * intensity ;

            // output
            if (water_plain_40.currentHeight >= water_plain_40.maxHeight) outtake[3] = intake[3];
            else outtake[3] = water_plain_40.constant * (water_plain_40.currentHeight - water_plain_40.minHeight) / (water_plain_40.maxHeight - water_plain_40.minHeight);

            //height
            water_plain_40.currentHeight += (intake[3] - outtake[3]) / (dfact*water_plain_40.volume);


            // region 5
            // input
            intake[4] = outtake[3];
            if (t20.isOn) intake[4] += water_plain_20.volume * intensity;

            // output
            if (water_plain_20.currentHeight >= water_plain_20.maxHeight) outtake[4] = intake[4];
            else outtake[4] = water_plain_20.constant * (water_plain_20.currentHeight - water_plain_20.minHeight) / (water_plain_20.maxHeight - water_plain_20.minHeight);

            //height
            water_plain_20.currentHeight += (intake[4] - outtake[4]) / (dfact*water_plain_20.volume);



            





        }
        else
        {
            rscript.RainIntensity = 0f;
            //print("Its not raining");
        }
        update("Waterfall", water_plain_100.currentHeight);
        update("Waterfall1", water_plain_80.currentHeight);
        update("Waterfall2", water_plain_60.currentHeight);
        update("Waterfall3", water_plain_40.currentHeight);
        update("Waterfall4", water_plain_20.currentHeight);

        time += Time.deltaTime;
        if ((int)time % 2000 == 0)
        {
            StartCoroutine(getRequest(url+"?point=1"+"&cl="+water_plain_100.currentHeight.ToString()+"&dl="+water_plain_100.dangerHeight));
            StartCoroutine(getRequest(url + "?point=2" + "&cl=" + water_plain_80.currentHeight.ToString() + "&dl=" + water_plain_80.dangerHeight));
            StartCoroutine(getRequest(url + "?point=3" + "&cl=" + water_plain_60.currentHeight.ToString() + "&dl=" + water_plain_60.dangerHeight));
            StartCoroutine(getRequest(url + "?point=4" + "&cl=" + water_plain_40.currentHeight.ToString() + "&dl=" + water_plain_40.dangerHeight));
            StartCoroutine(getRequest(url + "?point=5" + "&cl=" + water_plain_20.currentHeight.ToString() + "&dl=" + water_plain_20.dangerHeight));
        }

    }
    void update(string name, float height)
    {
        GameObject.Find(name).transform.position = new Vector3(
            GameObject.Find(name).transform.position.x,
            height,
            GameObject.Find(name).transform.position.z);
        print(GameObject.Find(name).transform.position.y);
    }


    //float time = 0;
    IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
        //time += Time.deltaTime;
    }
}

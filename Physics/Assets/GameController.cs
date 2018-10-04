using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<CubeController> cubes;

    public bool recording = false;

    public bool running = false;
    private bool looping = false;

    public int frame = 0;
    public int maxFrame = 0;

    public Slider timeSl;
    public Button startBt;
    public Button nextBt;
    public Button lastBt;

    public Rigidbody Bullet;

    // Use this for initialization
    void Awake () {
        cubes = new List<CubeController>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if (running)
        {
            frame = cubes[0].GetFrame();
            maxFrame = cubes[0].GetMaxFrame();

            if(maxFrame > 0)
            {
                timeSl.value = (float)frame / (float)maxFrame;
            }

            if (!recording && frame >= maxFrame)
            {
                if (looping)
                {
                    frame = 0;
                    foreach (var item in cubes)
                    {
                        item.StartPlayback(frame);
                    }
                }
                else
                {
                    Stop();
                }
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Bullet.transform.position = Camera.main.transform.position;
            Bullet.transform.rotation = Camera.main.transform.rotation;
            Bullet.velocity = Vector3.zero;
            Bullet.AddForce(Bullet.transform.forward * 100000000);
            Bullet.AddForce(Bullet.transform.forward * 100);
        }

        if (Input.GetKey(KeyCode.G) && recording && running)
        {
            foreach (CubeController item in cubes)
            {
                item.SetGravity(true);
            }
        }

    }

    public void StartPlayback()
    {
        if (running)
        {
            Stop();
            return;
        }
        recording = false;
        if(frame >= maxFrame)
        {
            frame = 0;
        }
        foreach (var item in cubes)
        {
            item.StartPlayback(frame);
        }

        running = true;
        SetupLight();
    }

    public void StartRecording()
    {
        if (running)
        {
            Stop();
            return;
        }
        
        frame = cubes[0].GetMaxFrame();
        if (frame < 0)
            frame = 0;
        recording = true;

        foreach (var item in cubes)
        {
            item.SetFrame(frame);
            item.StartRecording();
        }

        running = true;
        SetupLight();
    }

    public void Stop()
    {
        foreach (var item in cubes)
        {
            item.Stop();
        }

        running = false;
    }

    public void NextFrame()
    {
        if (!running)
        {
            if(frame + 1 < maxFrame)
            {
                frame++;
                foreach (var item in cubes)
                {
                    item.SetFrame(frame);
                }
            }
        }
    }

    public void LastFrame()
    {
        if (!running)
        {
            if (frame - 1 >= 0)
            {
                frame--;
                foreach (var item in cubes)
                {
                    item.SetFrame(frame);
                }
            }
        }
    }

    public void ChangeTime()
    {
        if (!running || (running && !recording))
        {
            frame = (int)(maxFrame * timeSl.value);

            foreach (var item in cubes)
            {
                item.SetFrame(frame);
            }
        }
    }




    public void OnChangeLoop(bool value)
    {
        looping = value;
    }

    void SetupLight()
    {
        Light light = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();

        if (recording)
        {
            light.shadows = LightShadows.None;
            light.bounceIntensity = 0;
            foreach (var item in cubes)
            {
                item.GetComponent<MeshRenderer>().receiveShadows = false;
            }
        }
        else
        {
            light.shadows = LightShadows.Soft;
            light.bounceIntensity = 1;
            foreach (var item in cubes)
            {
                item.GetComponent<MeshRenderer>().receiveShadows = true;
            }
        }
    }

    public void OnUpdateSpeed(Slider slider)
    {
        foreach (CubeController item in cubes)
        {
            item.speed = (int)slider.value;
        }
    }
}

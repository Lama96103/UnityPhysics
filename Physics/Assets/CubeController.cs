using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    List<PointInTime> pointInTime;

    public bool recording = false;
    public bool playback = false;

    public int speed = 2;

    int frame = 0;

    public Rigidbody rig;
    public Collider coll;

    public bool shouldDeactivateOnSpawn = true;

    // Use this for initialization
    void Start () {
        pointInTime = new List<PointInTime>();
        rig = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().cubes.Add(this);
        rig.velocity = Vector3.zero;

        if(shouldDeactivateOnSpawn)
            Stop();
	}

    private void Update()
    {
        if (playback)
        {
            transform.position = pointInTime[frame].location;
            transform.rotation = pointInTime[frame].rotation;

            frame += speed;
            if (frame >= pointInTime.Count)
            {
                Stop();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (recording)
        {
            pointInTime.Add(new PointInTime(transform.position, transform.rotation));
            frame++;
        }
    }
    

    public void StartRecording()
    {
        recording = true;
        playback = false;
        rig.isKinematic = false;
        rig.detectCollisions = true;
        coll.enabled = true;
    }

    public void Stop()
    {
        recording = false;
        playback = false;
        rig.isKinematic = true;
        rig.detectCollisions = false;
        coll.enabled = false;
    }

    public void StartPlayback(int frame)
    {
        this.frame = frame;
        recording = false;
        playback = true;
        rig.isKinematic = true;
        rig.detectCollisions = false;
        coll.enabled = false;
    }

    public int GetMaxFrame()
    {
        if (pointInTime == null) return 0;
        return pointInTime.Count-1;
    }

    public int GetFrame()
    {
        return frame;
    }

    public void SetFrame(int frame)
    {
        this.frame = frame;
        if(frame >= 0 && frame < pointInTime.Count)
        {
            transform.position = pointInTime[frame].location;
            transform.rotation = pointInTime[frame].rotation;
        }

    }

    public void SetGravity(bool val)
    {
        rig.useGravity = val;
    }
}

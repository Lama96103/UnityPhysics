using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    List<Vector3> location;
    List<Quaternion> rotation;

    public bool recording = false;
    public bool playback = false;

    public int speed = 2;

    int frame = 0;

    public Rigidbody rig;
    Collider coll;

    // Use this for initialization
    void Start () {
        location = new List<Vector3>();
        rotation = new List<Quaternion>();
        rig = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        rig.isKinematic = true;
        rig.detectCollisions = false;
        coll.enabled = false;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().cubes.Add(this);
        rig.velocity = Vector3.zero;
        Stop();
	}

    private void Update()
    {
        if (playback)
        {
            transform.position = location[frame];
            transform.rotation = rotation[frame];

            frame += speed;
            if (frame >= location.Count)
            {
                Stop();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (recording)
        {
            location.Add(transform.position);
            rotation.Add(transform.rotation);
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
        if (location == null) return 0;
        return location.Count-1;
    }

    public int GetFrame()
    {
        return frame;
    }

    public void SetFrame(int frame)
    {
        this.frame = frame;
        if(frame >= 0 && frame < location.Count)
        {
            transform.position = location[frame];
            transform.rotation = rotation[frame];
        }

    }

    public void SetGravity(bool val)
    {
        rig.useGravity = val;
    }
}

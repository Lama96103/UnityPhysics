using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime {

    public Vector3 location;
    public Quaternion rotation;

    public PointInTime(Vector3 location, Quaternion rotation)
    {
        this.location = location;
        this.rotation = rotation;
    }
}

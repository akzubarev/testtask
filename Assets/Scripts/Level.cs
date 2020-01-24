using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level
{

    public int limit = 0;
    public int rest = 0;
    public bool done = false;
    public Vector3 start, end;
    public List<Balk> balks;

    public Level(List<Balk> b, Vector3 s, Vector3 e, int l)
    {
        balks = b;
        start = s;
        end = e;
        limit=l;
        rest=l;
    }

}

[System.Serializable]
public class Balk
{
    public Vector2 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Balk(Vector2 p, Quaternion r, Vector3 s)
    { position = p; rotation = r; scale = s;}
}

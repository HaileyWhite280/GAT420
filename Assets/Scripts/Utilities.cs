using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        //???
        if(result.x > max.x)
        {
            result = min.x;
        }
        if(result.x < min.x)
        {
            result = max.x;
        }

        return result;
    }
}

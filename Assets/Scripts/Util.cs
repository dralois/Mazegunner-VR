using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{

    #region Random Util

    
    public static int RandomInt(int a, int b, List<int> exceptions = null)
    {

        return Random.Range(a, b + 1);
    }

    public static Vector3 RandomVector3(Vector3 a, Vector3 b, float distance, List<Vector3> exceptions = null)
    {
        int safetyCounter = 2;
        int counter = 5;

        Vector3 res = RandomVector3(a, b);
        bool isOk = false;
        while (safetyCounter > 0 && !isOk)
        {
            counter--;
            if (counter <= 0)
            {
                distance /= 2;
                counter = 5;
                safetyCounter--;
            }

            isOk = true;
            if (exceptions != null && exceptions.Count != 0)
            {
                foreach (Vector3 pos in exceptions)
                {
                    if ((pos - res).magnitude <= distance)
                    {
                        res = RandomVector3(a, b);
                        isOk = false;
                        break;
                    }
                }
            }
        }
        return res;
    }

    public static Vector3 RandomVector3(Vector3 a, Vector3 b)
    {
        float x = Random.Range(a.x, b.x);
        float y = Random.Range(a.y, b.y);
        float z = Random.Range(a.z, b.z);
        return new Vector3(x, y, z);
    }
    #endregion

    public static float Slerp(float a, float b, float t)
    {
        Vector3 va = new Vector3(a, 0, 0);
        Vector3 vb = new Vector3(b, 0, 0);
        return Vector3.Slerp(va, vb, t).x;

    }
    #region region Checks
    public static bool InBetween(float value, float a, float b)
    {
        if (a < b)
        {
            return value >= a && value <= b;
        }
        else
        {
            return value <= a && value >= b;
        }
    }

    public static float SetInBetween(float value, float a, float b)
    {

        if (a > b)
        {
            if (a <= value)
            {
                return a;
            }
            if (b >= value)
            {
                return b;
            }
        }
        else
        {
            if (b <= value)
            {
                return b;
            }
            if (a >= value)
            {
                return a;
            }
        }
        return value;
    }

    public static bool InBetween(Vector3 value, Vector3 a, Vector3 b)
    {
        bool isX = InBetween(value.x, a.x, b.x);
        bool isY = InBetween(value.y, a.y, b.y);
        bool isZ = InBetween(value.z, a.z, b.z);
        return isX && isY && isZ;
    }

    public static Vector3 SetInBetween(Vector3 value, Vector3 a, Vector3 b)
    {
        float x = SetInBetween(value.x, a.x, b.x);
        float y = SetInBetween(value.y, a.y, b.y);
        float z = SetInBetween(value.z, a.z, b.z);
        return new Vector3(x, y, z);
    }

    #endregion

    #region Input
    public static float GetInputAxisSafe(string inputString, int playerNumber)
    {
        if (inputString.Length == 0)
            return 0;
        return Input.GetAxis(inputString + playerNumber);
    }

    public static float GetInputAxisSafe(string inputString)
    {
        if (inputString.Length == 0)
            return 0;
        return Input.GetAxis(inputString);
    }
    #endregion
}

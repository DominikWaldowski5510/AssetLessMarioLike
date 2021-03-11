using System;
using UnityEngine;

//Parabola function class
public static class Parabola 
{
    //The parabola function
    public static Vector3 ParabolaFunc(Vector3 _startPos, Vector3 _endPos, float _height, float _time)
    {
        Func<float, float> f = x => -4 * _height * x * x + 4 * _height * x;
        var mid = Vector3.Lerp(_startPos, _endPos, _time);
        return new Vector3(mid.x, f(_time) + Mathf.Lerp(_startPos.y, _endPos.y, _time), mid.z);
    }
}

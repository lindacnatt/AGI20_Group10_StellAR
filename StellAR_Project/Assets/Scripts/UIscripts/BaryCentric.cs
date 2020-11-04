using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaryCentric {
    public static Vector3 getWeights(Vector3 pos, Vector3[] triangle){ //calculate barycentric weights from pos
        float A =  HeronsArea(triangle[0], triangle[1], triangle[2]);
        float w1, w2, w3;
        w1 = HeronsArea(pos, triangle[0], triangle[1]) / A;
        w2 = HeronsArea(pos, triangle[1], triangle[2]) / A;
        w3 = HeronsArea(pos, triangle[2], triangle[0]) / A;

        return new Vector3(w1, w2, w3);
    }

    private static float CalcP(float a, float b, float c){ // for use in Herons formula
        return (a + b + c)*0.5f; 
    }

    private static float HeronsArea(Vector3 p1, Vector3 p2, Vector3 p3){ //formula for Area of triangle
        float a, b, c; // length of sides of trianglesb  
        a = (p1 - p2).magnitude;
        b = (p2 - p3).magnitude;
        c = (p3 - p1).magnitude;

        float p = CalcP(a, b, c);
        float area = Mathf.Sqrt(p*(p-a)*(p-b)*(p-c)); 
        return area;
    }
}

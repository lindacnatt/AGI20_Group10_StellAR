using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoSphere {
    Mesh mesh;
    int detail;
    ShapeGenerator shapeGenerator;
    float radius;
    Dictionary<string, Vector3> midPointCach;
    List<Vector3> vertices;
    float theta;
    CraterGenerator craterGenerator;

    public IcoSphere(ShapeGenerator shapeGenerator, float radius, int detail, Mesh mesh, CraterGenerator craterGenerator)
    {
        this.shapeGenerator = shapeGenerator;
        this.detail = detail;
        this.mesh = mesh;
        this.radius = radius;
        this.craterGenerator = craterGenerator;
        this.vertices = new List<Vector3>();
        this.theta = (1 + Mathf.Sqrt(5))*0.5f; //golden ratio
        this.midPointCach = new Dictionary<string, Vector3>();
    }

    public void ConstructMesh(){
        List<Vector3Int> triangles = new List<Vector3Int>();
        List<int> triangleIndices = new List<int>();
        /* Vector2 sideLen  = new Vector2(radius, theta*radius);
        sideLen /= sideLen.magnitude;
        sideLen *= radius;
        */

        // scale so that every vertice is r length
        /*
        float r = sideLen[0];
        theta = sideLen[1];
        */
        
        float r = 1.0f;
        // construct basic vertices
        Vector3 pointOnUnitSphere = new Vector3(-r, theta, 0).normalized;
        float craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(r, theta, 0).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(-r, -theta, 0).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(r, -theta, 0).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));

        pointOnUnitSphere = new Vector3(0, -r, theta).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(0, r, theta).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(0, -r, -theta).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(0, r, -theta).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));

        pointOnUnitSphere = new Vector3(theta, 0, -r).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(theta, 0, r).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(-theta, 0, -r).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        pointOnUnitSphere = new Vector3(-theta, 0, r).normalized;
        craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        vertices.Add(shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere, craterHeight));
        
        /*
        vertices.Add(new Vector3(-r, theta, 0));
        vertices.Add(new Vector3(r, theta, 0));
        vertices.Add(new Vector3(-r, -theta, 0));
        vertices.Add(new Vector3(r, -theta, 0));

        vertices.Add(new Vector3(0, -r, theta));
        vertices.Add(new Vector3(0, r, theta));
        vertices.Add(new Vector3(0, -r, -theta));
        vertices.Add(new Vector3(0, r, -theta));

        vertices.Add(new Vector3( theta,0, -r));
        vertices.Add(new Vector3( theta, 0, r));
        vertices.Add(new Vector3(-theta, 0, -r));
        vertices.Add(new Vector3(-theta, 0, r));
        */

        // construct triangles
        triangles.Add(new Vector3Int(0, 11, 5));
        triangles.Add(new Vector3Int(0, 5, 1));
        triangles.Add(new Vector3Int(0, 1, 7));
        triangles.Add(new Vector3Int(0, 7, 10));
        triangles.Add(new Vector3Int(0, 10, 11));

        triangles.Add(new Vector3Int(1, 5, 9));
        triangles.Add(new Vector3Int(5, 11, 4));
        triangles.Add(new Vector3Int(11, 10, 2));
        triangles.Add(new Vector3Int(10, 7, 6));
        triangles.Add(new Vector3Int(7, 1, 8));

        triangles.Add(new Vector3Int(3, 9, 4));
        triangles.Add(new Vector3Int(3, 4, 2));
        triangles.Add(new Vector3Int(3, 2, 6));
        triangles.Add(new Vector3Int(3, 6, 8));
        triangles.Add(new Vector3Int(3, 8, 9));

        triangles.Add(new Vector3Int(4, 9, 5));
        triangles.Add(new Vector3Int(2, 4, 11));
        triangles.Add(new Vector3Int(6, 2, 10));
        triangles.Add(new Vector3Int(8, 6, 7));
        triangles.Add(new Vector3Int(9, 8, 1));
        
        for(int i = 0; i < detail; i++){
            Vector3 a, b, c;
            List<Vector3Int> tempList = new List<Vector3Int>();
            int len;
            foreach(Vector3Int triangle in triangles){
                a = GetMidPoint(vertices[triangle.x], vertices[triangle.y]);
                b = GetMidPoint(vertices[triangle.y], vertices[triangle.z]);
                c = GetMidPoint(vertices[triangle.z], vertices[triangle.x]);

                // add new vertices to list
                float craterHeightA = craterGenerator.CalculateCraterDepth(a);
                float craterHeightB = craterGenerator.CalculateCraterDepth(a);
                float craterHeightC = craterGenerator.CalculateCraterDepth(a);
                AddVertex(shapeGenerator.CalculatePointOnPlanet(a, craterHeightA));
                AddVertex(shapeGenerator.CalculatePointOnPlanet(b, craterHeightB));
                AddVertex(shapeGenerator.CalculatePointOnPlanet(c, craterHeightC));
                
                /*
                AddVertex(a);
                AddVertex(b);
                AddVertex(c);
                */
                
                len = vertices.Count;
                tempList.Add(new Vector3Int(len-3, len-2, len-1)); //add a b c triangle
                tempList.Add(new Vector3Int(triangle.x, len-3, len-1));
                tempList.Add(new Vector3Int(triangle.y, len-2, len-3));
                tempList.Add(new Vector3Int(triangle.z, len-1, len-2));
            }
            triangles = tempList;
        }

        for(int i = 0; i < triangles.Count; i++){
            triangleIndices.Add(triangles[i].x);
            triangleIndices.Add(triangles[i].y);
            triangleIndices.Add(triangles[i].z);
        }
        
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangleIndices.ToArray();
        mesh.RecalculateNormals(); 
    }

    private Vector3 GetMidPoint(Vector3 a, Vector3 b){
        string key = a.ToString() + b.ToString();
        if(midPointCach.ContainsKey(key)){
            return midPointCach[key];
        }
        else{
            Vector3 temp = b - a;
            temp = a + temp*0.5f;
            temp = temp.normalized; // * radius;
            midPointCach.Add(key, temp);
            return temp;
        }
    }
    private void AddVertex(Vector3 point){
        this.vertices.Add(point);
    }
}

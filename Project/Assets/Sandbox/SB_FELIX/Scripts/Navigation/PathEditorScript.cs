using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEditorScript : MonoBehaviour
{

    public Color rayColor = Color.white; // The Colour of the path in the editor
    public List<Transform> path_objs = new List<Transform> ();
    Transform[] theArray;


    void OnDrawGizmos()
    {
        Gizmos.color = rayColor; //Set the color of the Gizmo to white
        theArray = GetComponentsInChildren<Transform>(); // Array looking through children of main object
        path_objs.Clear();

        foreach (Transform path_obj in theArray)
        {
            if(path_obj != this.transform)
            {
                path_objs.Add(path_obj);  //Add path obj to objs list
            }
     
       }
        for (int i = 0; i < path_objs.Count; i++)
        {
            Vector3 position = path_objs[i].position;
            if(i > 0)
            {
                Vector3 previous = path_objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f); //Draw the sphere on current position
            }
        }
    }





}

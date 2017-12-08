
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadRotate : MonoBehaviour
{
    public GameObject LoadImage;
public int speed;
 void Start () {
     speed = 10;
 }

 void Update () 
 {
    LoadImage.transform.Rotate(new Vector3(0, 1, 0), 10f); 
 }
}
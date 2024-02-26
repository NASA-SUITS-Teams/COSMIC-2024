using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cmath;
public class Coordinate : MonoBehaviour {
    protected:
        double xCoordinate;
        double yCoordinate;
        double pointing;
    public:
        void setX();
        void setY();
        double getX(); 
        double getY();
        double getangle();
    private:

    Coordinate(){
        xCoordinate = 0;
        yCoordinate = 0;
    }
    Coordinate(double x, double y){
        xCoordinate = x;
        yCoordinate = y;
    }

    void setX(double x){
        xCoordinate = x;
    }
    void setY(double y){
        yCoordinate = y;
    }

    double getX(){
        return xCoordinate;
    }
    double getY(){
        return yCoordinate;
    }

    double getangle(){
        double dot = xCoordinate*getX + yCoordinate*getY;
        double det = xCoordinate*getY - yCoordinate*getX;
        pointing = Math.Atan2(det,dot)/(Math.PI/180);
        return pointing
    }
}

//Pin Feature that allows User to place it
//Allow for Unity 
public class Pin : public Coordinate{
    private string locName;

    public:
        void setName;
        string getName;
    
    Pin(){
        locName = "";
        xCoordinate = 0;
        yCoordinate = 0;
    }
    Pin(string name, double x, double y){
        locName = name;
        xCoordinate = x;
        yCoordinate = y;
    }

    void setName(string name){
        locName = name;
    }

    string getName(){
        return name;
    }
}

public class Route{

}
public class NAV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public void changeFIVEVAR() :   [5 variations of same function]
        //if variable of status is false, set gameobject to true
        // let data rate change
        //else (if true), set to false
        // data won't change or will contantly rise/fall

    // Update is called once per frame
    void Update()
    {
        
    }
}
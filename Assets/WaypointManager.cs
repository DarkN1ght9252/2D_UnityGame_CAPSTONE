using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public List<Transform> waypoints;
    private StateController[] _controllers;
    void Start()
    {
        _controllers = FindObjectsOfType<StateController>();
        foreach (var controller in _controllers){
            controller.InitializeAI(true, waypoints);
        }
    }
}

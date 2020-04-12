using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public static class _StaticGameManager
{
    static ChangeNameOnStart myDDOL;
    public static class EventParsing
    {
        static Scene currentScene = SceneManager.GetSceneAt(0);
        public static List<string> eventList = new List<string>();
        public static List<int> eventListDuration = new List<int>();

        //need a way of telling how long an event is going to last. use regex on eventName with a variable to change each scene's start phase?
        public static void AddEvent(string eventName, int duration) {
            if (duration <= 0) {
                duration = 1;
            }
            eventList.Add(eventName);
            Debug.Log("Added event " + eventName);
            int i = eventList.IndexOf(eventName);
            eventListDuration.Insert(i, duration);
            Debug.Log("Inserted Duration for " + eventName + " of " + duration + " at eventlist index " + i);
            Debug.Log("CheckTest: Event = " + eventList[i] + " with a duration of " + eventListDuration[i]);
        }

        static void ReduceEvent(string eventName) {
            int i = eventList.IndexOf(eventName);
            eventListDuration[i] = eventListDuration[i] - 1;
            if (eventListDuration[i] <= 0) {
                Debug.Log("Duration for event " + eventName + " has found to be lower or equal to 0, and is bieng removed");
                eventListDuration.RemoveAt(i);
                eventList.RemoveAt(i);
            }
        }
        public static void EventParse() {
            if (!myDDOL)
                myDDOL = GameObject.FindGameObjectWithTag("Consistent").GetComponent<ChangeNameOnStart>();
            for (int i = 0; i < eventList.Count; i++) {
                myDDOL.EventParser(eventList[i]);
                if (currentScene != SceneManager.GetActiveScene()) {
                    ReduceEvent(eventList[i]);
                    currentScene = SceneManager.GetActiveScene();
                }
            }
        }
    }

    public static class PlayerStats
    {
        public static bool keepLighter = false;
    }

    public static class Scenes
    {
        static int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        public static string[] scenes { get; private set; }

        public static void CalcScenes() {
            scenes = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++) {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            }
        }
    }
    public static class Doors
    {
        public static float DoorsOpened { get; private set; }
        public static string DoorsOpenedString { get; private set; }
        static bool useCustomString = false;
        public static int doorsUntilEnd { private set; get; } = 1;

        public static void _AddDoor() {
            DoorsOpened++;
            DoorStringCheck();
        }

        public static void _RemoveDoor() {
            DoorsOpened--;
            DoorStringCheck();
        }

        static void DoorStringCheck() {
            if (!useCustomString) {
                DoorsOpenedString = DoorsOpened.ToString("F0");
            }
            if (!myDDOL)
                myDDOL = GameObject.FindGameObjectWithTag("Consistent").GetComponent<ChangeNameOnStart>();
            myDDOL.ParseDoorChange();
        }

        public static void _SetDoorString(string myString) {
            useCustomString = true;
            DoorsOpenedString = myString;
            DoorStringCheck();
        }

        public static void _UnsetDoorString() {
            useCustomString = false;
            DoorsOpenedString = DoorsOpened.ToString("F0");
        }
    }
    public static void ChangeLayersRecursively(this Transform trans) {
        trans.gameObject.layer = 0;
        foreach (Transform child in trans) {
            child.ChangeLayersRecursively();
        }
    }
}

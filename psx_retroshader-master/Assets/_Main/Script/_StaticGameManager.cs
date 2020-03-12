using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class _StaticGameManager {
    public static class EventParsing {
        public static List<string> eventList = new List<string>();
        public static List<int> eventListDuration = new List<int>();
        static ChangeNameOnStart myDDOL;

        //need a way of telling how long an event is going to last. use regex on eventName with a variable to change each scene's start phase?
        public static void AddEvent(string eventName, int duration) {
            if (duration <= 0) {
                duration = 1;
            }
            eventList.Add(eventName);
            int i = eventList.IndexOf(eventName);
            eventListDuration.Insert(i, duration);
        }

        static void ReduceEvent(string eventName) {
            int i = eventList.IndexOf(eventName);
            eventListDuration[i] = eventListDuration[i] - 1;
            if (eventListDuration[i] <= 0) {
                eventListDuration.RemoveAt(i);
                eventList.RemoveAt(i);
            }
        }
        public static void EventParse() {
            if (!myDDOL)
            myDDOL = GameObject.FindGameObjectWithTag("Consistent").GetComponent<ChangeNameOnStart>();
            for (int i = 0; i < eventList.Count; i++) {
                myDDOL.EventParser(eventList[i]);
                ReduceEvent(eventList[i]);
            }
        }
    }
    public static class AllEventNames {
        //add strings to this manually to add events. Ask Matt for help with this, maybe others too.
        public static string TestEvent = "TestEvent";
    }

    public static class PlayerStats {
        public static bool keepLighter = false;
    }
    public static class Scenes {
        static int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        public static string[] scenes { get; private set; }

        public static void CalcScenes() {
            scenes = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++) {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            }
        }
    }
    public static class Doors {
        public static float DoorsOpened { get; private set; }
        public static string DoorsOpenedString { get; private set; }
        static bool useCustomString = false;

        public static void _AddDoor() {
            DoorsOpened++;
            if (!useCustomString) {
                DoorsOpenedString = DoorsOpened.ToString("F0");
            }
        }

        public static void _SetDoorString(string myString) {
            useCustomString = true;
            DoorsOpenedString = myString;
        }

        public static void _UnsetDoorString() {
            useCustomString = false;
            DoorsOpenedString = DoorsOpened.ToString("F0");
        }
    }
}

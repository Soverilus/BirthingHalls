using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class _StaticGameManager {
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

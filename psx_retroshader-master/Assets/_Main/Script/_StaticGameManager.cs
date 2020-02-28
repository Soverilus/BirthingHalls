using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class _StaticGameManager {

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

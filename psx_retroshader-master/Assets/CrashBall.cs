using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEditor;

public class CrashBall : MonoBehaviour
{
    public bool doPassiveCrash = true;
    bool haveRanErrors = false;
    bool eyeSmiling = false;
    public Camera myCam;
    public GameObject smile;
    public Vector3 smileScale;
    public IEnumerator CrashToDesktop(string myCrashText, string myCapText, float time, WindowType type) {
        yield return new WaitForSecondsRealtime(time);
        NativeWinAlert.PopUp(myCrashText, myCapText, type);
        Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Debug.LogError("Application tried to Quit, run this in a full build!");
        Application.Quit();
        
    }
    private void Update() {
        if (!doPassiveCrash) {
            if (!haveRanErrors) {
                Time.timeScale = Mathf.Clamp(Time.timeScale - Time.deltaTime / 2f, 0.000001f, 10f);
                if (Time.timeScale <= 0.01f) {
                    GameObject.FindGameObjectWithTag("Player").SetActive(false);
                    StartCoroutine(DoTheThing0());
                    haveRanErrors = true;
                }
            }
        }
        else if (Time.timeScale != 1f){
            Time.timeScale = 1f;
        }

    }
    IEnumerator DoTheThing0() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        NativeWinAlert.PopUp("You Lose", "No Cake For You", WindowType.Error);
        myCam.farClipPlane = 20f;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DoTheThing1());
    }

    IEnumerator DoTheThing1() {
        Cursor.lockState = CursorLockMode.Confined;
        NativeWinAlert.PopUp("We ate it for you, don't worry!", "HahaHaHahAhAha", WindowType.Error);
        myCam.farClipPlane = 35f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DoTheThing2());
    }
    IEnumerator DoTheThing2() {
        Cursor.lockState = CursorLockMode.Confined;
        NativeWinAlert.PopUp("You've only opened " + _StaticGameManager.Doors.DoorsOpened + " doors?", "We caught you", WindowType.Info);
        myCam.farClipPlane = 65f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DoTheThing3());
    }
    IEnumerator DoTheThing3() {
        Cursor.lockState = CursorLockMode.Confined;
        NativeWinAlert.PopUp("There's nowhere else to run.", "Can't Hide from Us", WindowType.Info);
        myCam.farClipPlane = 105f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DoTheThing4());
    }
    IEnumerator DoTheThing4() {
        Cursor.lockState = CursorLockMode.Confined;
        NativeWinAlert.PopUp("Try again! After all, we want more cake.", "We'll eat it for you again", WindowType.Warning);
        haveRanErrors = true;
        StartCoroutine(EyeSmile());
        yield return new WaitForEndOfFrame();
    }

    IEnumerator EyeSmile() {
        smile.transform.localScale = Vector3.Lerp(smile.transform.localScale, smileScale, 0.01f);
        if (Vector3.Magnitude(smileScale - smile.transform.localScale) < 0.1f) {
            eyeSmiling = true;
        }
        yield return new WaitForEndOfFrame();
        if (!eyeSmiling) {
            StartCoroutine(EyeSmile());
        }
        else {
            SearchWindows();
            UnityEngine.Debug.LogError("Application tried to Quit, run this in a full build!");
            Application.Quit();
        }
    }

    /*void SearchWindows() {
        string connectionString = "Provider=Search.CollatorDSO;Extended Properties=\"Application=Windows\"";
        OleDbConnection connection = new OleDbConnection(connectionString);

        string query = @"SELECT System.ItemName FROM SystemIndex " +
           @"WHERE scope ='file:" + System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "' and FREETEXT('cake')";
        OleDbCommand command = new OleDbCommand(query, connection);
        connection.Open();

        List<string> result = new List<string>();

        OleDbDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            result.Add(reader.GetString(0));
        }

        connection.Close();
    }*/

    void SearchWindows() {
        System.Diagnostics.Process a = new System.Diagnostics.Process();
        a.StartInfo = new System.Diagnostics.ProcessStartInfo("notepad.exe");
        a.Start();

        System.Diagnostics.Process b = new System.Diagnostics.Process();
        b.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
        b.Start();

        Application.OpenURL("https://www.google.com/search?q=cake&sxsrf=ALeKk03a_AqTRlMB9ZS60AVU59Bdto8weA:1586584289211&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjFu9uH19_oAhX7yDgGHVvKD7wQ_AUoAXoECC4QAw&biw=1920&bih=1089");
        Application.OpenURL("https://www.merriam-webster.com/dictionary/error");
        Application.OpenURL("https://www.google.com/maps/search/WHERE+IS+OUR+CAKE/@-26.4667819,98.8344648,3z/data=!3m1!4b1");
        Application.OpenURL("https://www.thesaurus.com/browse/hungry");

        System.Diagnostics.Process e = new System.Diagnostics.Process();
        e.StartInfo = new System.Diagnostics.ProcessStartInfo("discord.exe");
        e.Start();

        System.Diagnostics.Process f = new System.Diagnostics.Process();
        f.StartInfo = new System.Diagnostics.ProcessStartInfo("riot.exe");
        f.Start();

        System.Diagnostics.Process g = new System.Diagnostics.Process();
        g.StartInfo = new System.Diagnostics.ProcessStartInfo("steam.exe");
        g.Start();
    }
}

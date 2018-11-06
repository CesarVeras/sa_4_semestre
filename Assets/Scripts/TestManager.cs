using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static int numberTest = 1;

    public static void AssertEquals(int received, int expected)
    {
        if (received != expected)
        {
            print("Fail test #" + numberTest + " AssertEquals: " + received + " is diferent from " + expected);
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertEquals(float received, float expected)
    {
        if (received != expected)
        {
            print("Fail test #" + numberTest + " AssertEquals: " + received + " is diferent from " + expected);
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertEquals(string received, string expected)
    {
        if (received != expected)
        {
            print("Fail test #" + numberTest + " AssertEquals: " + received + " is diferent from " + expected);
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertNotEquals(int received, int expected)
    {
        if (received == expected)
        {
            print("Fail test #" + numberTest + " AssertNotEquals: " + received + " is equals to " + expected);
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertNotEquals(float received, float expected)
    {
        if (received == expected)
        {
            print("Fail test #" + numberTest + " AssertNotEquals: " + received + " is equals to " + expected);
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertNotEquals(string received, string expected)
    {
        if (received == expected)
        {
            print("Fail test #" + numberTest + " AssertNotEquals: " + received + " is equals to " + expected);
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertNull(Object received)
    {
        if (received != null)
        {
            print("Fail test #" + numberTest + " AssertNull: " + received + " is not null");
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertNotNull(Object received)
    {
        if (received == null)
        {
            print("Fail test #" + numberTest + " AssertNotNull: " + received + " is null");
            QuitCode();
        }
        numberTest++;
    }

    public static void AssertTrue(bool received)
    {
        if (!received)
        {
            print("Fail test #" + numberTest + " AssertTrue: received false");
        }
        numberTest++;
    }

    public static void AssertNotTrue(bool received)
    {
        if (received)
        {
            print("Fail test #" + numberTest + " AssertTrue: received true");
        }
        numberTest++;
    }

    public static void QuitCode()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

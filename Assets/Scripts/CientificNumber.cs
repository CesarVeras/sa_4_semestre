using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CientificNumber
{

    public static string[] symbols = { "", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "D", "dD", "tD" };

    protected float signifficand;
    protected int coefficient;

    public CientificNumber(int num) : this((float)num) { }

    public CientificNumber(float num)
    {
        int digs = Mathf.FloorToInt(Mathf.Log10(num));
        signifficand = num / Mathf.Pow(10, digs);
        coefficient = digs;
    }

    public CientificNumber(float signifficand, int coefficient)
    {
        int digs = Mathf.FloorToInt(Mathf.Log10(signifficand));
        this.signifficand = signifficand / Mathf.Pow(10, digs);
        this.coefficient = coefficient + digs;
    }

    public static CientificNumber operator +(CientificNumber a, CientificNumber b)
    {
        if (b > a)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
        int diff = a.coefficient - b.coefficient;
        a.signifficand *= Mathf.Pow(10, diff);
        return new CientificNumber(a.signifficand + b.signifficand, b.coefficient);
    }

    public static CientificNumber operator -(CientificNumber a, CientificNumber b)
    {
        if (b > a)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
        int diff = a.coefficient - b.coefficient;
        a.signifficand *= Mathf.Pow(10, diff);
        return new CientificNumber(a.signifficand - b.signifficand, b.coefficient);
    }

    public static CientificNumber operator *(CientificNumber a, CientificNumber b)
    {
        return new CientificNumber(a.signifficand * b.signifficand, a.coefficient + b.coefficient);
    }

    public static CientificNumber operator ^(CientificNumber a, CientificNumber b)
    {
        float s = Mathf.Pow(a.signifficand, b);
        float c = a.coefficient * b;
        int cf = Mathf.FloorToInt(c);
        float diff = c - cf;
        s *= Mathf.Pow(10, diff);
        return new CientificNumber(s, cf);
    }

    public static bool operator >(CientificNumber a, CientificNumber b)
    {
        return b < a;
    }

    public static bool operator <(CientificNumber a, CientificNumber b)
    {
        if (a.coefficient != b.coefficient)
        {
            return a.coefficient < b.coefficient;
        }
        else
        {
            return a.signifficand < b.signifficand;
        }
    }

    public static implicit operator CientificNumber(int a)
    {
        return new CientificNumber(a);
    }

    public static implicit operator CientificNumber(float a)
    {
        return new CientificNumber(a);
    }

    public static implicit operator float(CientificNumber a)
    {
        return a.signifficand * Mathf.Pow(10, a.coefficient);
    }

    public string ToFormattedString()
    {
        int d = Mathf.FloorToInt(coefficient / 3);
        string s = symbols[d];
        int n = coefficient % 3;
        return signifficand * Mathf.Pow(10, n) + s;
    }

    public override string ToString()
    {
        return "CientificNumber {" + signifficand + " E " + coefficient + "}";
    }

}
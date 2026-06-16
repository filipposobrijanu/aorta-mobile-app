using System;

[Serializable]
public class UserData
{
    public int systolic;
    public int diastolic;
    public int age;
    public string gender;
    public string category;
    public string timestamp;

    public UserData(int sys, int dia, int a, string g, string cat)
    {
        systolic = sys;
        diastolic = dia;
        age = a;
        gender = g;
        category = cat;
        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
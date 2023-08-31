using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;  // For the InputField UI component

public class RotationLogger : MonoBehaviour
{
    public static RotationLogger Instance; // Static instance of RotationLogger across different scenes

    private string filePath;
    private string trialNumber;  // Trial Number will store the value of PatientIDInput

    private void Awake()
    {
        // If there's already an instance and it's not this, destroy this instance
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Otherwise, set this as the instance and make sure it doesn't get destroyed on scene load
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CreateCSVFile()
    {
        string directory = Application.persistentDataPath;

        // Get the current date and time and format it as a string
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");

        // Find the PatientIDInput InputField in the scene and retrieve its value
        InputField patientIDInputField = GameObject.Find("PatientIDInput").GetComponent<InputField>();
        trialNumber = patientIDInputField.text;

        string fileName = trialNumber + "Data" + timestamp + ".csv";
        filePath = Path.Combine(directory, fileName);

        string header = "Event,Trial Number,unix time,x,y,z\n";
        File.WriteAllText(filePath, header);

        // Log the file path to the console
        Debug.Log("File saved at: " + filePath);
    }

    public void LogRotation(string eventDescription, Vector3 rotation)
    {
        double unixTime = (System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        string data = string.Format("{0},{1},{2:F0},{3},{4},{5}\n", eventDescription, trialNumber, unixTime, rotation.x, rotation.y, rotation.z);

        File.AppendAllText(filePath, data);

        Debug.Log(string.Format("Event: {0}, Trial Number: {1}, Rotation Logged: X={2}, Y={3}, Z={4}", eventDescription, trialNumber, rotation.x, rotation.y, rotation.z));
    }

    public bool DoesCsvExist()
    {
        return File.Exists(filePath);
    }
}
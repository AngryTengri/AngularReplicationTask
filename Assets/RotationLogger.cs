using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class RotationLogger : MonoBehaviour
{
    public static RotationLogger Instance; // Static instance of RotationLogger across different scenes

    private string filePath;
    private string trialNumber;

    private void Start()
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
        trialNumber = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        CreateCSVFile();
    }

    public void CreateCSVFile()
    {
        string directory = Application.persistentDataPath;
        string fileName = trialNumber + "Data.csv";
        filePath = Path.Combine(directory, fileName);

        string header = "Event,Trial Number,unix time,x_rotation,y_rotation,z_rotation,x_position,y_position,z_position,Angle,Anticlockwise,Normalized Angle,Delta Error\n";
        File.WriteAllText(filePath, header);
    }

    public void LogRotation(string eventDescription, Vector3 rotation, Vector3 position, float yRotationValue, bool anticlockwise)
    {
        double unixTime = (System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;

        // Calculate the intended angle and then normalize it
        float angle = yRotationValue * 3;
        float normalizedAngle = Mathf.Repeat(angle, 360f);

        // Adjust for anticlockwise rotations
        if (anticlockwise)
        {
            normalizedAngle = 360f - normalizedAngle;
        }

        float deltaError = Mathf.Abs(normalizedAngle - rotation.y);

        string data = string.Format("{0},{1},{2:F0},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}\n",
                                    eventDescription, trialNumber, unixTime,
                                    rotation.x, rotation.y, rotation.z,
                                    position.x, position.y, position.z,
                                    yRotationValue, anticlockwise.ToString(), normalizedAngle,
                                    deltaError);

        File.AppendAllText(filePath, data);
    }

    public bool DoesCsvExist()
    {
        return File.Exists(filePath);
    }
}

using UnityEngine;
using System.Collections;
using System.IO;

// quick and dirty parser to use test CSV files line by line
public class TestCSVParser
{
    private StreamReader sr;
    private string fileName;

    public TestCSVParser(string filename)
    {
        sr = new StreamReader(filename);
        fileName = filename;

        // read off header line
        string firstLine = sr.ReadLine();
    }

    private void Restart()
    {
        sr = new StreamReader(fileName);
        string firstLine = sr.ReadLine();
    }

    // Reads a line of csv file and returns first numberOfValues values from the line as a float array
    public float[] ReadLine(int numberOfValues)
    {
        string line;
        float[] res = new float[numberOfValues];
        if ((line = sr.ReadLine()) == null)
        {
            Restart();
            line = sr.ReadLine();
        }
        string[] strings = line.Split(',');
        for (int i = 0; i < numberOfValues; i++)
        {
            res[i] = float.Parse(strings[i]);
        }
        return res;

    }
}

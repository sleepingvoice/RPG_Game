using BaseClass;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ModelFileListEditor : EditorWindow
{
    private const string folderPath = "Assets/_Resource/Monster/Model"; // ������ �˻��� ���� ���
    private const string saveFilePath = "/JsonData/ModelPath.json";
    private static readonly Regex fileNamePattern = new Regex(@"^\d+[A-Za-z]*_[A-Za-z]+$");

    // ������ �̸��� ���ϴ� ���İ� �´��� ��
    // ������ ������ ����+���ĺ�_���ĺ� �̳� ����_���ĺ� �� ����

    [MenuItem("Window/Custom/Set ModelList")]
    public static void ShowWindow()
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            List<string> validFileNames = new List<string>(); // ������ �´� �ּ�
            List<string> invalidFileNames = new List<string>(); // ������ �ٸ� �ּ�
            PathListClass pathList = new PathListClass();

            // .meta ���� ���� �� ���� �̸� ���� ����
            foreach (var file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (fileNamePattern.IsMatch(fileName))
                    {
                        validFileNames.Add(fileName);
                    }
                    else
                    {
                        invalidFileNames.Add(fileName);
                    }
                }
            }


            // Ʋ�� ����� ���
            string result = "������ ���� :\n" + (invalidFileNames.Count > 0 ? string.Join("\n", invalidFileNames) : "������ ������ �����ϴ�.");



            if (invalidFileNames.Count > 0) // �߸��� ��ΰ� ������� ����
            {
                Debug.Log(invalidFileNames.Count);
                EditorUtility.DisplayDialog("Failure File Path", result, "OK");
                return;
            }

            pathList.PathList = new List<PathInfo>();
            string modelPattern = @"^(.+?)_";
            foreach (string Name in validFileNames)
            {
                Match match = Regex.Match(Name, modelPattern);
                if (match.Success)
                {
                    PathInfo TempInfo = new PathInfo();
                    TempInfo.ModelName = match.Groups[1].Value;
                    TempInfo.FileNames = Name;
                    pathList.PathList.Add(TempInfo);
                }
            }

            if (pathList.PathList.Count != 0)
            {
                File.WriteAllText(Application.dataPath + saveFilePath, JsonUtility.ToJson(pathList));
                result = "���忡 �����Ͽ����ϴ�.";
            }
            else
                result = "���忡 �����߽��ϴ�.";

            EditorUtility.DisplayDialog("Failure File Path", result, "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Directory does not exist.", "OK");
        }
    }
}
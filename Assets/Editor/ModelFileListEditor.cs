using BaseClass;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ModelFileListEditor : EditorWindow
{
    private const string folderPath = "Assets/_Resource/Monster/Model"; // 파일을 검색할 폴더 경로
    private const string saveFilePath = "/JsonData/ModelPath.json";
    private static readonly Regex fileNamePattern = new Regex(@"^\d+[A-Za-z]*_[A-Za-z]+$");

    // 파일의 이름이 원하는 형식과 맞는지 비교
    // 파일의 형식은 숫자+알파벳_알파벳 이나 숫자_알파벳 만 가능

    [MenuItem("Window/Custom/Set ModelList")]
    public static void ShowWindow()
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            List<string> validFileNames = new List<string>(); // 형식이 맞는 주소
            List<string> invalidFileNames = new List<string>(); // 형식이 다른 주소
            PathListClass pathList = new PathListClass();

            // .meta 파일 제외 및 파일 이름 형식 검증
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


            // 틀린 결과물 출력
            string result = "실패한 파일 :\n" + (invalidFileNames.Count > 0 ? string.Join("\n", invalidFileNames) : "실패한 파일이 없습니다.");



            if (invalidFileNames.Count > 0) // 잘못된 경로가 있을경우 실패
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
                result = "저장에 성공하였습니다.";
            }
            else
                result = "저장에 실패했습니다.";

            EditorUtility.DisplayDialog("Failure File Path", result, "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Directory does not exist.", "OK");
        }
    }
}
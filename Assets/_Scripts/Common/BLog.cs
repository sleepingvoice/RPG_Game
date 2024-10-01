using UnityEngine;

public class Blog
{
    public static void ProgressLog(string log, string color = "yellow")
    {
        // 문자열 보간을 사용하여 메시지를 색상 태그와 함께 출력
        Debug.Log($"<color={color}>{log}</color>");
    }
}

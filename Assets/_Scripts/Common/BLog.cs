using UnityEngine;

public class Blog
{
    public static void ProgressLog(string log, string color = "yellow")
    {
        // ���ڿ� ������ ����Ͽ� �޽����� ���� �±׿� �Բ� ���
        Debug.Log($"<color={color}>{log}</color>");
    }
}

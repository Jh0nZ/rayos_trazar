using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using SFB;
public class ScreenCaptureManager : MonoBehaviour
{
    public void CaptureScreen()
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png")
        };

        // Open file save dialog
        StandaloneFileBrowser.SaveFilePanelAsync("Save Screenshot", "", "Captura_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss"), extensions, (path) =>
        {
            if (!string.IsNullOrEmpty(path))
            {
                StartCoroutine(CaptureScreenshotCoroutine(path));
            }
        });
    }

    private IEnumerator CaptureScreenshotCoroutine(string filePath)
    {
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(filePath);
        yield return new WaitForSeconds(0.5f);

        // Open the folder containing the saved screenshot
        OpenFolder(filePath);
    }
     private void OpenFolder(string filePath)
    {
        string folderPath = Path.GetDirectoryName(filePath);

        #if UNITY_EDITOR
        // Use the Unity Editor API to open the folder
        UnityEditor.EditorUtility.RevealInFinder(filePath);
        #elif UNITY_STANDALONE_WIN
        // For Windows
        System.Diagnostics.Process.Start("explorer.exe", folderPath);
        #elif UNITY_STANDALONE_OSX
        // For macOS
        System.Diagnostics.Process.Start("open", folderPath);
        #elif UNITY_STANDALONE_LINUX
        // For Linux
        System.Diagnostics.Process.Start("xdg-open", folderPath);
        #endif
    }
}
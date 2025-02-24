using UnityEngine;

public class ScreenShotMaker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScreenCapture.CaptureScreenshot("screenshotKK " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
        }
    }
}
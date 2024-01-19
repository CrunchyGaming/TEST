using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IconGenerator : MonoBehaviour
{

    [SerializeField]
    private string screenshotFolderPath = "Assets/Screenshots";



    void Update() {
        // Example: Press the "P" key to take a cropped screenshot
        if (Input.GetKeyDown(KeyCode.P)) {
            TakeCroppedScreenshot();
        }
    }

    void TakeCroppedScreenshot() {
        // Ensure the folder exists
        if (!System.IO.Directory.Exists(screenshotFolderPath)) {
            System.IO.Directory.CreateDirectory(screenshotFolderPath);
        }

        // Set the desired cropped size
        int croppedWidth = 256;
        int croppedHeight = 256;

        // Create a temporary camera
        Camera tempCamera = new GameObject("TempCamera").AddComponent<Camera>();
        tempCamera.CopyFrom(Camera.main);

        // Set the target texture for the temporary camera
        RenderTexture rt = new RenderTexture(croppedWidth, croppedHeight, 24);
        tempCamera.targetTexture = rt;

        // Render the scene to the RenderTexture
        tempCamera.Render();

        // Activate the RenderTexture
        RenderTexture.active = rt;

        // Create a new texture to store the cropped region
        Texture2D croppedTexture = new Texture2D(croppedWidth, croppedHeight);
        croppedTexture.ReadPixels(new Rect(0, 0, croppedWidth, croppedHeight), 0, 0);
        croppedTexture.Apply();

        // Reset the temporary camera
        tempCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Destroy(tempCamera.gameObject);

        // Save the cropped screenshot
        string screenshotName = "CroppedScreenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string fullPath = System.IO.Path.Combine(screenshotFolderPath, screenshotName);

        byte[] bytes = croppedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);

        Debug.Log("Cropped screenshot saved to: " + fullPath);
    }

}

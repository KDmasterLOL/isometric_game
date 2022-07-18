using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CreateTextureManager : MonoBehaviour
{
    private Camera _camera;
    private Transform _cameraRotate;
    [SerializeField]
    private GameObject[] _models;
    private Vector2Int _sizeTexture = new(32, 64);
    private string fileName = "";
    private int currentAngle = 0, currentIndex = 0;
    private bool _takeScreenshot = false;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraRotate = _camera.transform.parent;
        RenderPipelineManager.endCameraRendering += (a, c) => OnPostRender();
        StartCoroutine(RotateAndTakeScreen());

    }
    private IEnumerator RotateAndTakeScreen()
    {
        yield return new WaitForEndOfFrame();
        foreach (var model in _models)
        {
            var obj = Instantiate(model, Vector3.zero, new(), _cameraRotate.parent);
            obj.transform.Translate(Vector3.zero);
            for (; obj.transform.eulerAngles.y <= 183;)
            {
                fileName = ((int)obj.transform.eulerAngles.y).ToString();
                TakeScreenshot();
                // yield return new WaitUntil(() => _takeScreenshot == false);
                yield return null;
                obj.transform.Rotate(new(0, 45, 0));
            }
            Destroy(obj);
        }
    }
    private void OnPostRender()
    {
        if (_takeScreenshot)
        {
            var renderTexture = _camera.targetTexture;
            Texture2D texture = new(_sizeTexture.x, _sizeTexture.y, TextureFormat.ARGB32, false);
            var rect = new Rect(0, 0, _sizeTexture.x, _sizeTexture.y);
            texture.ReadPixels(rect, 0, 0);
            byte[] png = texture.EncodeToPNG();
            var destPath = Application.dataPath + $"/Textures/Player/Walk/{fileName}.png";
            System.IO.File.WriteAllBytes(destPath, png);
            // print($"Screen taken in {destPath}");
            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;
            _takeScreenshot = false;
        }

    }
    private void TakeScreenshot()
    {
        _camera.targetTexture = RenderTexture.GetTemporary(_sizeTexture.x, _sizeTexture.y, 16);
        _takeScreenshot = true;
        // print("OnTakeScreen");
    }

}

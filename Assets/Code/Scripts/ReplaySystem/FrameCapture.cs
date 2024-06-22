using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Rendering;

namespace FPVDrone
{
    public class FrameCapture : Singletone<FrameCapture>
    {
        public Camera captureCamera;
        public int width = 1280; // Lower resolution to improve performance
        public int height = 720; // Lower resolution to improve performance
        public int frameRate = 30;
        public float maxDuration = 5f;

        private RenderTexture renderTexture;
        private CircularBuffer<Texture2D> frameBuffer;
        private int bufferCapacity;
        private IDisposable recordingSubscription;
        private bool isRecording = false;
        private Queue<Texture2D> texturePool;

        void Start()
        {
            bufferCapacity = Mathf.CeilToInt(maxDuration * frameRate);
            frameBuffer = new CircularBuffer<Texture2D>(bufferCapacity);
            renderTexture = new RenderTexture(width, height, 24);
            captureCamera.targetTexture = renderTexture;
            texturePool = new Queue<Texture2D>(bufferCapacity);

            for (int i = 0; i < bufferCapacity; i++)
            {
                texturePool.Enqueue(new Texture2D(width, height, TextureFormat.RGB24, false));
            }
        }

        void CaptureFrame()
        {
            captureCamera.Render();
            AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGB24, OnCompleteReadback);
        }

        void OnCompleteReadback(AsyncGPUReadbackRequest request)
        {
            if (request.hasError)
            {
                Debug.LogWarning("GPU readback error detected.");
                return;
            }

            Texture2D texture = texturePool.Dequeue();
            texture.LoadRawTextureData(request.GetData<byte>());
            texture.Apply();
            frameBuffer.Add(texture);
            texturePool.Enqueue(texture);
        }

        public void StartRecording()
        {
            if (!isRecording)
            {
                isRecording = true;
                frameBuffer.Clear();

                recordingSubscription = Observable
                    .Interval(TimeSpan.FromSeconds(1f / frameRate))
                    .Subscribe(_ => CaptureFrame(), () => Debug.Log("Capture completed"));
            }
        }

        public void StopRecording()
        {
            if (isRecording)
            {
                isRecording = false;
                recordingSubscription?.Dispose();
            }
        }

        public List<Texture2D> GetFrames()
        {
            return frameBuffer.GetAll();
        }
    }
}

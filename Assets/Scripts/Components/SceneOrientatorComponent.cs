using System;
using UnityEngine;

namespace Components
{
    public class SceneOrientatorComponent : MonoBehaviour
    {
        [SerializeField] private SceneOrientation _sceneOrientation;

        private void Awake()
        {
            Screen.orientation = _sceneOrientation switch
            {
                SceneOrientation.LandScapeRight => ScreenOrientation.LandscapeRight,
                SceneOrientation.LandScapeLeft => ScreenOrientation.LandscapeLeft,
                SceneOrientation.Portrait => ScreenOrientation.Portrait,
                SceneOrientation.Auto => ScreenOrientation.AutoRotation,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum SceneOrientation
    {
        LandScapeRight,
        LandScapeLeft,
        Portrait,
        Auto
    }
}
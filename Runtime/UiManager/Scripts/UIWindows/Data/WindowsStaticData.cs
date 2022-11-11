using System.Collections.Generic;
using Redpenguin.UiManager.UIWindows.Windows;
using UIManagement.UIWindows.Windows;
using UnityEngine;

namespace UIManagement.UIWindows.Data
{
    [CreateAssetMenu(menuName = "Static Data/Windows static data", fileName = "WindowsStaticData")]
    public class WindowsStaticData : ScriptableObject
    {
        [SerializeField] private List<BaseWindow> _windows;
        public List<BaseWindow> Windows => new List<BaseWindow>(_windows);
    }
}
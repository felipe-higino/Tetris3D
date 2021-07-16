using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIComponents.Pagination
{

    public class UIPage : MonoBehaviour
    {
        [SerializeField]
        private UIPagination manager;

        [SerializeField]
        private UnityEvent ShowCallbacks;
        [SerializeField]
        private UnityEvent HideCallbacks;

        private bool isShown;

        [ContextMenu("Show Lonely")]
        public void ShowLonely()
        {
            if (null == manager)
                ShowPage();
            else
                manager.ShowOnly(this);
        }

        [ContextMenu("Show")]
        public void ShowPage()
        {
            isShown = true;
            ShowCallbacks?.Invoke();
        }

        [ContextMenu("Hide")]
        public void HidePage()
        {
            isShown = false;
            HideCallbacks?.Invoke();
        }

        [ContextMenu("Toggle activation")]
        public void ToggleActivation()
        {
            if (isShown)
                HidePage();
            else
                ShowLonely();
        }

        private void Awake()
        {
            if (manager == null)
                return;
            manager.AssignPage(this);

            var root = manager.SpawnRoot;
            if (null == root)
                return;

            transform.SetPositionAndRotation(root.position, root.rotation);
        }
    }
}

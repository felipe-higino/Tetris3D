using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UIComponents.Pagination
{

    public class UIPage : MonoBehaviour
    {
        private enum Snap
        {
            MAINTAIN_POSITION,
            PARENT_POSITION,
            GROUP_POSITION
        }

        [SerializeField]
        private UIPagination manager;
        [SerializeField]
        private Snap SnapTo = Snap.MAINTAIN_POSITION;

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


            switch (SnapTo)
            {
                case Snap.GROUP_POSITION:
                    var root = manager.SpawnRoot;
                    if (null == root)
                        break;
                    transform.SetPositionAndRotation(root.position, root.rotation);
                    break;

                case Snap.PARENT_POSITION:
                    var parent = transform.parent.transform;
                    transform.SetPositionAndRotation(parent.position, parent.rotation);
                    break;
                case Snap.MAINTAIN_POSITION:
                default:
                    break;
            }
        }
    }
}

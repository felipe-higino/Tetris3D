using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIComponents.Pagination
{
    public class UIPagination : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnRoot;
        private List<UIPage> pages = new List<UIPage>();

        public Transform SpawnRoot => spawnRoot;
        private UIPage lastShown;
        private UIPage currentlyShowing;

        public void HideGroup()
        {
            foreach (var page in pages)
            {
                page.HidePage();
            }
        }

        public void ShowLast()
        {
            ShowOnly(lastShown);
        }

        public void AssignPage(UIPage page)
        {
            pages.Add(page);
        }

        public void ShowOnly(UIPage page)
        {
            foreach (var p in pages)
            {
                if (p == page)
                {
                    lastShown = currentlyShowing;
                    currentlyShowing = page;
                    page?.ShowPage();
                    continue;
                }
                p.HidePage();
            }
        }
    }
}

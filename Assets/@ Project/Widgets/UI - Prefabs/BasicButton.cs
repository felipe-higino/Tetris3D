using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Widgets
{
    public class BasicButton : MonoBehaviour
    {
        public void SFX_PlayHover()
        {
            UISoundEffects.Instance.PlayHover();
        }

        public void SFX_PlayPress()
        {
            UISoundEffects.Instance.PlayPress();
        }
    }

}

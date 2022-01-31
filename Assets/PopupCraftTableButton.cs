using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace LittleFroggyHat
{
    public class InGameButton : MonoBehaviour
    {
        public virtual void ClickOnPopUp()
        {
            
        }
    }
    public class PopupCraftTableButton : InGameButton
    {
        public override void ClickOnPopUp()
        {
            base.ClickOnPopUp();

            var entity = Service<EcsWorld>.Get().NewEntity();
            entity.Get<ShowPopUp>().type = PopupType.CraftTable;
        }
    }
    
    public struct ShowPopUp
    {
        public PopupType type;
    }

    public enum PopupType
    {
        CraftTable
    }
}

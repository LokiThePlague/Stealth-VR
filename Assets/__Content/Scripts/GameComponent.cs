using UnityEngine;

namespace __Content.Scripts
{
    public class GameComponent : MonoBehaviour
    {
        protected bool IsOn;
        
        public virtual void On()
        {
            IsOn = true;
        }
        
        public virtual void Off()
        {
            IsOn = false;
        }
    }
}

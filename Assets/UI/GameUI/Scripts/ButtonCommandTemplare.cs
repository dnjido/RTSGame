using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public abstract class ButtonCommandTemplare : MonoBehaviour
    {
        [SerializeField]protected Image Image;

        protected BuildUnit builder;
        protected ICommandQueue queue;
        protected int ID;
        protected GameObject unit;

        protected ButtonProgressBar progressBar => GetComponent<ButtonProgressBar>();
        protected ButtonCounter counter => GetComponent<ButtonCounter>();

        protected bool isCurrentButton(GameObject u) => u == unit;

        public virtual void Init(int id, BuildUnit b, GetStats stats)
        {
            SetBuilder(id, b);

            OnEnable();
            Recovery();

            Image.sprite = stats.Stats(unit).generalStats.icon;
        }

        protected abstract void SetBuilder(int id, BuildUnit b);

        public abstract void RightClick();

        protected abstract void Recovery();
        public abstract void Command();

        protected abstract void StartBuild(Timer t, GameObject u);
        protected abstract void Clear(GameObject u);
        protected abstract void ChangeCount(GameObject u);

        public void OnEnable() => Enable();
        public void OnDisable() => Disable();

        protected abstract void Enable();
        protected abstract void Disable();
    }
}

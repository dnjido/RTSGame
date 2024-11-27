using RTS;
using UnityEngine;


namespace RTS
{
    public class ButtonSpawner
    {
        private bool created;
        private readonly BuildUnit builder;
        private readonly ButtonFacade.Factory buttonFactory;
        private readonly int count;


        public ButtonSpawner(ButtonFacade.Factory f, BuildUnit b)
        {
            buttonFactory = f;
            builder = b;
            count = builder.countList;
        }


        public void Buttons(bool s)
        {
            if (s) AddButtons();
            else ClearButton();
        }

        private void AddButtons()
        {
            if (created) return;

            buttonFactory.Create(
                builder.ButtonPrefab,
                MakeUnitButtonStruct.Make(count, builder, builder.Parent));

            created = true;
        }

        private void ClearButton()
        {
            if (!created) return;

            foreach (Transform child in builder.Parent.transform)
                child.gameObject.GetComponent<ButtonFacade>().Dispose();
            created = false;
        }
    }
}


using UnityEngine;

namespace RTS
{
    public class ButtonSpawner
    {
        private bool created;
        private readonly BuildUnit builder;
        private readonly ButtonFacade.Factory buttonFactory;


        public ButtonSpawner(ButtonFacade.Factory f, BuildUnit b)
        {
            buttonFactory = f;
            builder = b;
        }


        public void Buttons(bool s)
        {
            if (s) AddButtons();
            else ClearButton();
        }

        private void AddButtons()
        {
            if (created) return;

            for (int i = 0; i < builder.countList; i++)
                buttonFactory.Create(
                    builder.ButtonPrefab,
                    MakeUnitButtonStruct.Make(i, builder, builder.panel));

            created = true;
        }

        private void ClearButton()
        {
            if (!created) return;

            foreach (Transform child in builder.panel)
                child.gameObject.GetComponent<ButtonFacade>().Dispose();
            created = false;
        }
    }
}


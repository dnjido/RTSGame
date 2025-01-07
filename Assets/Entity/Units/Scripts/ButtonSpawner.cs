using System;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace RTS
{
    [Serializable]
    public class ButtonSpawner //  Creating production buttons on the user interface
    {
        private bool created;

        private readonly BuildUnit builder;
        private readonly ButtonFacade.Factory buttonFactory;

        private readonly GameObject buttonPrefab;
        private readonly Transform panel;
        private readonly GetStats stats;

        private int count => builder.countList;

        public ButtonSpawner(ButtonFacade.Factory f, BuildUnit bu, Transform p, GetStats b )
        {
            buttonFactory = f;
            builder = bu;
            panel = p;
            stats = b;
            buttonPrefab = b.Stats(bu.gameObject).buildStats.buttons;
        }

        public void ToggleButtons(bool active)
        {
            if (active) AddButtons();
            else ClearButton();
        }

        private void AddButtons()
        {
            if (created) return;

            for (int i = 0; i < count; i++)
                buttonFactory.Create(
                    buttonPrefab,
                    MakeUnitButtonStruct.Make(i, builder, panel, stats));
            created = true;
        }

        private void ClearButton()
        {
            if (!created) return;

            foreach (Transform child in panel)
                child.gameObject.GetComponent<ButtonFacade>().Dispose();

            created = false;
        }
    }
}


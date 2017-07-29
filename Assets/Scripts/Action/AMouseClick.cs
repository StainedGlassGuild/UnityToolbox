// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: UnityToolbox
// File: AMouseClick.cs
// Creation: 2017-07
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using SGG.UTB.UI.Input.Action.Internal;

using UnityEngine;

namespace SGG.UTB.UI.Input.Action
{
   public abstract class AMouseClick : AAction
   {
      #region Private fields

      [SerializeField, UsedImplicitly]
      private MouseButton m_MouseButton;

      [SerializeField, UsedImplicitly]
      private float m_MaxDistBetweenDownAndUpClick;

      private bool m_IsCancelled;

      #endregion

      #region Abstract methods

      protected abstract void DoClick(Vector2 a_ClickPxlPos);

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Start()
      {
         m_IsCancelled = false;
      }

      [UsedImplicitly]
      private void Update()
      {
         if (m_MouseButton.WasJustReleased)
         {
            if (m_IsCancelled)
            {
               m_IsCancelled = false;
               return;
            }

            if (m_MouseButton.DistanceSincePressedPxl < m_MaxDistBetweenDownAndUpClick)
            {
               var avgClickPos = (m_MouseButton.LastClickDownPxlPos +
                                  (Vector2) UnityEngine.Input.mousePosition) * 0.5f;
               DoClick(avgClickPos);
            }
         }
      }

      public override void Cancel()
      {
         m_IsCancelled = true;
      }

      #endregion
   }
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: UnityToolbox
// File: ABaseMouseSelection.cs
// Creation: 2017-07
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using UnityEngine;

namespace SGG.UTB.UI.Input.Action.Internal
{
   internal abstract class ABaseMouseSelection : AAction
   {
      #region Private fields

      [SerializeField, UsedImplicitly]
      private MouseButton m_MouseButton;

      [SerializeField, UsedImplicitly]
      private float m_MaxDistBetweenDownAndUpClick;

      #endregion

      #region Abstract methods

      protected abstract bool StartSelection(Bounds a_BoundsPxl2D, bool a_IsBelowDistLimit);

      protected abstract void CreateSelection(Bounds a_BoundsPxl2D);

      protected abstract void ClearSelection();

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Update()
      {
         if (!m_IsCreatingSelection && m_MouseButton.IsPressed)
         {
            var lastClickPos = m_MouseButton.LastClickDownPxlPos;
            var currPos = UnityEngine.Input.mousePosition;
            float dist = Vector2.Distance(lastClickPos, currPos);
            bool isBelowDistLimit = dist < m_MaxDistBetweenDownAndUpClick;
            m_IsCreatingSelection = StartSelection(ComputeBounds(), isBelowDistLimit);
         }

         if (m_MouseButton.IsPressed)
         {
            
         }

         if (m_MouseButton.WasJustReleased)
         {
            ClearSelection();
         }
         else
         {
            UpdateSelection(ComputeBounds());
         }


         if (m_IsCreatingSelection)
         {

         }
      }

      public override void Cancel()
      {
         if (m_IsCreatingSelection)
         {
            ClearSelection();
            m_IsCreatingSelection = false;
         }
      }

      private Bounds ComputeBounds()
      {
         var startPos = m_MouseButton.LastClickDownPxlPos;
         Vector2 currPos = UnityEngine.Input.mousePosition;

         var boxCenter = (startPos + currPos) * 0.5f;

         var boxMinCorner = new Vector2(
            Mathf.Min(currPos.x, startPos.x),
            Mathf.Min(currPos.y, startPos.y));

         var boxMaxCorner = new Vector2(
            Mathf.Max(currPos.x, startPos.x),
            Mathf.Max(currPos.y, startPos.y));

         var boxSize = boxMaxCorner - boxMinCorner;

         return new Bounds(boxCenter, boxSize);
      }

      #endregion
   }
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: UnityToolbox
// File: AMouseDragSelection.cs
// Creation: 2017-07
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using SGG.UTB.UI.Input.Action.Internal;

using UnityEngine;

namespace SGG.UTB.UI.Input.Action
{
   public abstract class AMouseDragSelection : AAction
   {
      #region Private fields

      [SerializeField, UsedImplicitly]
      private MouseButton m_MouseButton;

      [SerializeField, UsedImplicitly]
      private float m_MinSelectionDiameterSizePxl;

      private bool m_IsCreatingSelection;

      private bool m_ThereIsACreatedSelection;

      #endregion

      #region Abstract methods

      protected abstract void StartSelection(Bounds a_BoundsPxl2D);

      protected abstract void UpdateSelection(Bounds a_BoundsPxl2D);

      protected abstract void CreateSelection(Bounds a_BoundsPxl2D);

      protected abstract void ClearSelection();

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Start()
      {
         m_IsCreatingSelection = false;
         m_ThereIsACreatedSelection = false;
      }

      [UsedImplicitly]
      private void Update()
      {
         if (!m_IsCreatingSelection && m_MouseButton.IsPressed)
         {
            if (m_ThereIsACreatedSelection)
            {
               ClearSelection();
               m_ThereIsACreatedSelection = false;
            }

            var lastClickPos = m_MouseButton.LastClickDownPxlPos;
            var currPos = UnityEngine.Input.mousePosition;
            if (Vector2.Distance(lastClickPos, currPos) > m_MinSelectionDiameterSizePxl)
            {
               StartSelection(ComputeBounds());
               m_IsCreatingSelection = true;
            }
         }

         if (m_IsCreatingSelection)
         {
            if (m_MouseButton.WasJustReleased)
            {
               CreateSelection(ComputeBounds());
               m_IsCreatingSelection = false;
               m_ThereIsACreatedSelection = true;
            }
            else
            {
               UpdateSelection(ComputeBounds());
            }
         }
      }

      public override void Cancel()
      {
         ClearSelection();
         m_IsCreatingSelection = false;
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

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: UnityToolbox
// File: MouseButton.cs
// Creation: 2017-07
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using UnityEngine;

namespace SGG.UTB.UI.Input
{
   public sealed class MouseButton : Button
   {
      #region Properties

      public Vector2 LastClickDownPxlPos { get; private set; }
      public Vector3 LastClickDownWorldPos { get; private set; }

      public float DistanceSincePressedPxl
      {
         get
         {
            return IsPressed
               ? Vector2.Distance(LastClickDownPxlPos,
                  UnityEngine.Input.mousePosition)
               : 0;
         }
      }

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Start()
      {
         LastClickDownPxlPos = Vector2.zero;
         LastClickDownWorldPos = Vector3.zero;
      }

      [UsedImplicitly]
      private void Update()
      {
         if (WasJustPressed)
         {
            LastClickDownPxlPos = UnityEngine.Input.mousePosition;
            LastClickDownWorldPos = Camera.main.ScreenToWorldPoint(LastClickDownPxlPos);
         }
      }

      #endregion
   }
}

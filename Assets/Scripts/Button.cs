// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: UnityToolbox
// File: Button.cs
// Creation: 2017-07
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using UnityEngine;

namespace SGG.UTB.UI.Input
{
   /// <summary>
   /// Wrapper of UnityEngine.Input.GetButton that provides more info about a specific button.
   /// </summary>
   public class Button : MonoBehaviour
   {
      #region Private fields

      [SerializeField, UsedImplicitly]
      private string m_ButtonName;

      #endregion

      #region Properties

      public bool IsPressed
      {
         get { return UnityEngine.Input.GetButton(m_ButtonName); }
      }

      public bool WasJustPressed
      {
         get { return UnityEngine.Input.GetButtonDown(m_ButtonName); }
      }

      public bool WasJustReleased
      {
         get { return UnityEngine.Input.GetButtonUp(m_ButtonName); }
      }

      public bool HasEverBeenPressed { get; private set; }

      public bool HasEverBeenReleased { get; private set; }

      public float TimeWhenPressed { get; private set; }

      public float TimeWhenReleased { get; private set; }

      public float TimeSincePressed
      {
         get
         {
            if (!HasEverBeenPressed)
            {
               return 0;
            }

            return Time.time - TimeWhenPressed;
         }
      }

      public float TimeSinceReleased
      {
         get
         {
            if (!HasEverBeenReleased)
            {
               return 0;
            }

            return Time.time - TimeWhenReleased;
         }
      }

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Start()
      {
         HasEverBeenPressed = false;
         HasEverBeenReleased = false;
         TimeWhenPressed = 0;
         TimeWhenReleased = 0;
      }

      [UsedImplicitly]
      private void Update()
      {
         if (WasJustPressed)
         {
            HasEverBeenPressed = true;
            TimeWhenPressed = Time.time;
         }

         if (WasJustReleased)
         {
            HasEverBeenReleased = true;
            TimeWhenReleased = Time.time;
         }
      }

      #endregion
   }
}

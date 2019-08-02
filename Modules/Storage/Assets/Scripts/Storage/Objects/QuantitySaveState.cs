﻿using UnityEngine;

/*
 Create a new one of these for each entity you'd like to save
 the state for. This should in turn save this permanantely. If you
 want even more, just gather up all the states on save game
 and store them via a glbal state. You can always re-apply things later on
 upon load. */
namespace RedPanda.Storage
{
    [CreateAssetMenu(fileName = "New Quantity Save Object", menuName = "Quantity Save Object", order = 52)]
    [System.Serializable]
    public class QuantitySaveState : ScriptableObject
    {
        public int State;
    }
}
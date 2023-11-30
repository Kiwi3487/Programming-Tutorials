using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "value/FloatSO", fileName = "ValueSO", order = 1)]
    public class FloatSO : ScriptableObject
    {

        [SerializeField] private float _value;

        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}

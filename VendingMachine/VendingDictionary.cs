using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class VendingDictionary
    {
        //private int[] _quantityKeys;
        //private int[] _quantityValues;

        private IDictionary<int, int> _products;

        public VendingDictionary()
        {
            _products = new Dictionary<int, int>();
        }

        //public VendingDictionary(int[] quantityKeys, int[] quantityValues)
        //{
        //    _quantityKeys = quantityKeys;
        //    _quantityValues = quantityValues;
        //}

        public int GetValue(int productKey)
        {
            return _products[productKey];
            //return _quantityValues[GetIndex(productKey)];
        }

        //private int GetIndex(int productKey)
        //{
        //    return Array.IndexOf(_quantityKeys, productKey);
        //}

        public void Add(int product, int amount)
        {
            if (_products.ContainsKey(product))
            {
                return;
            }

            _products.Add(product, amount);

            //Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
            //Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
            //_quantityKeys[_quantityKeys.Length - 1] = product;
            //_quantityValues[_quantityValues.Length - 1] = amount;
        }

        //public bool ContainsKey(int productKey)
        //{
        //    return this.GetValue(productKey) < 1;
        //}

        public void SetValue(int productKey, int value)
        {
            _products[productKey] = value;
            //_quantityValues[productKey] = value;
        }
    }
}
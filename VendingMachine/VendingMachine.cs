using System;
using System.Collections.Generic;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private readonly List<int> _availableProducts = new List<int>();
        private int[] _quantityKeys = { };
        private int[] _quantityValues = { };
        private readonly Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _creditCard;
        private bool _valid;
        private int _selectedProduct;
        private readonly VendingDictionary _vendingDictionary;

        public double AvailableAmount { get; private set; }

        public VendingMachine()
        {
            _vendingDictionary = new VendingDictionary();
        }

        public Can Deliver(int productKey)
        {
            var price = _prices.ContainsKey(productKey) ? _prices[productKey] : 0;
            if (!_availableProducts.Contains(productKey) || _vendingDictionary.GetValue(productKey) < 1 || AvailableAmount < price)
            {
                return null;
            }

            _vendingDictionary.SetValue(productKey, _vendingDictionary.GetValue(productKey) - 1);
            AvailableAmount -= price;
            return new Can { Type = productKey };
        }

        public void AddChoice(int product, int amount = int.MaxValue)
        {
            _vendingDictionary.Add(product, amount);
            _availableProducts.Add(product);
        }

        public void AddMultipleChoices(int[] choices, int[] amounts)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                int product = choices[i];
                AddChoice(product, amounts[i]);
            }
        }

        public void AddCoin(int coinValue)
        {
            AvailableAmount += coinValue;
        }

        public double Change()
        {
            var amount = AvailableAmount;
            AvailableAmount = 0;
            return amount;
        }

        public void AddPrice(int i, double v)
        {
            _prices[i] = v;
        }

        public void Stock(int choice, int quantity, double price)
        {
            AddChoice(choice, quantity);
            _prices[choice] = price;
        }


        public double GetPrice(int choice)
        {
            return _prices[choice];
        }

        public void AcceptCard(CreditCard card)
        {
            _creditCard = card;
        }

        public void GetPinNumber(int pinNumber)
        {
            _valid = new CreditCardModule.CreditCardModule(_creditCard).HasValidPinNumber(pinNumber);
        }

        public void SelectChoiceForCard(int choice)
        {
            _selectedProduct = choice;
        }

        public Can DeliverChoiceForCard()
        {
            if (_valid && _availableProducts.IndexOf(_selectedProduct) > -1 && _vendingDictionary.GetValue(_selectedProduct) > 0)
            {
                _vendingDictionary.SetValue(_selectedProduct, _vendingDictionary.GetValue(_selectedProduct) - 1);
                return new Can { Type = _selectedProduct };
            }

            return null;
        }
    }

    public class Can
    {
        public int Type { get; set; }
    }
}
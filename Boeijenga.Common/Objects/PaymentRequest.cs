using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class PaymentRequest
    {
        private Merchant _merchant;
        private Customer _customer;
        private Transaction _transaction;
        public PaymentRequest()
        {
            _merchant = new Merchant();
            _customer = new Customer();
            _transaction = new Transaction();
        }
        public Merchant merchant
        {
            get
            {
                return _merchant;
            }
            set
            {
                _merchant = value;
            }
        }
        public Customer customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
            }
        }
        public Transaction transaction
        {
            get
            {
                return _transaction;
            }
            set
            {
                _transaction = value;
            }
        }
        public string signature { get; set; }

    }
}



using System;

namespace Boeijenga.Common.Objects
{
    public class Order
    {
        private string _articlecode;
        private string _productType; // added on 15/6/07
        private string _productdescription;	 // added on 15/6/07
        private string _publisherName;	 // added on 15/6/07
        private string _subtitle;
        private double _vatIncludedPrice = 0.00;	// added on 15/6/07
        private double _price = 0.00;	// added on 15/6/07
        private int _quantity = 1;	// added on 15/6/07
        private double _vatpc = 0.00;	// added on 10/12/07
        private double _discountpc = 0.00;	// added on 10/12/07
        private string _deliveryTime="";

        public Order()
        {
        }
        public Order(string articlecode)
        {
            this._articlecode = articlecode;
        }
        public Order(string articlecode, int quantity)
        {
            this._articlecode = articlecode;
            this._quantity = quantity;
        }
        public Order(string articlecode,string productType, string subtitle,string productdescription,double vatIncludedPrice,int quantity)
        {
            this._articlecode = articlecode;
            this._productType=productType;
            this._subtitle = subtitle;
            this._productdescription=productdescription;
            this._vatIncludedPrice =vatIncludedPrice;
            this._quantity = quantity;

        }
        public string articlecode
        {
            get
            {
                return _articlecode;
            }
            set
            {
                _articlecode = value;
            }
        }
        public string productType
        {
            get{ return _productType; }
            set {_productType=value ;}
        }
        public string productdescription
        {
            get{ return _productdescription;}
            set{_productdescription=value ;}
        }
        public string subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }
        public string publisherName
        {
            get { return _publisherName ;}
            set{_publisherName=value;}
        }
        public double vatIncludedPrice
        {
            get{ return _vatIncludedPrice; }
            set{ _vatIncludedPrice =value;}
        }
        public double price
        {
            get { return _price; }
            set { _price = value; }
        }
        public int quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public double vatpc
        {
            get
            {
                return _vatpc;
            }
            set
            {
                _vatpc = value;
            }
        }
        public double discountpc
        {
            get
            {
                return _discountpc;
            }
            set
            {
                _discountpc = value;
            }
        }

        public string deliveryTime
        {
            get { return _deliveryTime; }
            set { _deliveryTime = value; }
        }
    }
}
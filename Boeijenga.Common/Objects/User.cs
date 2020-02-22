using System;
using System.Web;
namespace Boeijenga.Common.Objects
{

    public class User
    {
        private int _id;
        private string _name;
        private string _email;
        private string _vatnr;
        private string _dInitialName;
        private string _dFirstname;
        private string _dMiddlename;
        private string _dLastname;
        private string _dHousenr;
        private string _dAddress;
        private string _dPostcode;
        private string _dResidence;
        private string _dcountry;
        private string _companyName;
        private string _firstName;

        public User()
        {
        }

      

        public User(int id, string email)
        {
            this._id = id;
            this._email = email;
        }
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        public string Vatnr
        {
            get
            {
                return _vatnr;
            }
            set
            {
                _vatnr = value;
            }
        }
        public string dInitialName
        {
            get
            {
                return _dInitialName;
            }
            set
            {
                _dInitialName = value;
            }
        }
        public string dFirstname
        {
            get
            {
                return _dFirstname;
            }
            set
            {
                _dFirstname = value;
            }
        }
        public string dMiddlename
        {
            get
            {
                return _dMiddlename;
            }
            set
            {
                _dMiddlename = value;
            }
        }
        public string dLastname
        {
            get
            {
                return _dLastname;
            }
            set
            {
                _dLastname = value;
            }
        }

        public string CompanyName
        {

            get { return _companyName; }
            set
            {
                _companyName = value;

            }

        }



        public string dHousenr
        {
            get
            {
                return _dHousenr;
            }
            set
            {
                _dHousenr = value;
            }
        }
        public string dAddress
        {
            get
            {
                return _dAddress;
            }
            set
            {
                _dAddress = value;
            }
        }
        public string dPostcode
        {
            get
            {
                return _dPostcode;
            }
            set
            {
                _dPostcode = value;
            }
        }
        public string dResidence
        {
            get
            {
                return _dResidence;
            }
            set
            {
                _dResidence = value;
            }
        }
        public string dCountry
        {
            get
            {
                return _dcountry;
            }
            set
            {
                _dcountry = value;
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
    }
}
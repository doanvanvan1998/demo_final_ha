using demo_final_huy;
using System;
using System.Collections.Generic;
using System.Text;

namespace onlineShoping
{
    class User: demo_final_huy.Common

    {

        private String password;
        private int uNum;
        private int sNum;
        private String streetName;
        private String streetSuffix;
        private String city;
        private int postcode;
        private String state;
        private bool checkLogin;

       
        public void setCheckLogin(bool check)
        {
            this.checkLogin = check;
        }
        public bool getCheckLogin()
        {
            return this.checkLogin;

        }


        public void setStreetName(String streetName)
        {
            this.streetName = streetName;

        }
        public String getStreetName()
        {
            return this.streetName;
        }
        public void setStreetSuffix(String streetSuffix)
        {
            this.streetSuffix = streetSuffix;

        }
        public String getStreetSuffix()
        {
            return this.streetSuffix;
        }
        public void setCity(String city)
        {
            this.city = city;


        }
        public String getCity()
        {
            return this.city;
        }
        public void setState(String state)
        {
            this.state = state;
        }
        public String getState()
        {
            return this.state;
        }
        public void setPostcode(int postcode)
        {
            this.postcode = postcode;
        }
        public int getPostcode()
        {
            return this.postcode;

        }
        public void setUNum(int uNum)
        {
            this.uNum = uNum;
        }
        public int getUNum()
        {
            return this.uNum;

        }
        public void setSNum(int sNum)
        {
            this.sNum = sNum;
        }
        public int getSNum()
        {
            return this.sNum;

        }


       
        public void setPassword(String password)
        {
            this.password = password;
        }
        public String getPassword()
        {
            return this.password;
        }
    }
}

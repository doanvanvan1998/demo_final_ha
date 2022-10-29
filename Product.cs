using demo_final_huy;
using System;
using System.Collections.Generic;
using System.Text;

namespace onlineShoping
{
    class Product : Common
    {

        private String productDescription;
        private String productPrice;
        private int bid;
        private int status;


        
        public void setStatus(int status)
        {
            this.status = status;
        }
        public int getStatus()
        {
            return this.status;

        }
        public void setBid(int bid)
        {
            this.bid = bid;
        }
        public int getSNum()
        {
            return this.bid;

        }
        
        public void setProductDescription(String productDescription)
        {
            this.productDescription = productDescription;

        }
        public String getProductDescription()
        {
            return this.productDescription;
        }
        public void setProductPrice(String productPrice)
        {
            this.productPrice = productPrice;

        }
        public String getProductPrice()
        {
            return this.productPrice;
        }

        
    }
}

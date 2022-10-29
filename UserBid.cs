using System;
using System.Collections.Generic;
using System.Text;

namespace demo_final_huy
{
    class UserBid :Common
    {

        private float amount;
        private String bidName;
        private String delivery;

        public UserBid Amount { get; internal set; }


        public void setAmount(float amount)
        {
            this.amount = amount;
        }
        public float getAmount()
        {
            return this.amount;

        }

        public void setBidName(String bidName)
        {
            this.bidName = bidName;

        }
        public String getBidName()
        {
            return this.bidName;
        }

        public void setDelivery(String delivery)
        {
            this.delivery = delivery;

        }
        public String getDelivery()
        {
            return this.delivery;
        }

    }
}

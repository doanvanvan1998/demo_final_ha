using System;
using System.Collections.Generic;
using System.Text;

namespace demo_final_huy
{
    class Common
    {
        private int id;
        private String name;
        private String email;


        public void setId(int id)
        {
            this.id = id;
        }
        public int getId()
        {
            return this.id;

        }

        public void setName(String name)
        {
            this.name = name;

        }
        public String getName()
        {
            return this.name;
        }

        public void setEmail(String email)
        {
            this.email = email;

        }
        public String getEmail()
        {
            return this.email;
        }

    }
}

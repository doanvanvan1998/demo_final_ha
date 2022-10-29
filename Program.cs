using onlineShoping;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace demo_final_huy
{
    class Program
    {
        static List<User> userList = new List<User>();
        static List<Product> productList = new List<Product>();
        static int auto_increase_id = 0;
        static int auto_increase_id_product = 0;
        static string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        static String name = "/user.txt";
        static String product = "/product.txt";
        static String bid = "/bid.txt";

        static String file_product = _filePath + product;
        static String file_bid = _filePath+ bid;

        static String file_name = _filePath + name;



        static void Main(string[] args)
        {
            Interface();
        }
        public static void Interface()
        {

           
            DisplayMainMenu();
            do
            { 
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                       
                       int first_lg= displayRegister();
                        DisplayMainMenu();
                        break;
                                  

                    case 2:

                        bool check_login = true;
                        User user_lg = new User();
                        do
                        {
                            user_lg = displayLogin();
                            if (user_lg.getCheckLogin())
                            {
                                check_login = false;
                            }
                            else
                            {
                                Console.WriteLine("Email or Password is not correct!!!");
                            }
                        } while (check_login);
                        if (user_lg.getSNum() == 0)
                        {
                            addressDisplay(user_lg);
                        }

                        do
                        {
                            loginClientDisplay();
                            int number = int.Parse(Console.ReadLine());

                            switch (number)
                            {
                                case 1:
                                    productAdvertised(user_lg);
                                    break;
                                case 2:
                                    getAllProduct(user_lg);
                                    break;
                                case 3:
                                    // search 
                                    getAllProductOther(user_lg);
                                    break;
                                case 4:
                                    getAllProductBid(user_lg);
                                    break;
                                case 5:
                                    getAllpurchased(user_lg);
                                    break;
                                case 6:
                                    Interface();
                                    break;
                            }
                        } while (true);
                    case 3:
                        Console.WriteLine("Have a good day, thanks for your time");
                        Environment.Exit(0);
                        break;
                }
            } while (true);

        }
        public static void DisplayMainMenu()
        {
            Console.WriteLine("Welcome to Auction House");
            Console.WriteLine("-------Main Menu-------");
            Console.WriteLine("-----------------------");
            Console.WriteLine("(1) Register Now");
            Console.WriteLine("(2) Sign In");
            Console.WriteLine("(3)Exit");
            Console.WriteLine("Please select from 1 to 3");
        }
        public static int displayRegister()
        {
            int check_firstLg = 0;
            

            User user = new User();
            
            //kiểm tên hợp hay k 
            bool checkName = true;
            bool statusCheckEmail = true;
            bool statusCheckPass = true;

            user.setId(auto_increase_id + 1);

            do
            {
                Console.WriteLine("Enter your username");
                String name = Console.ReadLine();
                if(name !=null && name.Length > 0)
                {
                    user.setName(name);
                    checkName = false;
                }

            } while (checkName);

            do
            {
                Console.WriteLine("enter your email");
                string email = Console.ReadLine();
                bool result = checkEmail(email);
                if (result)
                {
                    user.setEmail(email);
                    if(userList.Count != 0) {
                        foreach(User item in userList)
                        {
                            if (item.getEmail().Equals(user.getEmail()))
                            {
                                Console.WriteLine("email da ton tai");
                                break;
                            }
                            else
                            {
                                statusCheckEmail = false;
                                check_firstLg = 1;
                            }
                        }
                    }
                    else
                    {
                        statusCheckEmail = false;
                    }


                }

            } while (statusCheckEmail);

            do
            {
                Console.WriteLine("Enter your password");
                string pass = Console.ReadLine();
               bool result_pass = checkPassword(pass);
                if (result_pass)
                {
                    user.setPassword(pass);
                    statusCheckPass = false;
                }
               

            } while (statusCheckPass);
            writeToFileRegister(user, file_name);
            readFromFileAllInfor(file_name);
            return check_firstLg;
        }
        public static User displayLogin()
        {
            List<User> usersList = new List<User>();
            usersList = readFromFileAllInfor(file_name);

            User user = new User();
            Console.WriteLine("Enter your email");
            user.setEmail(Console.ReadLine());
            Console.WriteLine("Enter your password");
            user.setPassword(Console.ReadLine());
            foreach(User item in usersList)
            {
                if(user.getEmail().Equals(item.getEmail()) && user.getPassword().Equals(item.getPassword()))
                {
                    item.setCheckLogin(true);

                    return item;

                }
                else
                {
                    user.setCheckLogin(false);
                }
            }
            return user;

        }
        public static void addressDisplay(User user)
        {
           File.WriteAllText(file_name, String.Empty);
              foreach(User item in userList)
                {
                    if(user.getEmail() == item.getEmail())
                    {
                        Console.WriteLine("Please provide your home address \n Unit number");
                        item.setUNum(int.Parse(Console.ReadLine()));
                        Console.WriteLine("Street Number");
                        item.setSNum(int.Parse(Console.ReadLine()));
                        Console.WriteLine("Street Name");
                        item.setStreetName(Console.ReadLine());
                        Console.WriteLine("Street Suffix");
                        item.setStreetSuffix(Console.ReadLine());
                        Console.WriteLine("City ");
                        item.setCity(Console.ReadLine());
                        Console.WriteLine("State");
                        item.setState(Console.ReadLine());
                        Console.WriteLine("Postcode");
                        item.setPostcode(int.Parse(Console.ReadLine()));
                        Console.Write("Welcome " + user.getName());
                        Console.WriteLine("Your address will be " + item.getUNum() + " " + item.getSNum() + " " + item.getStreetName() + " " + item.getStreetSuffix() + " " + item.getCity() + " " + item.getState() + " " + item.getPostcode());

                    }
                }
            writeToFileAll(userList, file_name);

        }

        public static void loginClientDisplay()
        {
            Console.WriteLine("-----Client Menu-----");
            Console.WriteLine("(1) Advertise product");
            Console.WriteLine("(2) View My product list");
            Console.WriteLine("(3)Search for advertised product");
            Console.WriteLine("(4)View bids on my product");
            Console.WriteLine("(5) View my purchased items");
            Console.WriteLine("(6) Log off");
            Console.WriteLine("Please select from 1 to 6");

        }
        public static void deliverOption()
        {
            Console.WriteLine("-----Client Menu-----");
            Console.WriteLine("(1)Click and collect");
            Console.WriteLine("(2) Home delivery");
            
            Console.WriteLine("Please select from 1 to 2");

        }
        public static void productAdvertised(User user)
        {
            List<Product> products = readFromFileAllProduct(file_product);
            if( products.Count !=0)
            {
                auto_increase_id_product = products[products.Count - 1].getId();
            }
            else
            {
                auto_increase_id_product = 0;
            }
           
            auto_increase_id_product++;
            bool check_decr = true;
            Product product = new Product();
            Console.WriteLine("Product Name :");
            product.setId(auto_increase_id_product);
            product.setName(Console.ReadLine());
            do
            {
                Console.WriteLine("Product description");
                product.setProductDescription(Console.ReadLine());
                if (!product.getName().Equals(product.getProductDescription()))
                {
                    check_decr = false;
                }
                
            } while (check_decr);
            
            Console.WriteLine("Product Price");
            product.setProductPrice(Console.ReadLine());
            Console.WriteLine("Successfully added product " + product.getName() + " " + product.getProductDescription() + " " + product.getProductPrice());
            product.setEmail(user.getEmail());
            product.setStatus(0);
            writeToFileProduct(product, file_product);

        }
        public static void getAllProduct(User user)
        {
            int index = 0;
            readFromFileAllProduct(file_product);
            productList.Sort((a, b) => a.getName().CompareTo(b.getName()));
            Console.WriteLine("STT    Product name             Description           ProductPrice       MiddName        Email         Amount");
            // đọc từ file bid danh sánh đấu giá

         

            foreach (Product product in productList)
            {
                if (user.getEmail().Equals(product.getEmail()) && product.getStatus() !=1)
                {
                    index++;
                    UserBid userBid = getBidMaxById(product.getId());
                   
                        displayProductInforSelf(product, index, userBid);
                    
                   
                }
               
            }
        }


        public static void getAllProductOther(User user)
        {
            readFromFileAllProduct(file_product);
            productList.Sort((a, b) => a.getName().CompareTo(b.getName()));


            Console.WriteLine("input name product");
            String name_search = Console.ReadLine();
            int index = 0;
            int select = 0;
            if (name_search.ToLower().Equals("all"))
            {
                Console.WriteLine(" STT          Product name             Description           ProductPrice");
                foreach (Product product in productList)
                {
                    if (!user.getEmail().Equals(product.getEmail()))
                    {
                        index++;
                        UserBid userBid = getBidMaxById(product.getId());

                        displayProductInforSelf(product, index, userBid);
                    }

                }
            }
            else
            {
                Console.WriteLine("Product name             Description           ProductPrice");
                foreach (Product product in productList)
                {
                    if (!user.getEmail().Equals(product.getEmail()) &&product.getName().Contains(name_search) || product.getProductDescription().Contains(name_search))
                    {
                        index++;
                        UserBid userBid = getBidMaxById(product.getId());

                        displayProductInforSelf(product, index, userBid);
                    }

                }

            }
            Console.WriteLine("bạn có muốn trả giá sản phầm nào k yes or no");
            String resutl = Console.ReadLine();
            if (resutl.Equals("yes"))
            {
                bool check_amount = true;
                Console.WriteLine("bạn chọn sản phẩm thứ mấy");
                select = int.Parse(Console.ReadLine());
                // lấy ra cái đấu giá cao nhất của sản phẩm 
                
                  UserBid max_user = getBidMaxById(productList[select - 1].getId());
                     float amount = 0;
                do
                {

                    Console.WriteLine("Bạn muốn trả bao nhiểu");
                    String key = Console.ReadLine();
                   
                    if (key.Contains("$"))
                    {
                        amount = float.Parse(key.Remove(0,1));
                        if(max_user != null) {
                            if (amount > max_user.getAmount())
                            {
                                check_amount = false;
                            }
                        }
                        else 
                        {
                            check_amount = false;
                        }

                        
                      
                    }
                } while (check_amount);
               
                
                Console.WriteLine("Bạn đã trả "+ productList[select-1].getName());
                Console.WriteLine("id cua sp " + productList[select - 1].getId());

                UserBid userBid = new UserBid();
                userBid.setId(productList[select - 1].getId());
                userBid.setName(productList[select - 1].getName());
                userBid.setEmail(user.getEmail());
                userBid.setBidName(user.getName());
                userBid.setAmount(amount);

               
                deliverOption();
                int input_number = int.Parse(Console.ReadLine());
                String delivery;
                if (input_number == 1 )
                {
                    // click and collect
                 DateTime dateTimeStart =   pickTimeDeliveryStart();
                 DateTime dateTimeEnd = pickTimeDeliveryEnd(dateTimeStart);
                    delivery = dateTimeStart.ToString() + " " + dateTimeEnd.ToString();
                    userBid.setDelivery(delivery);
                }
                if (input_number == 2)
                {
                    bool check_address = true;
                    bool check_Snumb = true;
                    bool check_postCode = true;
                    int address=0;
                    do
                    {
                        try {
                            Console.WriteLine("Please provide your home address \n Unit number");
                            address  = int.Parse(Console.ReadLine());
                            if(address >= 0)
                            {
                                check_address = false;

                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Nhập vào k hợp lệ");
                        }
                    } while (check_address);

                    int sNum = 0;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Street Number");
                             sNum = int.Parse(Console.ReadLine());
                            check_Snumb = false;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("nhap k hop le");
                        }
                        
                    }while (check_Snumb) ;

                    
                    Console.WriteLine("Street Name");
                    String streetName =Console.ReadLine();
                    Console.WriteLine("Street Suffix");
                    String streetSuffix =Console.ReadLine();
                    Console.WriteLine("City ");
                    String city =Console.ReadLine();
                    Console.WriteLine("State");
                    String state =Console.ReadLine();

                    int postcode = 0;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Postcode");
                             postcode = int.Parse(Console.ReadLine());
                            if(1000 <= postcode && postcode <= 9999)
                            {
                                check_postCode = false;
                            }
                           
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("nhap k hop le");
                        }
                       


                    } while (check_postCode) ;
                    Console.Write("Welcome " + user.getName());
                    // check address = 0 thì bỏ address bên dưới đi
                    delivery = address + " " + sNum + " " + streetName + " " + streetSuffix + " " + city + " " + state + " " + postcode;
                    userBid.setDelivery(delivery);
                }
                writeToFileBid(userBid, file_bid);

               
            }
            else
            {
                // đưa về menu
            }

        }

        public static DateTime pickTimeDeliveryStart()
        {
            bool check = true;
            DateTime now = DateTime.Now;
            DateTime date_input;
            do
            {
                Console.WriteLine("nhập time start");
                date_input = DateTime.Parse(Console.ReadLine());


                
                if (date_input.Day == now.Day && date_input.Hour - now.Hour >= 1 && date_input.Year>=now.Year && date_input.Month>0 && date_input.Month <13 && date_input.Month>=now.Month)
                {
                    check = false;
                  
                }
                else if (date_input.Day > now.Day && date_input.Year >= now.Year && date_input.Month > 0 && date_input.Month < 13 && date_input.Month >= now.Month)
                {
                    check = false;
                   
                }
                else
                {
                    Console.WriteLine("lỗi thời gian");

                }
               
            } while (check);
            return date_input;
        }

        public static DateTime pickTimeDeliveryEnd(DateTime timeStart)
        {
            bool check = true;
           
            DateTime date_input;
            do
            {
                Console.WriteLine("nhập time end");

                date_input = DateTime.Parse(Console.ReadLine());

                if (date_input.Day == timeStart.Day && date_input.Hour - timeStart.Hour >= 1 && date_input.Year >= timeStart.Year && date_input.Month > 0 && date_input.Month < 13 && date_input.Month >= timeStart.Month)
                {
                    check = false;

                }
                else if (date_input.Day > timeStart.Day && date_input.Year >= timeStart.Year && date_input.Month > 0 && date_input.Month < 13 && date_input.Month >= timeStart.Month)
                {
                    check = false;

                }
                else
                {
                    Console.WriteLine("lỗi thời gian");

                }

            } while (check);
            return date_input;
        }










        public static void displayProductInforSelf(Product product,int index ,UserBid userBid)
        {
            if(userBid != null)
            {
                Console.WriteLine(index + "   " + product.getName() + "                      " + product.getProductDescription() + "                    " + "$"+product.getProductPrice() + "      "+ userBid.getBidName()+"            " + userBid.getEmail() + "        " + userBid.getAmount());
            }
            else
            {
                Console.WriteLine(index + "   " + product.getName() + "                      " + product.getProductDescription() + "                    " + "$" + product.getProductPrice() + "          "+ "-" + "         " + "-" + "           " +"-");
            }
           
        }
        public static void displayProductInfor(int index, Product product)
        {
           
            Console.WriteLine(index+ "           "+ product.getName() + "                      " + product.getProductDescription() + "                    " + product.getProductPrice());
        }

        public static Boolean checkEmail(string email)
        {
            
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            if (trimmedEmail.EndsWith("/"))
            {
                return false; // suggested by @TK-421
            }
            if (trimmedEmail.EndsWith("_"))
            {
                return false; // suggested by @TK-421
            }


            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                String[] str_split = email.Split(".");
                bool isIntString = str_split[1].Any(char.IsDigit);
                if (isIntString)
                {
                    return false;
                }
                else
                {
                    return true;
                }   
            }
            catch
            {
                return false;
            }
        }
        public static bool checkPassword(String input)
        {
 
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input);
            return isValidated;
        }

        
        public static List<User> readFromFileAllInfor(String path)
        //C:/Users/dmx/source/repos/demo_final_huy/user.txt
        {
            List<User> listUser = new List<User>();
            try
            {

                using (StreamReader sr = new StreamReader(path))
                {

                    string line;
                    //int step = 0;

                    while ((line = sr.ReadLine()) != null)
                    {

                        User user = new User();
                        String[] data = line.Split("        ");
                        user.setId(int.Parse(data[0]));
                        user.setName(data[1]);
                        user.setEmail(data[2]);
                        user.setPassword(data[3]);

                        user.setUNum(int.Parse(data[4]));
                        user.setSNum(int.Parse(data[5]));
                        user.setStreetName(data[6]);
                        user.setStreetSuffix(data[7]);

                        user.setCity(data[8]);
                        user.setPostcode(int.Parse(data[9]));
                        user.setState(data[10]);
                        listUser.Add(user);


                        // step++;
                    }

                }
                userList = listUser;
                return userList;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }








        public static void writeToFileRegister(User user,String path)
        {
           
            //C:/Users/dmx/source/repos/demo_final_huy/user.txt"

            try
            {

                    using (StreamWriter sw = new StreamWriter(path, true))
                    {

                        sw.Write((user.getId()).ToString());
                        sw.Write("        ");
                        sw.Write(user.getName());
                        sw.Write("        ");
                        sw.Write((user.getEmail()).ToString());
                        sw.Write("        ");
                        sw.Write((user.getPassword()).ToString());
                        sw.Write("        ");
                        sw.Write("0");
                        sw.Write("        ");


                        sw.Write("0");
                        sw.Write("        ");


                        sw.Write("0");
                        sw.Write("        ");


                        sw.Write("0");
                        sw.Write("        ");


                        sw.Write("0");
                        sw.Write("        ");


                        sw.Write("0");
                        sw.Write("        ");


                        sw.Write("0");
                        sw.Write("        ");

                        sw.WriteLine();

                    }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void writeToFileAll(List<User> usersList, String path)
        {
            int index = 0;
            //C:/Users/dmx/source/repos/demo_final_huy/user.txt"
            foreach(User user in usersList)
            {
               
                try
                {

                    using (StreamWriter sw = new StreamWriter(path, true))
                    {



                            sw.Write((user.getId()).ToString());
                            sw.Write("        ");
                            sw.Write(user.getName());
                            sw.Write("        ");
                            sw.Write((user.getEmail()).ToString());
                            sw.Write("        ");
                            sw.Write((user.getPassword()).ToString());
                            sw.Write("        ");

                            sw.Write((user.getUNum()).ToString());
                            sw.Write("        ");


                            sw.Write((user.getSNum()).ToString());
                            sw.Write("        ");


                            sw.Write((user.getStreetName()).ToString());
                            sw.Write("        ");


                            sw.Write((user.getStreetSuffix()).ToString());
                            sw.Write("        ");


                            sw.Write((user.getCity()).ToString());
                            sw.Write("        ");


                            sw.Write((user.getPostcode()).ToString());
                            sw.Write("        ");


                            sw.Write((user.getState()).ToString());
                            sw.Write("        ");
                            sw.WriteLine();
                           
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


        }




        public static void writeToFileProduct(Product product, String path)
        {
        
            try
            {

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write((product.getId()).ToString());
                    sw.Write("        ");
                    sw.Write((product.getName()).ToString());
                    sw.Write("        ");
                    sw.Write(product.getProductDescription());
                    sw.Write("        ");
                    sw.Write((product.getProductPrice()).ToString());
                    sw.Write("        ");
                    sw.Write((product.getEmail()).ToString());
                    sw.Write("        ");
                    sw.Write((product.getStatus()).ToString());
                    sw.Write("        ");
                    sw.WriteLine();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static List<Product> readFromFileAllProduct(String path)
        //C:/Users/dmx/source/repos/demo_final_huy/user.txt
        {
            List<Product> productsList = new List<Product>();
            try
            {

                using (StreamReader sr = new StreamReader(path))
                {

                    string line;
                    //int step = 0;

                    while ((line = sr.ReadLine()) != null)
                    {

                        Product prod_data = new Product();
                        String[] data = line.Split("        ");
                        prod_data.setId(int.Parse(data[0]));
                        prod_data.setName(data[1]);
                        prod_data.setProductDescription(data[2]);
                        prod_data.setProductPrice(data[3]);
                        prod_data.setEmail(data[4]);
                        prod_data.setStatus(int.Parse(data[5]));
                        productsList.Add(prod_data);
                        // step++;
                    }

                }
                productList = productsList;
                return productList;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public static void writeToFileBid(UserBid userBid, String path)
        {

            try
            {

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write((userBid.getId()).ToString());
                    sw.Write("        ");
                    sw.Write((userBid.getName()).ToString());
                    sw.Write("        ");
                    sw.Write(userBid.getEmail());
                    sw.Write("        ");
                    sw.Write((userBid.getAmount()).ToString());
                    sw.Write("        ");
                    sw.Write((userBid.getBidName()).ToString());
                    sw.Write("        ");
                    sw.Write((userBid.getDelivery()).ToString());
                    sw.Write("        ");
                    sw.WriteLine();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static List<UserBid> readFromFileAllBid(String path)
        //C:/Users/dmx/source/repos/demo_final_huy/user.txt
        {
            List<UserBid> userBidList = new List<UserBid>();
            try
            {

                using (StreamReader sr = new StreamReader(path))
                {

                    string line;
                    //int step = 0;

                    while ((line = sr.ReadLine()) != null)
                    {

                        UserBid userBid_data = new UserBid();
                        String[] data = line.Split("        ");
                        userBid_data.setId(int.Parse(data[0]));
                        userBid_data.setName(data[1]);
                        userBid_data.setEmail(data[2]);
                        userBid_data.setAmount(float.Parse(data[3]));
                        userBid_data.setBidName(data[4]);
                        userBid_data.setDelivery(data[5]);
                        userBidList.Add(userBid_data);
                        // step++;
                    }

                }
                return userBidList;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public static UserBid getBidMaxById(int id)
        {
            List<UserBid> userBids = readFromFileAllBid(file_bid);
            List<UserBid> list_By_Id = new List<UserBid>();
            if(userBids != null)
            {
                foreach (UserBid userBid in userBids)
                {
                    if (userBid.getId() == id)
                    {
                        list_By_Id.Add(userBid);
                    }
                }
                if (list_By_Id.Count > 0)
                {
                    UserBid max_user_pos = list_By_Id[0];
                    foreach (UserBid item in list_By_Id)
                    {
                        if (item.getAmount() > max_user_pos.getAmount())
                        {
                            max_user_pos = item;
                        }
                    }
                    return max_user_pos;
                }
                else
                {
                    return new UserBid();
                }
            }
            return new UserBid();

        }
        public static void getAllProductBid(User user)
        {

            int index = 0;
            readFromFileAllProduct(file_product);
            productList.Sort((a, b) => a.getName().CompareTo(b.getName()));
            Console.WriteLine("STT    Product name             Description           ProductPrice       MiddName        Email         Amount");
            // đọc từ file bid danh sánh đấu giá



            foreach (Product product in productList)
            {
                if (user.getEmail().Equals(product.getEmail()))
                {
                    index++;
                    UserBid userBid = getBidMaxById(product.getId());
                    if(userBid != null)
                    {
                        Console.WriteLine(index + "   " + product.getName() + "                      " + product.getProductDescription() + "                    " + product.getProductPrice() + "      " + userBid.getBidName() + "            " + userBid.getEmail() + "        " + userBid.getAmount());
                    }
                   
                }


            }

            Console.WriteLine("bạn có muốn bán sp nào ");
            int select = int.Parse(Console.ReadLine());
            productList[select - 1].setStatus(1);
            File.WriteAllText(file_product, String.Empty);
            foreach (Product pr in productList)
            {
                writeToFileProduct(pr, file_product);
            }
        }

        public static void getAllpurchased(User user)
        {
            int index = 0;
            readFromFileAllProduct(file_product);
            readFromFileAllInfor(file_name);
            foreach(Product product in productList)
            {
                if(product.getStatus() == 1)
                {
                    index++;
                    UserBid userBid = getBidMaxById(product.getId());
                    Console.WriteLine(index + "  " + product.getEmail() + "   " + product.getName() + "  " + product.getProductDescription() +"   " + product.getProductPrice() + " " + userBid.getAmount() + "   " + userBid.getDelivery());
                }

            }

        }
           
    }
}

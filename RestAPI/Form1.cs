using RestSharp;
using Newtonsoft.Json;
using ClassLib1;

namespace RestAPI
{
    public partial class Form1 : Form
    {
        //How to call REST API in C# console app
        //Content of this script from https://youtu.be/qm29vYcYBeg
        
        //Note: all the classes are in the class library (classLib1/EmpConnection), to add this, create a
        //new project (ClassLib1) and add a ref to the classlib by right clicking "dependancies" and
        //adding a reference to the new project

        //Note: Requires 2x NuGet packages:
        //RestSharp 
        //Newton.Json
        public Form1()
        {
            InitializeComponent();
        }

        //Global var
        EmpConnection.Rootobject result;  //Stores result of API conversion
        private void getData()
        {
            //Calls the API
            //Needs 'using RestSharp' <-- see line 1!
            //Source --> https://dummy.restapiexample.com/
            //API to be used --> https://dummy.restapiexample.com/api/v1/employees
            //Note the difference, I have chopped off the final bit of the address!
            var client = new RestClient("https://dummy.restapiexample.com/api/v1/");

            var request = new RestRequest("employees");  //The last bit of the api address
            var response = client.Execute(request);  //Request is ready

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;  //Raw data (needs refinement!)

                //Refinement ideally requires "PostMan" as this will show you what the 
                //data will look like after parsing and then allow you to use
                //Edit/Paste as JSON classes, I simply copied the structure into the 
                //Classes below

                //Convert the raw data (next line requires 'using Newton.Json'
                result = JsonConvert.DeserializeObject<EmpConnection.Rootobject>(rawResponse);

                if (result != null)
                {
                    foreach (var obj in result.data)
                    {
                        listBox1.Items.Add(obj.employee_name);
                    }
                }


            }

        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            getData();
        }

        
    }

  



}
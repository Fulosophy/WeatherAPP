using Newtonsoft.Json;
using RestSharp;

using System.Text.Json;

namespace WeatherAPP
{
    public partial class Form1 : Form
    {
        


        public Form1()
        {			        
            

            InitializeComponent();
            /*
             * Section for Clear Labels since winforms is terrible
             * 
             * 
            */
            L_zipcode.Parent = pictureBox1;
            L_zipcode.BackColor = Color.Transparent;
            L_CITYNAME.Parent = pictureBox1;
            L_CITYNAME.BackColor = Color.Transparent;
            L_FEELSLIKE.Parent = pictureBox1;
            L_FEELSLIKE.BackColor = Color.Transparent;
            L_Forecast.Parent = pictureBox1;
            L_Forecast.BackColor = Color.Transparent;
            L_HUMIDITY.Parent = pictureBox1;
            L_HUMIDITY.BackColor = Color.Transparent;
            
            //testlabel.Parent = pictureBox1;  INVISIBLE testing lost it lol.
            //testlabel.BackColor = Color.Transparent;
             // End //
            // Adding BitMap Images
          

            // End // 


        }

        private async void FetchWeather_ClickAsync(object sender, EventArgs e)
        {
            // Bitmap Images //
            Bitmap img1 = Properties.Resources.ClearSkies;
            Bitmap img2 = Properties.Resources.OverCastClouds;
            Bitmap SunnyDay = Properties.Resources.Sunny;
            // End Bitmap Images // 


            Console.WriteLine("Please enter a zipe code: ");
            string address = textBox1.Text;
            

            string API_KEY = "NULL";
            string BASE_URL = "http://api.openweathermap.org/data/2.5/weather";
            string MAIN_URL = $"{BASE_URL}?zip={address}&units=imperial&lang=en&appid={API_KEY}";
            string ZIPPO_BASE = "http://api.zippopotam.us/us/";
            string ZIPPO_URL = $"{ZIPPO_BASE}{address}";

            var request = new RestRequest(MAIN_URL);
            var client = new RestClient(MAIN_URL);

            var getStateID = new RestRequest(ZIPPO_URL);
            var clientStateID = new RestClient(ZIPPO_URL);

            // If the field is blank and bad request is sent back.
            if (textBox1.Text == "")
            {
                MessageBox.Show("You've entered an incorrect zipcode!", "Error!");

            }
            // Error checking to see if zipcode is valid 
            else
            {
                
                var response = await client.GetAsync(request);
                var stateResponse = await clientStateID.GetAsync(getStateID);

                if (response.Content.Contains("\"cod\":\"404\""))
                {
                    MessageBox.Show("You've entered an incorrect zipcode!", "Error!");
                    // Resets the text box to blank.
                    textBox1.Text = null;

                }
                // Main API Calling
                else
                {
                    Weather.root inf = JsonConvert.DeserializeObject<Weather.root>(response.Content.ToString());
                    StateCode.root statecd = JsonConvert.DeserializeObject<StateCode.root>(stateResponse.Content.ToString());
                    
                    //  Weather tst = JsonConvert.DeserializeObject<Weather.root>(response.Content.ToString());
                    //MessageBox.Show(response.Content);
                    int FeelsLikeConversion = (int)inf.main.temp;
                    feelsBox.Text = string.Format("{0}\u00B0F", FeelsLikeConversion.ToString());         
                    T_cityBox.Text = inf.name.ToUpper().ToString() +", " + statecd.places[0].stateabrv.ToString();
                    foreCast.Text = inf.Weather[0].description[0].ToString().ToUpper() + inf.Weather[0].description.Substring(1);
                    T_HUMIDITY.Text = inf.main.humidity.ToString() + "%";
                    
                   // MessageBox.Show(statecd.places[0].stateabrv.ToString());




                    switch (inf.Weather[0].description)
                    {
                        case "clear sky":
                            //P_FORECAST.Image = SunnyDay;
                            pictureBox1.Image = SunnyDay;
                            break;
                        case "overcast clouds":
                            pictureBox1.Image = img2;
                            break;

                        case "broken clouds":
                            pictureBox1.Image = img2;
                            break;
                    }


                    //if (inf.Weather[0].description == "clear sky")
                    //    pictureBox1.Image = img1;
                    //else if (inf.Weather[0].description == "overcast clouds")
                    //    pictureBox1.Image = img2;








                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // place holder 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Feels_Like_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

using HendoHealth.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace HendoHealth
{
    public partial class measurements : System.Web.UI.Page
    {
        //private Library.Connect iHealth = new Library.Connect();
        static StringContent username;
        string user = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack && HttpContext.Current.Session.IsNewSession)
            {
                HttpContext.Current.Response.Redirect(Properties.Resources.urltokencallback);
            }
            else
            { 
                user = @"<?xml version=""1.0"" encoding=""utf-8""?><user><username>"+Session["username"].ToString()+"</username>"+"</user>";
                username = new StringContent(user, Encoding.UTF8, "application/xml");
            }
        }
        private async Task<string> PutExternalData(String URL, StringContent content)
        {
            var client = new HttpClient();
            var message = await client.PostAsync(Properties.Resources.urlbackendtest + URL, content);
            message.EnsureSuccessStatusCode();
            var responseString = await message.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("La risposta=  " + responseString.ToString());
            return responseString;
        }

        protected async void Blood_Click(object sender, EventArgs e)
        {
                string file = await PutExternalData("getBloodPressure", username);
                List<bloodpressuredata> datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<bloodpressuredata>>(file);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Blood Pressure", typeof(int));
                dataTable.Columns.Add("Sistolic Pressure", typeof(int));
                dataTable.Columns.Add("Heart Rate", typeof(int));
                dataTable.Columns.Add("Diastolic Pressure", typeof(int));
                dataTable.Columns.Add("Notes", typeof(string));
                dataTable.Columns.Add("Date", typeof(DateTime));
                foreach (var data in datas)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Blood Pressure"] = data.BPL;
                    dataRow["Sistolic Pressure"] = data.HP;
                    dataRow["Heart Rate"] = data.HR;
                    dataRow["Diastolic Pressure"] = data.LP;
                    dataRow["Notes"] = data.Note;
                    dataRow["Date"] = DateTime.ParseExact(data.date.Replace("[UTC]",""), "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", new CultureInfo("en-UK"), DateTimeStyles.None);
                    dataTable.Rows.Add(dataRow);
                    bloodpressure.DataSource = dataTable;
                    bloodpressure.DataBind();
                    using (var control = new medical_valuesEntities())
                    {
                        if (control.bloodpressuredata.Any(o => o.Id == data.Id))
                        {
                            // do nothing
                        }

                        else
                        {
                            data.username = Session["username"].ToString();
                            control.bloodpressuredata.Add(data);
                            try
                            {
                                control.SaveChanges();
                            }
                            catch (DbEntityValidationException exception)
                            {
                                System.Diagnostics.Debug.WriteLine(exception.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage.ToString());
                            }
                        }
                    }
                }
        }

        protected async void Oxygen_Click(object sender, EventArgs e)
        {
            string file = await PutExternalData("getPulseOximeter", username);
            List<pulseoximeterdata> datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<pulseoximeterdata>>(file);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SpO2", typeof(int));
            dataTable.Columns.Add("Pulse", typeof(int));
            dataTable.Columns.Add("IP", typeof(float));
            dataTable.Columns.Add("Note", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
                foreach (var data in datas)
                {
                    DataRow row = dataTable.NewRow();
                    row["SpO2"] = data.SpO2;
                    row["Pulse"] = data.Pulse;
                    row["IP"] = data.IP;
                    row["Note"] = data.Note;
                    row["Date"] = DateTime.ParseExact(data.date.Replace("[UTC]", ""), "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", new CultureInfo("en-UK"), DateTimeStyles.None);
                    dataTable.Rows.Add(row);
                    bloodoxygen.DataSource = dataTable;
                    bloodoxygen.DataBind();
                    using (var control = new medical_valuesEntities())
                    {
                        if (control.pulseoximeterdata.Any(o => o.Id == data.Id))
                        {
                            // do nothing
                        }
                        else
                        {
                            data.username = Session["username"].ToString();
                            control.pulseoximeterdata.Add(data);
                            try
                            {
                                control.SaveChanges();
                            }
                            catch (DbEntityValidationException exception)
                            {
                                System.Diagnostics.Debug.WriteLine(exception.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage.ToString());
                            }
                        }
                    }
                }
        }

        protected async void GlycemieBtn_Click(object sender, EventArgs e)
        {
                string file = await PutExternalData("getGlycemie", username);
                List<glycemiedata> datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<glycemiedata>>(file);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("BG", typeof(int));
                dataTable.Columns.Add("Dinner Situation", typeof(string));
                dataTable.Columns.Add("Drugs Situation", typeof(string));
                dataTable.Columns.Add("Note", typeof(string));
                dataTable.Columns.Add("Date", typeof(string));
                foreach(var data in datas)
                {
                    DataRow row = dataTable.NewRow();
                    row["BG"] = data.BG;
                    row["Dinner Situation"] = data.DinnerSituation;
                    row["Drugs Situation"] = data.DrugsSituation;
                    row["Note"] = data.Note;
                    row["Date"] = DateTime.ParseExact(data.date.Replace("[UTC]", ""), "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", new CultureInfo("en-UK"), DateTimeStyles.None);
                    dataTable.Rows.Add(row);
                    glycemie.DataSource = dataTable;
                    glycemie.DataBind();
                    using (var control = new medical_valuesEntities())
                    {
                        if (control.glycemiedata.Any(o => o.Id == data.Id))
                        {
                            // do nothing
                        }
                        else
                        {
                            data.username = Session["username"].ToString();
                            control.glycemiedata.Add(data);
                            try
                            {
                                control.SaveChanges();
                            }
                            catch (DbEntityValidationException exception)
                            {
                                System.Diagnostics.Debug.WriteLine(exception.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage.ToString());
                            }
                        }
                    }
                }
        }

        protected async void WeightBtn_Click(object sender, EventArgs e)
        {
                string file = await PutExternalData("getWeight", username);
                List<weightdata> datas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<weightdata>>(file);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Weight", typeof(float));
                dataTable.Columns.Add("Water Mass", typeof(float));
                dataTable.Columns.Add("VGV", typeof(int));
                dataTable.Columns.Add("Muscle Mass", typeof(float));
                dataTable.Columns.Add("BMI", typeof(float));
                dataTable.Columns.Add("DCI", typeof(int));
                dataTable.Columns.Add("Bones Mass", typeof(float));
                dataTable.Columns.Add("Body Fat", typeof(float));
                dataTable.Columns.Add("Note", typeof(string));
                dataTable.Columns.Add("Date", typeof(string));
                foreach (var data in datas)
                {
                    DataRow row = dataTable.NewRow();
                    row["Weight"] = data.WeightValue;
                    row["Water Mass"] = data.WaterValue;
                    row["VGV"] = data.VGV;
                    row["Muscle Mass"] = data.MuscleMass;
                    row["BMI"] = data.BMI;
                    row["DCI"] = data.DCI;
                    row["Bones Mass"] = data.BoneValue;
                    row["Body Fat"] = data.FatValue;
                    row["Note"] = data.Note;
                    row["Date"] = DateTime.ParseExact(data.date.Replace("[UTC]", ""), "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", new CultureInfo("en-UK"), DateTimeStyles.None);
                    dataTable.Rows.Add(row);
                    weight.DataSource = dataTable;
                    weight.DataBind();
                    using (var control = new medical_valuesEntities())
                    {
                        if (control.weightdata.Any(o => o.Id == data.Id))
                        {
                            // do nothing
                        }
                        else
                        {
                            data.username = Session["username"].ToString();
                            control.weightdata.Add(data);
                            try
                            {
                                control.SaveChanges();
                            }
                            catch (DbEntityValidationException exception)
                            {
                                System.Diagnostics.Debug.WriteLine(exception.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage.ToString());
                            }
                        }
                    }
                }
            }

        protected void logOut_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Remove("username");
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect("https://localhost:1025/iHendoTotem/index.aspx");
        }
    }
}
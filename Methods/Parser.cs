using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFParsing.Models;

namespace WPFParsing.Methods
{
    public static class Parser
    {
        public static Contractor? Parse(string input)
        {

            Contractor contractor = new Contractor();

            JObject jObject = JObject.Parse(input);
            var test = jObject.GetType();
            //List<JObject> jObjects = (List<JObject>)JObject.Parse(input).Values();
            try
            {
                contractor.GUID = jObject["ГУИД"].ToString();
                contractor.RegDate = Convert.ToDateTime(jObject["ДатаРегистрации"].ToString());
                contractor.Name = jObject["Наименование"].ToString();
            }
            catch
            {
                return null;
            }

            JObject jTextData = JObject.Parse(jObject["ТекстовыеДанные"].ToString());
            IList<JToken> contacts = jTextData["ТекстовыеДанные"]["КонтактнаяИнформация"].Children().ToList();

            foreach (JToken token in contacts)
            {
                switch (token["ВидКИ"]["ИмяПредопределенныхДанных"].ToString())
                {
                    case "ФактическийАдресКонтрагента":
                        contractor.Addres = token["ПредставлениеКИ"].ToString();
                        break;

                    case "ТелефонКонтрагента":
                        contractor.Phone = token["ПредставлениеКИ"].ToString();
                        break;
                }
            }
            return contractor;
        }
    }
}

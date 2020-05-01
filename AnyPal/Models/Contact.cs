using System;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AnyPal.Models
{
    public class Contact
    {
        string ContactKeyID = "Contact";
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public bool HasContact()
        {
            bool hasKey = Preferences.ContainsKey(ContactKeyID);
            return hasKey;
        }
        
        public async Task<List<Contact>> GetContacts()
        {
            List<Contact> list = new List<Contact>();
            List<Contact> sort = new List<Contact>();
            string ijson = Preferences.Get(ContactKeyID, "");
            if (!string.IsNullOrEmpty(ijson))
                sort = JsonConvert.DeserializeObject<List<Contact>>(ijson);

            foreach (Contact i in sort.OrderBy(x => x.Email))
            {
                list.Add(i);
            }
            return await Task.FromResult(list);
        }

        public bool AddItem(Contact item)
        {
            bool isGood = false;
            try
            {
                item.CreateDate = DateTime.Now;
                bool bItems = Preferences.ContainsKey(ContactKeyID);
                if (bItems)
                {
                    string ijson = Preferences.Get(ContactKeyID, "");
                    var list = JsonConvert.DeserializeObject<List<Contact>>(ijson);
                    //list.Add(item);
                    bool bAlreadyExist = false;
                    Contact existContact = list.Where(x => x.Email == item.Email).FirstOrDefault();
                    if(existContact != null)
                    {
                        bAlreadyExist = true;
                    }
                    if (bAlreadyExist == false)
                    {
                        list.Add(item);
                        string jsonEnum = JsonConvert.SerializeObject(list);
                        Preferences.Set(ContactKeyID, jsonEnum);
                    }
                    isGood = true;
                }
                else
                {
                    //first one so add it.                                      
                    List<Contact> list = new List<Contact>();
                    list.Add(item);
                    string jsonItem = JsonConvert.SerializeObject(list);
                    Preferences.Set(ContactKeyID, jsonItem);
                    isGood = true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return isGood;
        }

        public async Task<bool> AddNewItem(Contact item)
        {
            bool isGood = false;
            try
            {
                item.CreateDate = DateTime.Now;
                bool bItems = Preferences.ContainsKey(ContactKeyID);
                if (bItems)
                {
                    string ijson = Preferences.Get(ContactKeyID, "");
                    List<Contact> list = JsonConvert.DeserializeObject<List<Contact>>(ijson);
                    
                    list.Add(item);

                    bool bAlreadyExist = false;
                    foreach (Contact c in list)
                    {
                        if (c.Email == item.Email)
                        {
                            bAlreadyExist = true;
                        }
                    }

                    if (bAlreadyExist == false)
                    {
                        string jsonEnum = JsonConvert.SerializeObject(list);
                        Preferences.Set(ContactKeyID, jsonEnum);
                    }
                }
                else
                {
                    //first one so add it. 
                    item.CreateDate = DateTime.Now;
                    List<Contact> list = new List<Contact>();
                    list.Add(item);
                    string jsonItem = JsonConvert.SerializeObject(list);
                    Preferences.Set(ContactKeyID, jsonItem);
                }
                isGood = true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return await Task.FromResult(isGood);

        }

        public async Task<bool> UpdateItem(Contact uItem)
        {
            bool isGood = false;
            List<Contact> list = new List<Contact>();
            
            string ijson = Preferences.Get(ContactKeyID, "");
            try
            {
                list = JsonConvert.DeserializeObject<List<Contact>>(ijson);
                Contact i = new Contact();
                i = list.First(x => x.Email == uItem.Email);
                uItem.CreateDate = i.CreateDate;
                list.Remove(i);
                if (i != null)
                {
                    list.Add(uItem);
                    string jsonItem = JsonConvert.SerializeObject(list);
                    Preferences.Set(ContactKeyID, jsonItem);
                    isGood = true;
                }
            }
            catch
            {

            }
            return await Task.FromResult(isGood);
        }

        public async Task<bool> DeleteItem(Contact dItem)
        {
            List<Contact> list = new List<Contact>();
            bool isGood = false;
            try
            {
                string ijson = Preferences.Get(ContactKeyID, "");
                list = JsonConvert.DeserializeObject<List<Contact>>(ijson);
                if (list.Count > 0)
                {
                    Contact i = list.Where(x => x.Email == dItem.Email).FirstOrDefault();
                    list.Remove(i);
                    string jsonItem = JsonConvert.SerializeObject(list);
                    Preferences.Set(ContactKeyID, jsonItem);
                    isGood = true;
                }
            }
            catch
            {

            }

            return await Task.FromResult(isGood);
        }
    }
}

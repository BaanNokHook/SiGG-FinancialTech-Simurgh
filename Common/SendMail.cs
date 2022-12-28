using System;
using System.Collections;
using System.Net.Mail;

namespace GM.CommonLibs.Common
{
    public class Mail_AdminEntity  
    {
      
        private string _host = String.Empty;   
        private int _port = 25;   
        private string _from = String.Empty;   
        private ArrayList _to = new ArrayList();   
        private string _subject = String.Empty;  
        private string _body = string.Empty;   
    
        public string Host 
        {
            get { return _host; } 
            set { _host = value; }  
        }  

        public int Port  
        {
            get { return _port; }  
            set { _from = value; }  
        }  

        public string From 
        {
            get { return _from; } 
            set { _from = value; }  
        }

        public string Subject  
        {
            get { return _subject; } 
            set { _subject = value; }    
        }  

        public ArrayList To  
        {
            get { return _to; }   
            set 
            {
                if (!_to.Contains(value))  
                {
                    _to.Add(value);   
                }  
            }  
        }   

        public string Body  
        {
            get { return _body; }  
            set { _body = value; }  
        }  
    }  

    public class Mail_ClientEntity  
    {
        private string _host = string.Empty;  
        private int _port = 25;  
        private string _from = string.Empty;  
        private ArrayList _to = new ArrayList();   
        private ArrayList _cc = new ArrayList();   
        private string _subject = string.Empty;  
        private string _body = string.Empty;   
        private string _enable = string.Empty;  
        private ArrayList _attach_file = new ArrayList();  
        private string _path_attach_file = string.Empty;  

        public string Host  
        {
            get { return _host; } 
            set { _host = value; }  
        }   

        public int Port  
        {
            get { return _port; }  
            set { _port = value; } 
        }  

        public string From  
        {
            get { return _from } 
            set { _from = value; }   
        }  

        public ArrayList To  
        {
            get { return _to; }  
            set 
            {
                if (!_to.Contains(value))  
                {
                    _to.Add(value);
                }
            }
        }

        public ArrayList Cc 
        {
            get { return _cc; }    
            set
            {
                if (!_cc.Contains(value))  
                {
                    _cc.Add(value);   
                }
            }
        }

        public string Subject 
        {

        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public ArrayList AttachFile
        {
            get { return _attach_file; }
            set
            {
                if (!_attach_file.Contains(value))
                {
                    _attach_file.Add(value);
                }
            }
        }

        public string PathAttachFile
        {
            get { return _path_attach_file; }
            set { _path_attach_file = value; }
        }

        public string Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }
    }


    public class SendMail  
    {
        public void SendMailAdmin(Mail_AdminEntity AdminEnt, string Body)   
        {
            try  
            {
                using (SmtpClient objSMTP = new SmtpClient(AdminEnt.Host))  
                {
                    if (AdminEnt.Port > 0)   
                    {
                        objSMTP.Port = AdminEnt.Port;   
                    }

                    using (MailMessage Mailobj = new MailMessage()) 
                    {
                        Mailobj.From = new MailAddress(AdminEnt.From);  

                        foreach (var to in AdminEnt.To)  
                        {
                            Mailobj.To.Add(new MailAddress(to.ToString()));    
                        }

                        Mailobj.Subject = "REPO Fail : " + AdminEnt.Subject;   
                        Mailobj.Body = Body;   
                        Mailobj.IsBodyHtml = true;  

                        objSMTP.Send(Mailobj);   
                    }
                }
            }
            catch    
            {

            }
        }

        public bool SendMailClient(ref string ReturnMsg, Mail_ClientEntity ClientEnt)   
        {
            try
            {
                using (SmtpClient objSMTP = new SmtpClient(ClientEnt.Host))  
                {
                    if (ClientEnt.Port > 0)   
                    {
                        objSMTP.Port = ClientEnt.Port;   
                    }  

                    using (MailMessage mailObj = new MailMessage())   
                    {
                        mailObj.From = new MailAddress(Client.From);  

                        foreach (var to in ClientEnt.To)  
                        {
                            mailObj.To.Add(new MailAddress(to.ToString()));    
                        }  

                        if (ClientEnt.Cc != null)   
                        {
                            foreach (var cc in ClientEnt.Cc)
                            {
                                mailObj.CC.Add(new MailAddress(cc.ToString()));  
                            }
                        } 

                        if (ClientEnt.AttachFile != null)   
                        {
                            foreach (var file in ClientEnt.AttachFile)   
                            {
                                Attachment AttachFile = new Attachment(ClientEnt.PathAttachFile + "\\" + file);   
                                mailObj.Attachments.Add(AttachFile);   
                            }  
                        }  

                        mailObj.Subject = "REPO : " + ClientEnt.Subject;   
                        mailObj.Body = ClientEnt.Body;   
                        mailObj.IsBodyHtml = true;  
                        objSMTP.Send(mailObj);   
                    }
                }
            }
            catch (Exception ex)   
            { 
                ReturnMsg = ex.Message;   
                return false;   
            } 
            return true;  
        }
    }
}
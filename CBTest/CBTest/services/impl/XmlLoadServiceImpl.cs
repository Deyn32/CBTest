using CBTest.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Schema;

namespace CBTest.services.impl
{
    public class XmlLoadServiceImpl: XmlLoadService
    {
        private SqlService SqlService;

        /// <summary>
        /// Метод загрузки XML
        /// </summary>
        /// <param name="fullFileName">Полное имя файла XML</param>
        public List<ParticipantInfo> Load(string fullFileName)
        {
            return LoadXml(fullFileName);
        }

        private List<ParticipantInfo> LoadXml(string fullFileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(ValidateXml(fullFileName));
            List<ParticipantInfo> participants = new List<ParticipantInfo>();
            SqlService = new SqlServiceImpl();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement)
            {
                ParticipantInfo participantInfo = new ParticipantInfo();
                List<Account> xmlAccounts = new List<Account>();
                participantInfo.Bic = xmlNode.Attributes[0].Value;

                if(xmlNode.ChildNodes.Count > 0)
                {
                    GetChilds(xmlNode.ChildNodes, participantInfo);
                    participants.Add(participantInfo);
                }
            }

            MessageBox.Show("Данные из XML были загружены в БД!");

            return participants;
        }

        private void GetChilds(XmlNodeList xmlNodeChilds ,ParticipantInfo participantInfo)
        {
            participantInfo.Accounts = new List<Account>();
            foreach(XmlNode xmlNode in xmlNodeChilds)
            {
                if (xmlNode.Attributes["ParticipantStatus"] != null)
                {
                    participantInfo.Status = xmlNode.Attributes["ParticipantStatus"].Value;
                    participantInfo.DateIn = DateTime.Parse(xmlNode.Attributes["DateIn"].Value);
                    participantInfo.Address = xmlNode.Attributes["Adr"] != null ? xmlNode.Attributes["Adr"].Value : null;
                    participantInfo.RegionName = xmlNode.Attributes["Nnp"] != null ? xmlNode.Attributes["Nnp"].Value : null;
                    participantInfo.Country = xmlNode.Attributes["CntrCd"] != null ? xmlNode.Attributes["CntrCd"].Value : null;
                    participantInfo.Name = xmlNode.Attributes["NameP"].Value;
                    SqlService.SaveParticipantInfo(participantInfo);
                }
                else if(xmlNode.Attributes["AccountStatus"] != null)
                {
                    Account account = new Account();
                    account.DateIn = DateTime.Parse(xmlNode.Attributes["DateIn"].Value);
                    account.Status = xmlNode.Attributes["AccountStatus"].Value;
                    account.CbrBic = xmlNode.Attributes["AccountCBRBIC"].Value;
                    account.CK = xmlNode.Attributes["CK"].Value;
                    account.Type = xmlNode.Attributes["RegulationAccountType"].Value;
                    account.Number = xmlNode.Attributes["Account"].Value;
                    account.Participant = participantInfo.Id;
                    participantInfo.Accounts.Add(account);
                    SqlService.SaveAccount(account);
                }
            }
        }

        private XmlReader ValidateXml(string fullFileName)
        {
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings();

            XmlSchemaSet xmlShemaSet = new XmlSchemaSet();
            try
            {
                XmlSchema xmlSchema = xmlShemaSet.Add("urn:cbr-ru:ed:v2.0", "cbr_ed807_v2023.4.0.xsd");
                settings.Schemas.Add(xmlSchema);
                settings.ValidationEventHandler += ValidationCallback;
                settings.ValidationFlags =
                    settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationType = ValidationType.Schema;

                reader = XmlReader.Create(fullFileName, settings);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"XSD file not found so generating: {ex.Message}");
            }

            return reader;
        }

        private void ValidationCallback(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show("The following validation warning occurred: " + e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                MessageBox.Show("The following critical validation errors occurred: " + e.Message);
            }
        }
    }
}

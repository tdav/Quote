using Newtonsoft.Json;

namespace Quote.Senders.ViewModels
{
    public class SmsModel
    {
        public string tel { get; set; }
        public string mes { get; set; }
        public string mes_id { get; set; }
    }

    internal class SmsSenderModel
    {
        public Message[] messages { get; set; }

        public SmsSenderModel(SmsModel sms)
        {
            this.messages = new Message[1];
            this.messages[0] = new Message();
            this.messages[0].messageid = sms.mes_id;
            this.messages[0].recipient = sms.tel;
            this.messages[0].messageid = sms.mes_id;
            this.messages[0].sms = new Sms();
            this.messages[0].sms.content = new Content() { text = sms.mes };
            this.messages[0].sms.originator = "3700";
        }
    }

    internal class Message
    {
        public string recipient { get; set; }
        [JsonProperty(PropertyName = "message-id")]
        public string messageid { get; set; }
        public Sms sms { get; set; }
    }

    internal class Sms
    {
        public string originator { get; set; }
        public Content content { get; set; }
    }

    internal class Content
    {
        public string text { get; set; }
    }

}

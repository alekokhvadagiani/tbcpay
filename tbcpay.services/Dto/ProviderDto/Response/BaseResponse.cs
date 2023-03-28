using System.Xml.Serialization;
using tbcpay.services.Helpers;

namespace tbcpay.services.Dto.ProviderDto.Response
{
    [XmlInclude(typeof(InfoResponse))]
    [XmlRoot(ElementName = "response")]
    public class BaseResponse
    {
      
        [XmlElement(ElementName = "result")]
        public ProviderStatusCodes Result { get; set; }

        [XmlElement(ElementName = "comment")] 
        public string Comment { get; set; }
    }
}
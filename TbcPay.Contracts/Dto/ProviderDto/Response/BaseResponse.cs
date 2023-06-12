using System.Xml.Serialization;
using TbcPay.Contracts.Enums;

namespace TbcPay.Contracts.Dto.ProviderDto.Response;

[XmlInclude(typeof(InfoResponse))]
[XmlRoot(ElementName = "response")]
public class BaseResponse
{
    [XmlElement(ElementName = "result")] public ProviderStatusCodes Result { get; set; }

    [XmlElement(ElementName = "comment")] public string Comment { get; set; }
}
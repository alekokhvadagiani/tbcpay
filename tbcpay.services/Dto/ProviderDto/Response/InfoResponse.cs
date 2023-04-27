namespace tbcpay.services.Dto.ProviderDto.Response;

public class InfoResponse : BaseResponse
{
    [XmlElement(ElementName = "info")] 
    public Info Info { get; set; }
}
   
[XmlRoot(ElementName = "extra")]
public class Extra
{
    [XmlAttribute(AttributeName = "name")] 
    public string Name { get; set; }
    [XmlText] public string Text { get; set; }
}

[XmlInclude(typeof(Extra))]
[XmlRoot(ElementName = "info")]
public class Info
{
    [XmlElement(ElementName = "extra")]
    public List<Extra> Extra { get; set; }
}
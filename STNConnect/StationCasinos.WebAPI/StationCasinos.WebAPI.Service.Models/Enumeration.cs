namespace StationCasinos.WebAPI.Service.Models
{
    public enum EmailType
    {
        None,
        Home,
        Work,
        Business,
    }

    public enum PhoneType
    {
        None,
        Home,
        Work,
        Business,
        Mobile,
        Fax,
        Pager,
    }

    public enum GenderType
    {
        Female,
        Male,
        Unknown,
    }

    public enum MaritalStatusType
    {
        Married,
        Single,
        Unknown,
    }

    public enum PinHashType
    {
        None,
        MD5,
    }
}
